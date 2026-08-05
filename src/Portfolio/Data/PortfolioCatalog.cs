using Portfolio.I18n;
using Portfolio.Models;

namespace Portfolio.Data;

/// <summary>
/// Static portfolio content (pt-BR + en-US). Resolve with <see cref="For"/>.
/// </summary>
public static class PortfolioCatalog
{
    public static SiteProfile ProfileBase { get; } = new()
    {
        Name = "Danton Galasse",
        Role = "Infra · SRE & DevOps · Cloud",
        TaglinePtBr =
            "Infraestrutura de TI com foco em SRE, DevOps e IA — demos ao vivo com Cloudflare e AWS, sem teatro.",
        TaglineEnUs =
            "IT infrastructure focused on SRE, DevOps, and AI — live demos on Cloudflare and AWS, no theater.",
        Location = "Brasil",
        Email = "contato@example.com",
        LinkedInUrl = "https://www.linkedin.com/in/danton-galasse",
        GitHubUrl = "https://github.com/dangalasse",
        SiteUrl = "https://portfolio.galasse.dev",
        CvPdfPath = "/files/cv.pdf",
    };

    /// <summary>Backward-compatible default (pt-BR).</summary>
    public static SiteProfile Profile => For(AppLocales.PtBr).Profile;

    public static IReadOnlyList<ProjectItem> ProjectsPt { get; } =
    [
        new ProjectItem
        {
            Slug = "tote",
            Title = "TOTE",
            Summary =
                "Plataforma de patrimônio com whitelabel, setup wizard e deploy zero-touch.",
            Description =
                "Sistema full-stack com bounded contexts, identidade visual configurável e operação em containers. Demonstra Nest/Next no produto, disciplina de domínio (SDD) e chart Helm demonstrativo em k8s/tote-chart/.",
            SummaryEn =
                "Asset management platform with whitelabel branding, setup wizard, and zero-touch deploy.",
            DescriptionEn =
                "Full-stack system with bounded contexts, configurable visual identity, and container operations. Shows Nest/Next in product, domain discipline (SDD), and a demonstrative Helm chart under k8s/tote-chart/.",
            Stack =
            [
                "TypeScript",
                "Next.js",
                "NestJS",
                "PostgreSQL",
                "Docker",
                "Caddy",
                "AWS EC2",
                "Helm",
            ],
            Accent = "#3dd6c6",
            LiveUrl = "https://demo.tote.galasse.dev",
            RepoUrl = "https://github.com/dangalasse/TOTE",
            DemoNote =
                "Vitrine efêmera em demo.tote.galasse.dev — workspace isolado por visitante.",
            DemoNoteEn =
                "Ephemeral showcase at demo.tote.galasse.dev — isolated workspace per visitor.",
            Featured = true,
        },
        new ProjectItem
        {
            Slug = "pipeline-pulse",
            Title = "Pipeline Pulse",
            Summary =
                "Esteira CI/CD completa: GitHub Actions → Cloudflare Workers, com meta-dashboard ao vivo.",
            Description =
                "Repo de vitrine DevOps: lint/typecheck/test/build, preview de PR, staging com smoke /api/health e production via tag/environment. A UI mostra SHA, ambiente e o workflow run que publicou o build.",
            SummaryEn =
                "Full CI/CD conveyor: GitHub Actions → Cloudflare Workers, with a live meta-dashboard.",
            DescriptionEn =
                "DevOps showcase repo: lint/typecheck/test/build, PR preview, staging with /api/health smoke, and production via tag/environment. The UI shows SHA, environment, and the workflow run that shipped the build.",
            Stack =
            [
                "GitHub Actions",
                "Cloudflare Workers",
                "Hono",
                "Vite",
                "React",
                "Biome",
                "Vitest",
            ],
            Accent = "#3dd6a5",
            LiveUrl = "https://pipeline.galasse.dev",
            RepoUrl = "https://github.com/dangalasse/pipeline-pulse",
            DemoNote =
                "Live em pipeline.galasse.dev — veja também a aba Actions do repo.",
            DemoNoteEn =
                "Live at pipeline.galasse.dev — also open the repo Actions tab.",
            Featured = true,
        },
        new ProjectItem
        {
            Slug = "edge-status",
            Title = "Edge Status Lab",
            Summary =
                "Worker Cloudflare que retorna região, cf-ray e latência — prova de edge real.",
            Description =
                "Mini-lab ligado a esta página Labs: o frontend TypeScript consulta um endpoint e mostra indicadores ao vivo. Até o Worker estar deployado, o probe ASP.NET responde localmente.",
            SummaryEn =
                "Cloudflare Worker that returns region, cf-ray, and latency — real edge proof.",
            DescriptionEn =
                "Mini-lab wired to this Labs page: the TypeScript frontend hits an endpoint and shows live indicators. Until the Worker is deployed, the ASP.NET probe answers locally.",
            Stack = ["Cloudflare Workers", "TypeScript", "ASP.NET Core"],
            Accent = "#f6821f",
            LiveUrl = "/Labs",
            RepoUrl = "https://github.com/dangalasse/portfolio/tree/main/workers/edge-status",
            DemoNote = "Indicador ao vivo na página Labs.",
            DemoNoteEn = "Live indicator on the Labs page.",
            Featured = true,
        },
        new ProjectItem
        {
            Slug = "aws-static-demo",
            Title = "AWS Static Demo",
            Summary =
                "Asset estático servido via S3 + CloudFront (OAC) provisionado com Terraform.",
            Description =
                "Fatia mínima de AWS para CV: bucket privado, Origin Access Control, distribuição CloudFront PriceClass_100. Objetivo: prova de IaC Free Tier sem microserviços desnecessários.",
            SummaryEn =
                "Static asset via S3 + CloudFront (OAC) provisioned with Terraform.",
            DescriptionEn =
                "Minimal AWS slice for a resume: private bucket, Origin Access Control, CloudFront PriceClass_100. Goal: Free Tier IaC proof without unnecessary microservices.",
            Stack = ["AWS S3", "CloudFront", "Terraform", "OAC"],
            Accent = "#ff9900",
            LiveUrl = "https://static.galasse.dev",
            RepoUrl = "https://github.com/dangalasse/aws-static-demo",
            DemoNote =
                "Live em static.galasse.dev (S3 privado + CloudFront OAC + ACM). Código Terraform no GitHub.",
            DemoNoteEn =
                "Live at static.galasse.dev (private S3 + CloudFront OAC + ACM). Terraform on GitHub.",
            Featured = true,
        },
        new ProjectItem
        {
            Slug = "edge-labs",
            Title = "Edge Labs (LLMOps)",
            Summary =
                "Worker Cloudflare que analisa logs de erro com Workers AI (Free Tier) e Gemini opcional.",
            Description =
                "LLMOps no edge: POST /analyze-error → Workers AI (llama fp8) ou Gemini → summary, likelyCause, suggestedFix. Domínio edge.galasse.dev. API keys só via wrangler secret.",
            SummaryEn =
                "Cloudflare Worker that analyzes error logs with Workers AI (Free Tier) and optional Gemini.",
            DescriptionEn =
                "Edge LLMOps: POST /analyze-error → Workers AI (llama fp8) or Gemini → summary, likelyCause, suggestedFix. Domain edge.galasse.dev. API keys only via wrangler secret.",
            Stack = ["Cloudflare Workers", "Workers AI", "TypeScript", "Gemini", "Wrangler"],
            Accent = "#8b5cf6",
            LiveUrl = "https://edge.galasse.dev/",
            RepoUrl = "https://github.com/dangalasse/edge-labs",
            DemoNote =
                "Abra edge.galasse.dev — botão Analyze live. A resposta JSON traz provider, model e analyzedAt (prova de inferência real). Hoje: Workers AI; Gemini quando o secret GEMINI_API_KEY existir.",
            DemoNoteEn =
                "Open edge.galasse.dev — Analyze live. JSON includes provider, model, analyzedAt (real inference proof). Today: Workers AI; Gemini when GEMINI_API_KEY secret is set.",
            Featured = true,
        },
        new ProjectItem
        {
            Slug = "portfolio",
            Title = "Este portfólio",
            TitleEn = "This portfolio",
            Summary =
                "ASP.NET Core Razor Pages + TypeScript + Tailwind — simples, rápido, demonstrável.",
            Description =
                "Site de CV com projetos, labs Cloudflare/AWS e links LinkedIn/GitHub. Build de assets via npm (esbuild + Tailwind v4), hospedado na EC2 atrás do Caddy com DNS Cloudflare. CI: Actions → ECR → Ansible; OTEL → Grafana Cloud.",
            SummaryEn =
                "ASP.NET Core Razor Pages + TypeScript + Tailwind — simple, fast, demonstrable.",
            DescriptionEn =
                "Resume site with projects, Cloudflare/AWS labs, and LinkedIn/GitHub links. Asset build via npm (esbuild + Tailwind v4), hosted on EC2 behind Caddy with Cloudflare DNS. CI: Actions → ECR → Ansible; OTEL → Grafana Cloud.",
            Stack =
            [
                "ASP.NET Core 8",
                "TypeScript",
                "Tailwind CSS",
                "Caddy",
                "ECR",
                "Ansible",
                "OpenTelemetry",
            ],
            Accent = "#9db0bb",
            LiveUrl = "https://portfolio.galasse.dev",
            RepoUrl = "https://github.com/dangalasse/portfolio",
            DemoNote =
                "Você está vendo a demo — veja o fluxo de arquitetura abaixo.",
            DemoNoteEn =
                "You're looking at the live demo — see the architecture flow below.",
            Featured = false,
        },
    ];

