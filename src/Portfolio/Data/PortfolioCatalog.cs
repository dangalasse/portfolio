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
                "Sistema full-stack com bounded contexts, identidade visual configurável e operação em containers. Demonstra Nest/Next no produto e disciplina de domínio (SDD).",
            SummaryEn =
                "Asset management platform with whitelabel branding, setup wizard, and zero-touch deploy.",
            DescriptionEn =
                "Full-stack system with bounded contexts, configurable visual identity, and container operations. Shows Nest/Next in product and domain discipline (SDD).",
            Stack = ["TypeScript", "Next.js", "NestJS", "PostgreSQL", "Docker", "Caddy", "AWS EC2"],
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
                "Asset estático servido via S3 + CloudFront com diagrama e custo free-tier.",
            Description =
                "Fatia mínima de AWS para CV: bucket privado, distribuição CloudFront, URL pública read-only. Objetivo: prova de infra sem microserviços desnecessários.",
            SummaryEn =
                "Static asset served via S3 + CloudFront with a diagram and free-tier cost notes.",
            DescriptionEn =
                "Minimal AWS slice for a resume: private bucket, CloudFront distribution, read-only public URL. Goal: infra proof without unnecessary microservices.",
            Stack = ["AWS S3", "CloudFront", "CDK/CLI"],
            Accent = "#ff9900",
            LiveUrl = null,
            RepoUrl = null,
            DemoNote = "Ligar URL pública após o primeiro deploy AWS.",
            DemoNoteEn = "Attach a public URL after the first AWS deploy.",
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
                "Site de CV com projetos, labs Cloudflare/AWS e links LinkedIn/GitHub. Build de assets via npm (esbuild + Tailwind v4), hospedado na EC2 atrás do Caddy com DNS Cloudflare.",
            SummaryEn =
                "ASP.NET Core Razor Pages + TypeScript + Tailwind — simple, fast, demonstrable.",
            DescriptionEn =
                "Resume site with projects, Cloudflare/AWS labs, and LinkedIn/GitHub links. Asset build via npm (esbuild + Tailwind v4), hosted on EC2 behind Caddy with Cloudflare DNS.",
            Stack = ["ASP.NET Core 8", "TypeScript", "Tailwind CSS", "Caddy", "ECR"],
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
            Description = "Pages/DNS + Worker de status (região / ray / latência).",
            DescriptionEn = "Pages/DNS + status Worker (region / ray / latency).",
            Proof = "Card com probe ao vivo nesta página.",
            ProofEn = "Card with a live probe on this page.",
            DocsUrl = "https://developers.cloudflare.com/workers/",
        },
        new LabIndicator
        {
            Id = "aws",
            Title = "AWS Surface",
            Provider = "AWS",
            Description = "Demo mínima S3 + CloudFront (ou Lambda URL) com link público.",
            DescriptionEn = "Minimal S3 + CloudFront demo (or Lambda URL) with a public link.",
            Proof = "URL + diagrama no detalhe do projeto.",
            ProofEn = "URL + diagram on the project detail page.",
            DocsUrl = "https://docs.aws.amazon.com/",
        },
        new LabIndicator
        {
            Id = "github",
            Title = "GitHub",
            Provider = "GitHub",
            Description = "Código-fonte deste site e repositórios dos projetos.",
            DescriptionEn = "Source code for this site and project repositories.",
            Proof = "Botão Ver código / perfil GitHub.",
            ProofEn = "View source button / GitHub profile.",
            DocsUrl = ProfileBase.GitHubUrl,
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
