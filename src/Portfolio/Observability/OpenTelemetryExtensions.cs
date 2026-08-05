using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Portfolio.Observability;

/// <summary>
/// OpenTelemetry wiring for Grafana Cloud (OTLP).
/// WHY: traces + metrics leave the process over OTLP so the same Free Tier
/// Grafana Cloud stack can correlate app signals with host metrics from Alloy.
/// Secrets never live in code — use env vars (OTEL_EXPORTER_OTLP_*).
/// </summary>
public static class OpenTelemetryExtensions
{
    public const string ServiceName = "portfolio";

    /// <summary>
    /// Registers ASP.NET Core + HttpClient instrumentation and OTLP exporters.
    /// Disabled when OpenTelemetry:Enabled is false (local default) so dev
    /// machines do not spam Grafana Cloud Free Tier quotas.
    /// </summary>
    public static WebApplicationBuilder AddPortfolioOpenTelemetry(this WebApplicationBuilder builder)
    {
        var section = builder.Configuration.GetSection("OpenTelemetry");
        var enabled = section.GetValue("Enabled", false);
        if (!enabled)
        {
            return builder;
        }

        // WHY: prefer explicit env (Grafana Cloud paste) over appsettings so
        // containers get credentials via ECS/EC2 task env without rebuilding.
        var endpoint =
            Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT")
            ?? section["OtlpEndpoint"]
            ?? "https://otlp-gateway-prod-<REGION>.grafana.net/otlp";

        var serviceName = section["ServiceName"] ?? ServiceName;
        var serviceVersion =
            typeof(OpenTelemetryExtensions).Assembly.GetName().Version?.ToString() ?? "0.0.0";

        builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(
                serviceName: serviceName,
                serviceVersion: serviceVersion,
                serviceInstanceId: Environment.MachineName))
            .WithTracing(tracing =>
            {
                tracing
                    .AddAspNetCoreInstrumentation(options =>
                    {
                        // WHY: /api/status is polled by Labs — exclude to cut noise and Free Tier series.
                        options.Filter = context =>
                            !context.Request.Path.StartsWithSegments("/api/status");
                    })
                    .AddHttpClientInstrumentation()
                    .AddOtlpExporter(exporter =>
                    {
                        exporter.Endpoint = new Uri(endpoint);
                        // Headers (Authorization Basic …) come from OTEL_EXPORTER_OTLP_HEADERS.
                    });
            })
            .WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddOtlpExporter(exporter =>
                    {
                        exporter.Endpoint = new Uri(endpoint);
                    });
            });

        return builder;
    }
}