    public static IReadOnlyList<LabIndicator> LabsPt { get; } =
    [
        new LabIndicator
        {
            Id = "cloudflare",
            Title = "Cloudflare Edge",
            Provider = "Cloudflare",
            Description =
                "Workers/DNS + Pipeline Pulse + Edge Labs (LLMOps em edge.galasse.dev).",
            DescriptionEn =
                "Workers/DNS + Pipeline Pulse + Edge Labs (LLMOps at edge.galasse.dev).",
            Proof = "pipeline.galasse.dev · edge.galasse.dev/health",
            ProofEn = "pipeline.galasse.dev · edge.galasse.dev/health",
            DocsUrl = "https://edge.galasse.dev/health",
        },
        new LabIndicator
        {
            Id = "github",
            Title = "GitHub Actions",
            Provider = "GitHub",
            Description =
                "CI/CD público no Pipeline Pulse — CI, preview, staging e production.",
            DescriptionEn =
                "Public CI/CD on Pipeline Pulse — CI, preview, staging, and production.",
            Proof = "Badges e runs em dangalasse/pipeline-pulse/actions.",
            ProofEn = "Badges and runs at dangalasse/pipeline-pulse/actions.",
            DocsUrl = "https://github.com/dangalasse/pipeline-pulse/actions",
        },
        new LabIndicator
        {
            Id = "aws",
            Title = "AWS Surface",
            Provider = "AWS",
            Description =
                "S3 privado + CloudFront OAC (Terraform) em static.galasse.dev + portfolio na EC2/ECR.",
            DescriptionEn =
                "Private S3 + CloudFront OAC (Terraform) at static.galasse.dev + portfolio on EC2/ECR.",
            Proof = "static.galasse.dev · github.com/dangalasse/aws-static-demo",
            ProofEn = "static.galasse.dev · github.com/dangalasse/aws-static-demo",
            DocsUrl = "https://static.galasse.dev",
        },
    ];

