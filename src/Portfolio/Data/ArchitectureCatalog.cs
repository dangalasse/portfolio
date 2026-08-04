using Portfolio.Models;

namespace Portfolio.Data;

/// <summary>One architecture flow per project / platform surface.</summary>
public static class ArchitectureCatalog
{
    public static ArchitectureFlow? ForProject(string slug) =>
        Flows.TryGetValue(slug, out var flow) ? flow : null;

    private static readonly Dictionary<string, ArchitectureFlow> Flows = new(StringComparer.OrdinalIgnoreCase)
    {
        ["tote"] = new ArchitectureFlow
        {
            ProjectSlug = "tote",
            Title = "Fluxo TOTE",
            Caption = "Browser → Cloudflare DNS → EC2 / Caddy → Next.js ↔ NestJS → Postgres + Redis",
            LayerOrder = ["client", "edge", "gateway", "app", "data"],
            Nodes =
            [
                N("browser", "Browser", "Utilizador", "googlechrome", "#e8eef1", "client"),
                N("cf", "Cloudflare DNS", "tote.galasse.dev", "cloudflare", "#f6821f", "edge"),
                N("ec2", "AWS EC2", "Elastic IP · Compose", "amazonec2", "#ff9900", "gateway"),
                N("caddy", "Caddy", "TLS Let's Encrypt", "nginx", "#3dd6c6", "gateway"),
                N("next", "Next.js", "Frontend :3000", "nextdotjs", "#e8eef1", "app"),
                N("nest", "NestJS", "API /v1 :3001", "nestjs", "#e0234e", "app"),
                N("pg", "PostgreSQL", "Prisma ORM", "postgresql", "#4169e1", "data"),
                N("redis", "Redis", "Cache / filas", "redis", "#dc382d", "data"),
                N("docker", "Docker", "Compose full", "docker", "#2496ed", "gateway"),
            ],
            Edges =
            [
                E("browser", "cf", "HTTPS"),
                E("cf", "ec2", "A · DNS only"),
                E("ec2", "caddy", "80/443"),
                E("caddy", "next", "proxy"),
                E("next", "nest", "/v1"),
                E("nest", "pg", "SQL"),
                E("nest", "redis", "cache"),
                E("docker", "caddy", "orchestrates"),
                E("docker", "next", null),
                E("docker", "nest", null),
                E("docker", "pg", null),
                E("docker", "redis", null),
            ],
        },
        ["portfolio"] = new ArchitectureFlow
        {
            ProjectSlug = "portfolio",
            Title = "Fluxo Portfólio",
            Caption = "Browser → Cloudflare → Caddy → ASP.NET Core · assets TypeScript + Tailwind",
            LayerOrder = ["client", "edge", "gateway", "app", "build"],
            Nodes =
            [
                N("browser", "Browser", "CV / demos", "googlechrome", "#e8eef1", "client"),
                N("cf", "Cloudflare DNS", "portfolio.galasse.dev", "cloudflare", "#f6821f", "edge"),
                N("apex", "galasse.dev", "301 redirect", "cloudflare", "#f6821f", "edge"),
                N("caddy", "Caddy", "TLS LE", "nginx", "#3dd6c6", "gateway"),
                N("aspnet", "ASP.NET Core", "Razor Pages", "dotnet", "#512bd4", "app"),
                N("ts", "TypeScript", "esbuild bundle", "typescript", "#3178c6", "build"),
                N("tw", "Tailwind CSS", "v4 CLI", "tailwindcss", "#38bdf8", "build"),
                N("ecr", "Amazon ECR", "Container image", "amazonwebservices", "#ff9900", "gateway"),
            ],
            Edges =
            [
                E("browser", "apex", "apex"),
                E("apex", "cf", "301"),
                E("browser", "cf", "HTTPS"),
                E("cf", "caddy", "EIP"),
                E("ecr", "caddy", "pull image"),
                E("caddy", "aspnet", ":8080"),
                E("ts", "aspnet", "wwwroot/js"),
                E("tw", "aspnet", "wwwroot/css"),
            ],
        },
        ["edge-status"] = new ArchitectureFlow
        {
            ProjectSlug = "edge-status",
            Title = "Fluxo Edge Status",
            Caption = "TypeScript no browser consulta probe ASP.NET ou Worker Cloudflare",
            LayerOrder = ["client", "origin", "edge"],
            Nodes =
            [
                N("browser", "Browser TS", "Labs indicators", "typescript", "#3178c6", "client"),
                N("aspnet", "ASP.NET /api/status", "Probe local", "dotnet", "#512bd4", "origin"),
                N("worker", "CF Worker", "region · cf-ray", "cloudflareworkers", "#f6821f", "edge"),
            ],
            Edges =
            [
                E("browser", "aspnet", "fallback"),
                E("browser", "worker", "live"),
            ],
        },
        ["aws-static-demo"] = new ArchitectureFlow
        {
            ProjectSlug = "aws-static-demo",
            Title = "Fluxo AWS Static",
            Caption = "Utilizador → CloudFront → S3 privado (read-only via CDN)",
            LayerOrder = ["client", "cdn", "storage", "iac"],
            Nodes =
            [
                N("user", "Utilizador", "HTTPS", "googlechrome", "#e8eef1", "client"),
                N("cfdist", "CloudFront", "CDN edge", "amazoncloudfront", "#ff9900", "cdn"),
                N("s3", "Amazon S3", "Bucket privado", "amazons3", "#569a31", "storage"),
                N("cdk", "AWS CDK", "IaC TypeScript", "amazonwebservices", "#ff9900", "iac"),
            ],
            Edges =
            [
                E("user", "cfdist", "HTTPS"),
                E("cfdist", "s3", "OAI / OAC"),
                E("cdk", "cfdist", "synth"),
                E("cdk", "s3", "synth"),
            ],
        },
    };

    private static ArchNode N(
        string id,
        string label,
        string subtitle,
        string icon,
        string color,
        string layer) =>
        new()
        {
            Id = id,
            Label = label,
            Subtitle = subtitle,
            Icon = icon,
            Color = color,
            Layer = layer,
        };

    private static ArchEdge E(string from, string to, string? label) =>
        new()
        {
            From = from,
            To = to,
            Label = label,
        };
}
