using Portfolio.I18n;
using Portfolio.Models;

namespace Portfolio.Data;

/// <summary>One architecture flow per project / platform surface (pt-BR + en-US).</summary>
public static class ArchitectureCatalog
{
    public static ArchitectureFlow? ForProject(string slug, string? locale = null)
    {
        if (!Flows.TryGetValue(slug, out var flow))
        {
            return null;
        }

        var en = AppLocales.IsEnglish(locale);
        return new ArchitectureFlow
        {
            ProjectSlug = flow.ProjectSlug,
            Title = en ? flow.TitleEn : flow.TitlePt,
            Caption = en ? flow.CaptionEn : flow.CaptionPt,
            LayerOrder = flow.LayerOrder,
            Nodes = flow.Nodes
                .Select(n => new ArchNode
                {
                    Id = n.Id,
                    Label = n.Label,
                    Subtitle = en ? n.SubtitleEn : n.SubtitlePt,
                    Icon = n.Icon,
                    Color = n.Color,
                    Layer = n.Layer,
                })
                .ToList(),
            Edges = flow.Edges,
        };
    }

    private sealed class FlowDef
    {
        public required string ProjectSlug { get; init; }
        public required string TitlePt { get; init; }
        public required string TitleEn { get; init; }
        public required string CaptionPt { get; init; }
        public required string CaptionEn { get; init; }
        public required IReadOnlyList<string> LayerOrder { get; init; }
        public required IReadOnlyList<NodeDef> Nodes { get; init; }
        public required IReadOnlyList<ArchEdge> Edges { get; init; }
    }

    private sealed class NodeDef
    {
        public required string Id { get; init; }
        public required string Label { get; init; }
        public required string SubtitlePt { get; init; }
        public required string SubtitleEn { get; init; }
        public required string Icon { get; init; }
        public required string Color { get; init; }
        public required string Layer { get; init; }
    }