    public static CatalogSnapshot For(string locale)
    {
        var en = AppLocales.IsEnglish(locale);
        var profile = ProfileBase with
        {
            Tagline = en ? ProfileBase.TaglineEnUs : ProfileBase.TaglinePtBr,
        };

        var projects = ProjectsPt
            .Select(p => p with
            {
                Title = en && !string.IsNullOrWhiteSpace(p.TitleEn) ? p.TitleEn! : p.Title,
                Summary = en ? (p.SummaryEn ?? p.Summary) : p.Summary,
                Description = en ? (p.DescriptionEn ?? p.Description) : p.Description,
                DemoNote = en ? (p.DemoNoteEn ?? p.DemoNote) : p.DemoNote,
            })
            .ToList();

        var labs = LabsPt
            .Select(l => l with
            {
                Description = en ? (l.DescriptionEn ?? l.Description) : l.Description,
                Proof = en ? (l.ProofEn ?? l.Proof) : l.Proof,
            })
            .ToList();

        return new CatalogSnapshot(profile, projects, labs);
    }

    public static ProjectItem? FindProject(string slug, string locale) =>
        For(locale).Projects.FirstOrDefault(p =>
            p.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));

    public static ProjectItem? FindProject(string slug) =>
        FindProject(slug, AppLocales.PtBr);
}

public sealed record CatalogSnapshot(
    SiteProfile Profile,
    IReadOnlyList<ProjectItem> Projects,
    IReadOnlyList<LabIndicator> Labs);
