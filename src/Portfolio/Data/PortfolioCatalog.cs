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
            "Sou um profissional de infraestrutura de TI com 5 anos de experiência na área, atualmente focado em Inteligência Artificial e cultura SRE & DevOps.",
        TaglineEnUs =
            "I'm an IT infrastructure professional with 5 years of experience, currently focused on Artificial Intelligence and SRE & DevOps culture.",
        Location = "Brasil",
        Email = "danton@galasse.dev",
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
                "Full-stack com bounded contexts, identidade visual configurável e operação em containers. Nest/Next no produto, disciplina de domínio (SDD) e um chart Helm demonstrativo em k8s/tote-chart/.",
            SummaryEn =
                "Asset management platform with whitelabel branding, setup wizard, and zero-touch deploy.",
            DescriptionEn =
                "Full-stack with bounded contexts, configurable visual identity, and container ops. Nest/Next in the product, domain discipline (SDD), and a demonstrative Helm chart under k8s/tote-chart/.",
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
                "Vitrine em demo.tote.galasse.dev — workspace isolado por visitante.",
            DemoNoteEn =
                "Showcase at demo.tote.galasse.dev — isolated workspace per visitor.",
            Featured = true,
        },
        new ProjectItem
        {
            Slug = "pipeview",
            Title = "Pipeview",
            Summary =
                "Painel de uma esteira CI/CD real: GitHub Actions → Cloudflare Workers, com o que foi publicado à vista.",
            Description =
                "Vitrine DevOps: lint, typecheck, testes e build; preview de PR; staging com smoke em /api/health; production via tag/environment. A UI mostra SHA, ambiente e o workflow que publicou o build.",
            SummaryEn =
                "A live view of a real CI/CD conveyor: GitHub Actions → Cloudflare Workers, with what shipped in plain sight.",
            DescriptionEn =
                "DevOps showcase: lint, typecheck, tests, and build; PR preview; staging with /api/health smoke; production via tag/environment. The UI shows SHA, environment, and the workflow that shipped the build.",
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
            LiveUrl = "https://pipeview.galasse.dev",
            RepoUrl = "https://github.com/dangalasse/pipeline-pulse",
            DemoNote =
                "Disponível em pipeview.galasse.dev — a aba Actions do repositório também vale a visita.",
            DemoNoteEn =
                "Available at pipeview.galasse.dev — the repo Actions tab is worth a look too.",
            Featured = true,
        },
        new ProjectItem
        {
            Slug = "edge-status",
            Title = "Edge Status Lab",
            Summary =
                "Worker Cloudflare que devolve região, cf-ray e latência — o probe que alimenta a página Labs.",
            Description =
                "Mini-lab ligado à página Labs: o frontend TypeScript consulta um endpoint e mostra os indicadores. Enquanto o Worker não estiver deployado, o probe ASP.NET responde localmente.",
            SummaryEn =
                "Cloudflare Worker that returns region, cf-ray, and latency — the probe behind the Labs page.",
            DescriptionEn =
                "Mini-lab wired to the Labs page: the TypeScript frontend hits an endpoint and shows the indicators. Until the Worker is deployed, the ASP.NET probe answers locally.",
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
                "Asset estático via S3 + CloudFront (OAC), provisionado com Terraform.",
            Description =
                "Fatia mínima de AWS para o CV: bucket privado, Origin Access Control, CloudFront PriceClass_100. Objetivo: mostrar IaC Free Tier de forma enxuta.",
            SummaryEn =
                "Static asset via S3 + CloudFront (OAC), provisioned with Terraform.",
            DescriptionEn =
                "Minimal AWS slice for a resume: private bucket, Origin Access Control, CloudFront PriceClass_100. Goal: show Free Tier IaC without extra moving parts.",
            Stack = ["AWS S3", "CloudFront", "Terraform", "OAC"],
            Accent = "#ff9900",
            LiveUrl = "https://static.galasse.dev",
            RepoUrl = "https://github.com/dangalasse/aws-static-demo",
            DemoNote =
                "Live em static.galasse.dev (S3 privado + CloudFront OAC + ACM). Terraform no GitHub.",
            DemoNoteEn =
                "Live at static.galasse.dev (private S3 + CloudFront OAC + ACM). Terraform on GitHub.",
            Featured = true,
        },
        new ProjectItem
        {
            Slug = "aws-ops-labs",
            Title = "AWS Ops Labs",
            Summary =
                "Probe Always Free: Lambda Function URL + DynamoDB + EventBridge + KMS GenerateRandom.",
            Description =
                "Lab público sem CMK e sem API Gateway. A cada 5 minutos a Lambda testa as superfícies ao vivo, grava o resultado no DynamoDB (TTL 7d) e emite métricas EMF. GET /kms/random pede entropia ao KMS sem chave gerenciada ($1/mês evitado).",
            SummaryEn =
                "Always Free probe: Lambda Function URL + DynamoDB + EventBridge + KMS GenerateRandom.",
            DescriptionEn =
                "Public lab with no CMK and no API Gateway. Every 5 minutes Lambda checks live surfaces, writes DynamoDB (7-day TTL), and emits EMF metrics. GET /kms/random pulls HSM entropy without a customer-managed key ($1/mo avoided).",
            Stack =
            [
                "AWS Lambda",
                "Function URL",
                "DynamoDB",
                "EventBridge",
                "KMS",
                "CloudFormation",
            ],
            Accent = "#ff9900",
            LiveUrl = "https://4notqcazblkzqyd3avwjrkxtki0grnho.lambda-url.sa-east-1.on.aws/status",
            RepoUrl = "https://github.com/dangalasse/portfolio/tree/main/labs/always-free",
            DemoNote =
                "GET /status, POST /probe, GET /kms/random — CloudFormation em labs/always-free.",
            DemoNoteEn =
                "GET /status, POST /probe, GET /kms/random — CloudFormation under labs/always-free.",
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
                "Em edge.galasse.dev pode experimentar o Analyze live. A resposta JSON traz provider, model e analyzedAt. Hoje: Workers AI; Gemini quando o secret GEMINI_API_KEY existir.",
            DemoNoteEn =
                "At edge.galasse.dev you can try Analyze live. The JSON includes provider, model, and analyzedAt. Today: Workers AI; Gemini when the GEMINI_API_KEY secret is set.",
            Featured = true,
        },
        new ProjectItem
        {
            Slug = "portfolio",
            Title = "Este portfólio",
            TitleEn = "This portfolio",
            Summary =
                "ASP.NET Core Razor Pages + TypeScript + Tailwind — site de CV que também hospeda os labs.",
            Description =
                "Site de CV com projetos, labs Cloudflare/AWS e links LinkedIn/GitHub. Assets via npm (esbuild + Tailwind v4), na EC2 atrás do Caddy com DNS Cloudflare. CI: Actions → ECR → Ansible. Observabilidade: OTEL → Alloy → Grafana Cloud.",
            SummaryEn =
                "ASP.NET Core Razor Pages + TypeScript + Tailwind — resume site that also hosts the labs.",
            DescriptionEn =
                "Resume site with projects, Cloudflare/AWS labs, and LinkedIn/GitHub links. Assets via npm (esbuild + Tailwind v4), on EC2 behind Caddy with Cloudflare DNS. CI: Actions → ECR → Ansible. Observability: OTEL → Alloy → Grafana Cloud.",
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
                "Este próprio site é a demo — o fluxo de arquitetura aparece mais abaixo.",
            DemoNoteEn =
                "This site is the live demo — the architecture flow appears further down.",
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
                "Workers e DNS: Pipeview e Edge Labs (LLMOps em edge.galasse.dev — / e /coach).",
            DescriptionEn =
                "Workers and DNS: Pipeview and Edge Labs (LLMOps at edge.galasse.dev — / and /coach).",
            Proof = "pipeview.galasse.dev · edge.galasse.dev",
            ProofEn = "pipeview.galasse.dev · edge.galasse.dev",
            DocsUrl = "https://edge.galasse.dev/",
        },
        new LabIndicator
        {
            Id = "github",
            Title = "GitHub Actions",
            Provider = "GitHub",
            Description =
                "CI/CD público no Pipeview — CI, preview, staging e production.",
            DescriptionEn =
                "Public CI/CD on Pipeview — CI, preview, staging, and production.",
            Proof = "Runs em dangalasse/pipeline-pulse/actions.",
            ProofEn = "Runs at dangalasse/pipeline-pulse/actions.",
            DocsUrl = "https://github.com/dangalasse/pipeline-pulse/actions",
        },
        new LabIndicator
        {
            Id = "aws",
            Title = "AWS Surface",
            Provider = "AWS",
            Description =
                "S3+CloudFront (static.galasse.dev) e o probe Always Free (Lambda Function URL + DynamoDB + EventBridge + KMS GenerateRandom).",
            DescriptionEn =
                "S3+CloudFront (static.galasse.dev) and the Always Free probe (Lambda Function URL + DynamoDB + EventBridge + KMS GenerateRandom).",
            Proof = "static.galasse.dev · Lambda Function URL /status",
            ProofEn = "static.galasse.dev · Lambda Function URL /status",
            DocsUrl = "https://4notqcazblkzqyd3avwjrkxtki0grnho.lambda-url.sa-east-1.on.aws/status",
        },
        new LabIndicator
        {
            Id = "observability",
            Title = "Observability",
            Provider = "Grafana · OTEL",
            Description =
                "No host: OpenTelemetry do ASP.NET → Alloy → Grafana Cloud. Node Exporter via Ansible. Grafana não é público.",
            DescriptionEn =
                "On the host: ASP.NET OpenTelemetry → Alloy → Grafana Cloud. Node Exporter via Ansible. Grafana is not public.",
            Proof = "Probe Edge Status nesta página · ansible/roles/observability",
            ProofEn = "Edge Status probe on this page · ansible/roles/observability",
            DocsUrl = "https://github.com/dangalasse/portfolio/tree/main/ansible/roles/observability",
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
