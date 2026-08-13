using Portfolio.I18n;
using Portfolio.Models;

namespace Portfolio.Data;

/// <summary>
/// Static portfolio content (pt-BR + en-US). Resolve with <see cref="For"/>.
/// Featured is curated: product + one DevOps proof. Labs stay in /Labs.
/// </summary>
public static class PortfolioCatalog
{
    public static SiteProfile ProfileBase { get; } = new()
    {
        Name = "Danton Galasse",
        Role = "Infra · SRE & DevOps · Cloud",
        TaglinePtBr =
            "Infraestrutura de TI, 5 anos. O trabalho principal é operar e entregar sistemas — não colecionar tags. Este site tem um produto (TOTE), uma prova de esteira (Pipeview) e labs Always Free como evidência, não como o cargo.",
        TaglineEnUs =
            "IT infrastructure, 5 years. The actual job is running and shipping systems — not collecting tags. This site has one product (TOTE), one CI/CD proof (Pipeview), and Always Free labs as evidence, not as the role.",
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
            Kind = ProjectKind.Product,
            Summary =
                "Sistema de inventário de ativos de TI: patrimônio, custódia, auditoria e whitelabel.",
            Description =
                "Produto full-stack (Next.js + NestJS + PostgreSQL) com bounded contexts, schema EAV, RBAC e deploy por Docker Compose na EC2. Existe um chart Helm em k8s/tote-chart/ — é demonstrativo. Produção neste host é Compose, não Kubernetes.",
            SummaryEn =
                "IT asset inventory: patrimony, custody, audit, and whitelabel.",
            DescriptionEn =
                "Full-stack product (Next.js + NestJS + PostgreSQL) with bounded contexts, EAV schema, RBAC, and Docker Compose on EC2. There is a Helm chart under k8s/tote-chart/ — it is demonstrative. Production on this host is Compose, not Kubernetes.",
            Problem =
                "Patrimônio de TI espalhado em planilha, sem trilha de quem mudou o quê, e onboarding de cliente que exige logo/cores sem rebuild.",
            ProblemEn =
                "IT assets living in spreadsheets, no audit trail, and customer onboarding that needs branding without a rebuild.",
            HonestLimit =
                "A porta é o Turnstile. Marque o checkbox: o bootstrap cria um workspace ADMIN efêmero (~2h) e autentica sozinho, sem senha. A tela de login que aparece antes disso é o produto, não o portão.",
            HonestLimitEn =
                "The gate is Turnstile. Check the box: bootstrap mints an ephemeral ADMIN workspace (~2h) and signs you in — no password. The login screen before that is the product, not the lock.",
            VerifyHint =
                "demo.tote.galasse.dev → checkbox humano → dashboard. Experimente Colunas, Integridade EAV, Identidade. Relatórios/usuários/aprovações ficam ocultos de propósito.",
            VerifyHintEn =
                "demo.tote.galasse.dev → human checkbox → dashboard. Try Columns, EAV integrity, Identity. Reports/users/approvals stay hidden on purpose.",
            LiveCta = "Abrir a vitrine (Turnstile)",
            LiveCtaEn = "Open the showcase (Turnstile)",
            Stack =
            [
                "TypeScript",
                "Next.js",
                "NestJS",
                "PostgreSQL",
                "Docker",
                "Caddy",
                "AWS EC2",
            ],
            Accent = "#3dd6c6",
            LiveUrl = "https://demo.tote.galasse.dev",
            RepoUrl = "https://github.com/dangalasse/TOTE",
            DemoNote =
                "demo.tote.galasse.dev — checkbox do Turnstile, depois sessão efêmera. Sem senha.",
            DemoNoteEn =
                "demo.tote.galasse.dev — Turnstile checkbox, then an ephemeral session. No password.",
            Featured = true,
        },
        new ProjectItem
        {
            Slug = "pipeview",
            Title = "Pipeview",
            Kind = ProjectKind.Proof,
            Summary =
                "Painel da esteira real: GitHub Actions → Cloudflare Workers, com SHA, ambiente e o run que publicou.",
            Description =
                "Prova DevOps, não um produto. Lint, typecheck, testes, preview de PR, staging com smoke em /api/health, production por tag. A UI mostra o deploy atual. O último run da demo pode estar vermelho — é o GitHub de verdade, não um print verde.",
            SummaryEn =
                "Dashboard of the real conveyor: GitHub Actions → Cloudflare Workers, with SHA, environment, and the run that shipped.",
            DescriptionEn =
                "DevOps proof, not a product. Lint, typecheck, tests, PR preview, staging smoke on /api/health, production via tag. The UI shows the current deploy. The last demo run may be red — that is real GitHub, not a green screenshot.",
            Problem =
                "Portfólio DevOps que só mostra YAML. Aqui a esteira está no ar, com o SHA do build visível.",
            ProblemEn =
                "Most DevOps portfolios show YAML. This one shows the live conveyor and the SHA that shipped.",
            HonestLimit =
                "A esteira é o último run de live-demo.yml no GitHub — inclusive se o lint quebrar. O card de cima (SHA / PRODUCTION) é o que está publicado. São duas coisas.",
            HonestLimitEn =
                "The belt is the last live-demo.yml run on GitHub — including a lint failure. The card above (SHA / PRODUCTION) is what is published. Those are two different things.",
            VerifyHint =
                "Card de cima: SHA e workflow que publicaram. Esteira: último live-demo. Se a esteira estiver vermelha, abra o run no GitHub — é o job real.",
            VerifyHintEn =
                "Top card: SHA and the workflow that shipped. Belt: last live-demo. If the belt is red, open the GitHub run — that is the real job.",
            LiveCta = "Abrir o painel (run real)",
            LiveCtaEn = "Open the dashboard (real run)",
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
                "pipeview.galasse.dev — staging em staging.pipeview.galasse.dev. O run pode estar vermelho.",
            DemoNoteEn =
                "pipeview.galasse.dev — staging at staging.pipeview.galasse.dev. The run may be red.",
            Featured = true,
        },
        new ProjectItem
        {
            Slug = "edge-status",
            Title = "Edge Status Lab",
            Kind = ProjectKind.Lab,
            Summary =
                "Worker Cloudflare que devolve região e cf-ray. Evidência de probe, não um produto.",
            Description =
                "Lab mínimo: o frontend consulta o Worker e mostra região/latência. Se o container não tiver a URL, cai no probe ASP.NET deste site. Os dois estados são honestos — o pill não finge Cloudflare quando a origem é local.",
            SummaryEn =
                "Cloudflare Worker that returns region and cf-ray. Probe evidence, not a product.",
            DescriptionEn =
                "Minimal lab: the frontend hits the Worker and shows region/latency. If the container has no URL, it falls back to this site's ASP.NET probe. Both states are honest — the pill does not pretend to be Cloudflare when the origin is local.",
            HonestLimit =
                "Isto prova um GET na edge. Não prova SRE.",
            HonestLimitEn =
                "This proves a GET at the edge. It does not prove SRE.",
            VerifyHint =
                "Olhe o pill no topo. Se disser Cloudflare, o Worker respondeu. Se disser probe ASP.NET, o fallback está no ar.",
            VerifyHintEn =
                "Look at the status pill. Cloudflare means the Worker answered. ASP.NET probe means the fallback is live.",
            Stack = ["Cloudflare Workers", "TypeScript", "ASP.NET Core"],
            Accent = "#f6821f",
            LiveUrl = "/Labs",
            RepoUrl = "https://github.com/dangalasse/portfolio/tree/main/workers/edge-status",
            DemoNote = "Indicador no topo desta página e em /Labs.",
            DemoNoteEn = "Status pill on this page and on /Labs.",
            Featured = false,
        },
        new ProjectItem
        {
            Slug = "aws-static-demo",
            Title = "AWS Static Demo",
            Kind = ProjectKind.Lab,
            Summary =
                "Página estática atrás de S3 privado + CloudFront OAC, via Terraform. Lab, não produto.",
            Description =
                "Fatia mínima de AWS: bucket sem acesso público, Origin Access Control, CloudFront PriceClass_100, ACM. Se a página abre, DNS + TLS + OAC fecharam. Não há app por trás — é o HTML que o IaC publica.",
            SummaryEn =
                "Static page behind private S3 + CloudFront OAC, via Terraform. A lab, not a product.",
            DescriptionEn =
                "Minimal AWS slice: no public bucket access, Origin Access Control, CloudFront PriceClass_100, ACM. If the page loads, DNS + TLS + OAC closed. There is no app behind it — it is the HTML the IaC publishes.",
            HonestLimit =
                "Dois parágrafos no ar. A prova é o caminho IaC, não a UI. Leia a página humana em /Labs/Static antes do HTML cru.",
            HonestLimitEn =
                "Two paragraphs on the wire. The proof is the IaC path, not the UI. Read the human page at /Labs/Static before the raw HTML.",
            VerifyHint =
                "Terraform no repo aws-static-demo: block_public_acls, OAC, PriceClass_100.",
            VerifyHintEn =
                "Terraform in aws-static-demo: block_public_acls, OAC, PriceClass_100.",
            LiveCta = "Ver a explicação",
            LiveCtaEn = "Read the explanation",
            Stack = ["AWS S3", "CloudFront", "Terraform", "OAC"],
            Accent = "#ff9900",
            LiveUrl = "/Labs/Static",
            RepoUrl = "https://github.com/dangalasse/aws-static-demo",
            DemoNote =
                "HTML cru em static.galasse.dev — a página deste site traduz o que aquilo prova.",
            DemoNoteEn =
                "Raw HTML at static.galasse.dev — this site's page translates what that proves.",
            Featured = false,
        },
        new ProjectItem
        {
            Slug = "aws-ops-labs",
            Title = "AWS Ops Labs",
            Kind = ProjectKind.Lab,
            Summary =
                "Lambda que pinga as superfícies ao vivo a cada 5 minutos e grava DynamoDB. JSON, não dashboard.",
            Description =
                "Lab Always Free: Function URL (sem API Gateway), DynamoDB PAY_PER_REQUEST com TTL 7d, EventBridge rate(5 minutes), KMS GenerateRandom sem CMK. GET /status é JSON cru. A página /Labs/Ops traduz isso para humano.",
            SummaryEn =
                "Lambda that pings live surfaces every 5 minutes and writes DynamoDB. JSON, not a dashboard.",
            DescriptionEn =
                "Always Free lab: Function URL (no API Gateway), DynamoDB PAY_PER_REQUEST with 7-day TTL, EventBridge rate(5 minutes), KMS GenerateRandom with no CMK. GET /status is raw JSON. /Labs/Ops translates it for a human.",
            HonestLimit =
                "Não é monitoramento de produção. É um probe Free Tier. Grafana deste portfólio continua privado.",
            HonestLimitEn =
                "This is not production monitoring. It is a Free Tier probe. Grafana for this portfolio stays private.",
            VerifyHint =
                "Abra /Labs/Ops: tabela de labs, HTTP, latência, último check. JSON cru no link.",
            VerifyHintEn =
                "Open /Labs/Ops: lab table, HTTP, latency, last check. Raw JSON is linked.",
            LiveCta = "Ver o status em tabela",
            LiveCtaEn = "See status as a table",
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
            LiveUrl = "/Labs/Ops",
            RepoUrl = "https://github.com/dangalasse/portfolio/tree/main/labs/always-free",
            DemoNote =
                "Function URL pública continua no ar; esta página é a leitura humana.",
            DemoNoteEn =
                "The public Function URL is still live; this page is the human reading.",
            Featured = false,
        },
        new ProjectItem
        {
            Slug = "edge-labs",
            Title = "Edge Labs",
            Kind = ProjectKind.Lab,
            Summary =
                "Playground de inferência no Workers AI: cola um erro, recebe JSON. Não é LLMOps de produção.",
            Description =
                "Worker Cloudflare chama Llama (fp8) no Free Tier e devolve summary, likelyCause, suggestedFix, provider, model, analyzedAt. Gemini só entra se o secret existir — hoje não está configurado, então não está na stack. Abas SDD/DDD/TDD são prompts, não disciplinas implementadas.",
            SummaryEn =
                "Workers AI inference playground: paste an error, get JSON. Not production LLMOps.",
            DescriptionEn =
                "A Cloudflare Worker calls Llama (fp8) on the Free Tier and returns summary, likelyCause, suggestedFix, provider, model, analyzedAt. Gemini only appears if the secret exists — it is not configured today, so it is not on the stack. SDD/DDD/TDD tabs are prompts, not implemented disciplines.",
            HonestLimit =
                "Isto prova que uma inferência rodou na edge. Não prova treino, eval, RAG, agente ou operação de modelo.",
            HonestLimitEn =
                "This proves one inference ran at the edge. It does not prove training, evals, RAG, agents, or model ops.",
            VerifyHint =
                "edge.galasse.dev — complete o Turnstile, analise, confira provider/model/analyzedAt. /health diz workers-ai.",
            VerifyHintEn =
                "edge.galasse.dev — complete Turnstile, analyze, check provider/model/analyzedAt. /health says workers-ai.",
            LiveCta = "Abrir o playground",
            LiveCtaEn = "Open the playground",
            Stack = ["Cloudflare Workers", "Workers AI", "TypeScript", "Wrangler"],
            Accent = "#8b5cf6",
            LiveUrl = "https://edge.galasse.dev/",
            RepoUrl = "https://github.com/dangalasse/edge-labs",
            DemoNote =
                "Turnstile na frente. Gemini não está ligado. O JSON muda analyzedAt a cada chamada.",
            DemoNoteEn =
                "Turnstile in front. Gemini is not on. analyzedAt changes every call.",
            Featured = false,
        },
        new ProjectItem
        {
            Slug = "portfolio",
            Title = "Este portfólio",
            TitleEn = "This portfolio",
            Kind = ProjectKind.Lab,
            Summary =
                "O site que você está lendo: ASP.NET Core, Caddy, ECR, Ansible. É o veículo, não o produto.",
            Description =
                "Razor Pages + TypeScript + Tailwind na EC2 atrás do Caddy. CI: Actions → ECR → Ansible. Observabilidade: OTEL → Alloy → Grafana Cloud (Grafana não é público). Este projeto existe para hospedar o CV e os labs.",
            SummaryEn =
                "The site you are reading: ASP.NET Core, Caddy, ECR, Ansible. The vehicle, not the product.",
            DescriptionEn =
                "Razor Pages + TypeScript + Tailwind on EC2 behind Caddy. CI: Actions → ECR → Ansible. Observability: OTEL → Alloy → Grafana Cloud (Grafana is not public). This project exists to host the resume and the labs.",
            HonestLimit =
                "Não é o produto. É o site do CV. ASP.NET está aqui porque este host já opera .NET — não porque o TOTE seja .NET.",
            HonestLimitEn =
                "Not the product. The resume site. ASP.NET is here because this host already runs .NET — not because TOTE is .NET.",
            VerifyHint =
                "ansible/ no repo, workflow deploy.yml, Dockerfile. O fluxo de arquitetura está abaixo se você quiser o desenho.",
            VerifyHintEn =
                "ansible/ in the repo, deploy.yml workflow, Dockerfile. The architecture flow is below if you want the drawing.",
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
            DemoNote = "Você já está na demo.",
            DemoNoteEn = "You are already on the demo.",
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
                "Workers e DNS. Pipeview é a prova de esteira. Edge Labs é um playground de inferência — não LLMOps de produção.",
            DescriptionEn =
                "Workers and DNS. Pipeview is the conveyor proof. Edge Labs is an inference playground — not production LLMOps.",
            Proof = "pipeview.galasse.dev · edge.galasse.dev",
            ProofEn = "pipeview.galasse.dev · edge.galasse.dev",
            DocsUrl = "https://pipeview.galasse.dev",
        },
        new LabIndicator
        {
            Id = "github",
            Title = "GitHub Actions",
            Provider = "GitHub",
            Description =
                "CI/CD público no Pipeview: CI, preview, staging, production. O run visível pode estar vermelho.",
            DescriptionEn =
                "Public CI/CD on Pipeview: CI, preview, staging, production. The visible run may be red.",
            Proof = "dangalasse/pipeline-pulse/actions",
            ProofEn = "dangalasse/pipeline-pulse/actions",
            DocsUrl = "https://github.com/dangalasse/pipeline-pulse/actions",
        },
        new LabIndicator
        {
            Id = "aws",
            Title = "AWS Surface",
            Provider = "AWS",
            Description =
                "S3+CloudFront (OAC) e o probe Lambda. Páginas humanas em /Labs/Static e /Labs/Ops — o HTML/JSON crus são o anexo.",
            DescriptionEn =
                "S3+CloudFront (OAC) and the Lambda probe. Human pages at /Labs/Static and /Labs/Ops — raw HTML/JSON are the appendix.",
            Proof = "/Labs/Static · /Labs/Ops",
            ProofEn = "/Labs/Static · /Labs/Ops",
            DocsUrl = "/Labs/Ops",
        },
        new LabIndicator
        {
            Id = "observability",
            Title = "Observability",
            Provider = "Grafana · OTEL",
            Description =
                "OTEL do ASP.NET → Alloy → Grafana Cloud no host. Grafana não é público. A prova visível é o probe desta página e /Labs/Ops.",
            DescriptionEn =
                "ASP.NET OTEL → Alloy → Grafana Cloud on the host. Grafana is not public. Visible proof is this page's probe and /Labs/Ops.",
            Proof = "Probe nesta página · ansible/roles/observability",
            ProofEn = "Probe on this page · ansible/roles/observability",
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
                Problem = en ? (p.ProblemEn ?? p.Problem) : p.Problem,
                HonestLimit = en ? (p.HonestLimitEn ?? p.HonestLimit) : p.HonestLimit,
                VerifyHint = en ? (p.VerifyHintEn ?? p.VerifyHint) : p.VerifyHint,
                LiveCta = en ? (p.LiveCtaEn ?? p.LiveCta) : p.LiveCta,
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