    private static NodeDef N(
        string id,
        string label,
        string subtitlePt,
        string subtitleEn,
        string icon,
        string color,
        string layer) =>
        new()
        {
            Id = id,
            Label = label,
            SubtitlePt = subtitlePt,
            SubtitleEn = subtitleEn,
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

    private static readonly Dictionary<string, FlowDef> Flows = new(StringComparer.OrdinalIgnoreCase)
    {
        ["tote"] = new FlowDef
        {
            ProjectSlug = "tote",
            TitlePt = "Fluxo TOTE",
            TitleEn = "TOTE flow",
            CaptionPt = "Browser → Cloudflare DNS → EC2 / Caddy → Next.js ↔ NestJS → Postgres + Redis",
            CaptionEn = "Browser → Cloudflare DNS → EC2 / Caddy → Next.js ↔ NestJS → Postgres + Redis",
            LayerOrder = ["client", "edge", "gateway", "app", "data"],
            Nodes =
            [
                N("browser", "Browser", "Usuário", "User", "googlechrome", "#e8eef1", "client"),
                N("cf", "Cloudflare DNS", "tote.galasse.dev", "tote.galasse.dev", "cloudflare", "#f6821f", "edge"),
                N("ec2", "AWS EC2", "Elastic IP · Compose", "Elastic IP · Compose", "amazonec2", "#ff9900", "gateway"),
                N("caddy", "Caddy", "TLS Let's Encrypt", "TLS Let's Encrypt", "nginx", "#3dd6c6", "gateway"),
                N("next", "Next.js", "Frontend :3000", "Frontend :3000", "nextdotjs", "#e8eef1", "app"),
                N("nest", "NestJS", "API /v1 :3001", "API /v1 :3001", "nestjs", "#e0234e", "app"),
                N("pg", "PostgreSQL", "Prisma ORM", "Prisma ORM", "postgresql", "#4169e1", "data"),
                N("redis", "Redis", "Cache / filas", "Cache / queues", "redis", "#dc382d", "data"),
                N("docker", "Docker", "Compose full", "Compose full", "docker", "#2496ed", "gateway"),
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
        ["portfolio"] = new FlowDef
        {
            ProjectSlug = "portfolio",
            TitlePt = "Fluxo Portfólio",
            TitleEn = "Portfolio flow",
            CaptionPt = "Browser → Cloudflare → Caddy → ASP.NET Core · assets TypeScript + Tailwind",
            CaptionEn = "Browser → Cloudflare → Caddy → ASP.NET Core · TypeScript + Tailwind assets",
            LayerOrder = ["client", "edge", "gateway", "app", "build"],
            Nodes =
            [
                N("browser", "Browser", "CV / demos", "Resume / demos", "googlechrome", "#e8eef1", "client"),
                N("cf", "Cloudflare DNS", "portfolio.galasse.dev", "portfolio.galasse.dev", "cloudflare", "#f6821f", "edge"),
                N("apex", "galasse.dev", "301 redirect", "301 redirect", "cloudflare", "#f6821f", "edge"),
                N("caddy", "Caddy", "TLS LE", "TLS LE", "nginx", "#3dd6c6", "gateway"),
                N("aspnet", "ASP.NET Core", "Razor Pages", "Razor Pages", "dotnet", "#512bd4", "app"),
                N("ts", "TypeScript", "esbuild bundle", "esbuild bundle", "typescript", "#3178c6", "build"),
                N("tw", "Tailwind CSS", "v4 CLI", "v4 CLI", "tailwindcss", "#38bdf8", "build"),
                N("ecr", "Amazon ECR", "Container image", "Container image", "amazonwebservices", "#ff9900", "gateway"),
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
        ["edge-status"] = new FlowDef
        {
            ProjectSlug = "edge-status",
            TitlePt = "Fluxo Edge Status",
            TitleEn = "Edge Status flow",
            CaptionPt = "TypeScript no browser consulta probe ASP.NET ou Worker Cloudflare",
            CaptionEn = "Browser TypeScript hits the ASP.NET probe or Cloudflare Worker",
            LayerOrder = ["client", "origin", "edge"],
            Nodes =
            [
                N("browser", "Browser TS", "Indicadores Labs", "Labs indicators", "typescript", "#3178c6", "client"),
                N("aspnet", "ASP.NET /api/status", "Probe local", "Local probe", "dotnet", "#512bd4", "origin"),
                N("worker", "CF Worker", "region · cf-ray", "region · cf-ray", "cloudflareworkers", "#f6821f", "edge"),
            ],
            Edges =
            [
                E("browser", "aspnet", "fallback"),
                E("browser", "worker", "live"),
            ],
        },
        ["aws-static-demo"] = new FlowDef
        {
            ProjectSlug = "aws-static-demo",
            TitlePt = "Fluxo AWS Static",
            TitleEn = "AWS Static flow",
            CaptionPt = "Usuário → CloudFront (OAC) → S3 privado · Terraform",
            CaptionEn = "User → CloudFront (OAC) → private S3 · Terraform",
            LayerOrder = ["client", "cdn", "storage", "iac"],
            Nodes =
            [
                N("user", "Usuário", "HTTPS", "HTTPS", "googlechrome", "#e8eef1", "client"),
                N("cfdist", "CloudFront", "CDN + OAC", "CDN + OAC", "amazoncloudfront", "#ff9900", "cdn"),
                N("s3", "Amazon S3", "Bucket privado", "Private bucket", "amazons3", "#569a31", "storage"),
                N("tf", "Terraform", "IaC", "IaC", "terraform", "#7B42BC", "iac"),
            ],
            Edges =
            [
                E("user", "cfdist", "HTTPS"),
                E("cfdist", "s3", "OAC SigV4"),
                E("tf", "cfdist", "apply"),
                E("tf", "s3", "apply"),
            ],
        },
        ["edge-labs"] = new FlowDef
        {
            ProjectSlug = "edge-labs",
            TitlePt = "Fluxo Edge Labs (LLMOps)",
            TitleEn = "Edge Labs (LLMOps) flow",
            CaptionPt = "Cliente → Worker → Gemini AI Studio → JSON de remediação",
            CaptionEn = "Client → Worker → Gemini AI Studio → remediation JSON",
            LayerOrder = ["client", "edge", "ai"],
            Nodes =
            [
                N("client", "Cliente", "POST /analyze-error", "POST /analyze-error", "googlechrome", "#e8eef1", "client"),
                N("worker", "CF Worker", "edge-labs", "edge-labs", "cloudflare", "#f6821f", "edge"),
                N("gemini", "Workers AI", "llama fp8 / Gemini", "llama fp8 / Gemini", "cloudflare", "#f6821f", "ai"),
            ],
            Edges =
            [
                E("client", "worker", "JSON log"),
                E("worker", "gemini", "inference"),
                E("gemini", "worker", "analysis"),
                E("worker", "client", "fix"),
            ],
        },
    };
}
