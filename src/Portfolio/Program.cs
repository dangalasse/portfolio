using Portfolio.Data;

var builder = WebApplication.CreateBuilder(args);

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

app.MapGet("/api/status", () =>
{
    var profile = PortfolioCatalog.Profile;
    return Results.Json(new
    {
        ok = true,
        region = Environment.GetEnvironmentVariable("TOTE_REGION") ?? "local-aspnet",
        runtime = $".NET {Environment.Version}",
        service = "portfolio-status",
        profile = profile.Name,
        checkedAt = DateTimeOffset.UtcNow,
    });
});

app.MapRazorPages();

app.Run();
