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

app.MapRazorPages();

app.Run();
