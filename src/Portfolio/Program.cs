using Portfolio.Data;
using Portfolio.I18n;
using Portfolio.Observability;

var builder = WebApplication.CreateBuilder(args);

// WHY: OTLP → Grafana Cloud Free Tier; gated by OpenTelemetry:Enabled / env.
builder.AddPortfolioOpenTelemetry();

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Behind Caddy TLS — avoid HTTPS redirect noise in container logs.
if (!app.Environment.IsDevelopment())
{
    // keep HSTS when served publicly via Caddy
}
else
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// WHY: playful honeypots for scanners — no secrets, no privilege path.
var honeypotPaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
{
    "/.env", "/.env.local", "/.git/config", "/wp-admin", "/wp-login.php",
    "/admin", "/admin/login", "/api/v1/secrets", "/phpmyadmin", "/actuator/env",
};
var honeypotHints = new[] { "tenta mais", "ainda não", "quase lá", "boa tentativa" };
app.Use(async (http, next) =>
{
    var path = http.Request.Path.Value ?? "";
    if (honeypotPaths.Contains(path))
    {
        var hint = honeypotHints[Math.Abs(path.GetHashCode()) % honeypotHints.Length];
        http.Response.StatusCode = StatusCodes.Status404NotFound;
        http.Response.Headers.CacheControl = "no-store";
        http.Response.Headers["X-Content-Type-Options"] = "nosniff";
        await http.Response.WriteAsJsonAsync(new
        {
            ok = false,
            hint,
            note = "Fourth wall: nothing useful here. Try /Labs instead.",
        });
        return;
    }

    await next();
});

// WHY: Labs probe prefers Cloudflare Worker when EDGE_STATUS_URL is set on the container.
app.Use(async (http, next) =>
{
    var edgeStatus = Environment.GetEnvironmentVariable("EDGE_STATUS_URL");
    if (!string.IsNullOrWhiteSpace(edgeStatus))
    {
        http.Items["EdgeStatusUrl"] = edgeStatus.Trim();
    }

    await next();
});

app.MapGet("/api/status", (HttpContext http) =>
{
    var locale = Locale.Current(http);
    var profile = PortfolioCatalog.For(locale).Profile;
    return Results.Json(new
    {
        ok = true,
        region = Environment.GetEnvironmentVariable("TOTE_REGION") ?? "local-aspnet",
        runtime = $".NET {Environment.Version}",
        service = "portfolio-status",
        profile = profile.Name,
        locale,
        checkedAt = DateTimeOffset.UtcNow,
    });
});

app.MapPost("/api/locale", async (HttpContext http) =>
{
    var form = await http.Request.ReadFormAsync();
    var locale = AppLocales.Normalize(form["locale"].ToString());
    var returnUrl = form["returnUrl"].ToString();
    if (string.IsNullOrWhiteSpace(returnUrl) || !returnUrl.StartsWith('/'))
    {
        returnUrl = "/";
    }

    http.Response.Cookies.Append(
        AppLocales.CookieName,
        locale,
        new CookieOptions
        {
            Path = "/",
            MaxAge = TimeSpan.FromDays(365),
            SameSite = SameSiteMode.Lax,
            IsEssential = true,
            HttpOnly = false,
        });

    return Results.Redirect(returnUrl);
});

// WHY: legacy Google-indexed slug after Pipeview rename.
app.MapGet("/Projects/pipeline-pulse", () => Results.Redirect("/Projects/pipeview", permanent: true));

app.MapGet("/robots.txt", (HttpContext http) =>
{
    var profile = PortfolioCatalog.ProfileBase;
    var baseUrl = profile.SiteUrl.TrimEnd('/');
    var body =
        $"User-agent: *\nAllow: /\nDisallow: /Error\nDisallow: /api/\n\nSitemap: {baseUrl}/sitemap.xml\n";
    return Results.Text(body, "text/plain; charset=utf-8");
});

app.MapGet("/sitemap.xml", (HttpContext http) =>
{
    var profile = PortfolioCatalog.ProfileBase;
    var baseUrl = profile.SiteUrl.TrimEnd('/');
    var urls = new (string Path, string Priority, string Changefreq)[]
    {
        ("/", "1.0", "weekly"),
        ("/Projects", "0.9", "weekly"),
        ("/Labs", "0.9", "weekly"),
        ("/About", "0.8", "monthly"),
    };

    var sb = new System.Text.StringBuilder();
    sb.AppendLine("""<?xml version="1.0" encoding="UTF-8"?>""");
    sb.AppendLine("""<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">""");
    foreach (var (path, priority, changefreq) in urls)
    {
        sb.AppendLine("  <url>");
        sb.AppendLine($"    <loc>{baseUrl}{path}</loc>");
        sb.AppendLine($"    <changefreq>{changefreq}</changefreq>");
        sb.AppendLine($"    <priority>{priority}</priority>");
        sb.AppendLine("  </url>");
    }

    sb.AppendLine("</urlset>");
    return Results.Text(sb.ToString(), "application/xml; charset=utf-8");
});

app.MapRazorPages();

app.Run();
