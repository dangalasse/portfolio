using Portfolio.Models;

namespace Portfolio.Data;

/// <summary>
/// Static portfolio content. Edit here or move to appsettings / Markdown later.
/// </summary>
public static class PortfolioCatalog
{
    public static SiteProfile Profile { get; } = new()
    {
        Name = "Danton Galasse",
        Role = "Infra · SRE & DevOps · Cloud",
        Tagline = "Infraestrutura de TI com foco em SRE, DevOps e IA — demos ao vivo com Cloudflare e AWS, sem teatro.",
        Location = "Brasil",
        Email = "contato@example.com",
        LinkedInUrl = "https://www.linkedin.com/in/danton-galasse",
        GitHubUrl = "https://github.com/dangalasse",
        SiteUrl = "https://portfolio.galasse.dev",
        CvPdfPath = "/files/cv.pdf",
    };

    public static IReadOnlyList<ProjectItem> Projects { get; } =
    [
        new ProjectItem
        {
            Slug = "tote",
            Title = "TOTE",
            Summary = "Plataforma de património com whitelabel, setup wizard e deploy zero-touch.",
            Description =
                "Sistema full-stack com bounded contexts, identidade visual configurável e operação em containers. Demonstra Nest/Next no produto e disciplina de domínio (SDD).",
            Stack = ["TypeScript", "Next.js", "NestJS", "PostgreSQL", "Docker", "Caddy", "AWS EC2"],
            Accent = "#3dd6c6",
            LiveUrl = "https://demo.tote.galasse.dev",
            RepoUrl = "https://github.com/dangalasse/TOTE",
            DemoNote = "Vitrine efémera em demo.tote.galasse.dev — workspace isolado por visitante.",
            Featured = true,
        },
        new ProjectItem
        {
            Slug = "edge-status",
            Title = "Edge Status Lab",
            Summary = "Worker Cloudflare que devolve região, cf-ray e latência — prova de edge real.",
            Description =
                "Mini-lab ligado a esta página Labs: o frontend TypeScript consulta um endpoint e mostra indicadores ao vivo. Até o Worker estar deployado, o probe ASP.NET responde localmente.",
            Stack = ["Cloudflare Workers", "TypeScript", "ASP.NET Core"],
            Accent = "#f6821f",
            LiveUrl = "/Labs",
            RepoUrl = "https://github.com/dangalasse/portfolio/tree/main/workers/edge-status",
            DemoNote = "Indicador ao vivo na página Labs.",
            Featured = true,
        },
        new ProjectItem
        {
            Slug = "aws-static-demo",
            Title = "AWS Static Demo",
            Summary = "Asset estático servido via S3 + CloudFront com diagrama e custo free-tier.",
            Description =
                "Fatia mínima de AWS para CV: bucket privado, distribuição CloudFront, URL pública read-only. Objectivo: prova de infra sem microserviços desnecessários.",
            Stack = ["AWS S3", "CloudFront", "CDK/CLI"],
            Accent = "#ff9900",
            LiveUrl = null,
            RepoUrl = null,
            DemoNote = "Ligar URL pública após o primeiro deploy AWS.",
            Featured = true,
        },
        new ProjectItem
        {
            Slug = "portfolio",
            Title = "Este portfólio",
            Summary = "ASP.NET Core Razor Pages + TypeScript + Tailwind — simples, rápido, demonstrável.",
            Description =
                "Site de CV com projectos, labs Cloudflare/AWS e links LinkedIn/GitHub. Build de assets via npm (esbuild + Tailwind v4), hospedado na EC2 atrás do Caddy com DNS Cloudflare.",
            Stack = ["ASP.NET Core 8", "TypeScript", "Tailwind CSS", "Caddy", "ECR"],
            Accent = "#9db0bb",
            LiveUrl = "https://portfolio.galasse.dev",
            RepoUrl = "https://github.com/dangalasse/portfolio",
            DemoNote = "Você está a ver a demo — ver fluxo de arquitectura abaixo.",
            Featured = false,
        },
    ];

    public static IReadOnlyList<LabIndicator> Labs { get; } =
    [
        new LabIndicator
        {
            Id = "cloudflare",
            Title = "Cloudflare Edge",
            Provider = "Cloudflare",
            Description = "Pages/DNS + Worker de status (região / ray / latência).",
            Proof = "Card com probe ao vivo nesta página.",
            DocsUrl = "https://developers.cloudflare.com/workers/",
        },
        new LabIndicator
        {
            Id = "aws",
            Title = "AWS Surface",
            Provider = "AWS",
            Description = "Demo mínima S3 + CloudFront (ou Lambda URL) com link público.",
            Proof = "URL + diagrama Mermaid no detalhe do projecto.",
            DocsUrl = "https://docs.aws.amazon.com/",
        },
        new LabIndicator
        {
            Id = "github",
            Title = "GitHub",
            Provider = "GitHub",
            Description = "Código-fonte deste site e repositórios dos projectos.",
            Proof = "Botão View source / perfil GitHub.",
            DocsUrl = Profile.GitHubUrl,
        },
    ];

    public static ProjectItem? FindProject(string slug) =>
        Projects.FirstOrDefault(p => p.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));
}
