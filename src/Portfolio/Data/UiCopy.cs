using Portfolio.I18n;
using Portfolio.Models;

namespace Portfolio.Data;

/// <summary>Chrome / nav / shared UI copy (pt-BR + en-US).</summary>
public static class UiCopy
{
    public static readonly L NavHome = new("Início", "Home");
    public static readonly L NavProjects = new("Projetos", "Projects");
    public static readonly L NavLabs = new("Labs", "Labs");
    public static readonly L NavAbout = new("Sobre", "About");
    public static readonly L NavMenu = new("Menu", "Menu");
    public static readonly L NavMainAria = new("Principal", "Primary");
    public static readonly L SeeProjects = new("Ver o produto e a prova", "See the product and the proof");
    public static readonly L LiveLabs = new("Labs (evidência)", "Labs (evidence)");
    public static readonly L DownloadCv = new("Baixar CV", "Download CV");
    public static readonly L StackInUse = new("Stack deste site", "Stack behind this site");
    public static readonly L FeaturedProjects = new("O que importa", "What matters");
    public static readonly L FeaturedLead = new(
        "Dois itens, de propósito. O resto é lab e fica em Labs.",
        "Two items, on purpose. Everything else is a lab and lives under Labs.");
    public static readonly L AllProjects = new("Lista completa, agrupada →", "Full list, grouped →");
    public static readonly L Presence = new("Onde me encontrar", "Where to find me");
    public static readonly L PresenceLead = new(
        "GitHub, LinkedIn e e-mail. CV só aparece quando o PDF estiver neste site.",
        "GitHub, LinkedIn, and email. CV only shows when the PDF is on this site.");
    public static readonly L KindProduct = new("Produto", "Product");
    public static readonly L KindProof = new("Prova DevOps", "DevOps proof");
    public static readonly L KindLab = new("Lab — evidência", "Lab — evidence");
    public static readonly L Problem = new("Problema", "Problem");
    public static readonly L HonestLimit = new("Limite honesto", "Honest limit");
    public static readonly L Verify = new("O que verificar", "What to verify");
    public static readonly L RecruiterMapTitle = new("Mapa em 5 minutos", "5-minute map");
    public static readonly L RecruiterMapLead = new(
        "Se você está filtrando a vaga, leia isto antes de clicar em tudo.",
        "If you are screening the role, read this before clicking everything.");
    public static readonly L LabsStripTitle = new("Labs", "Labs");
    public static readonly L LabsStripLead = new(
        "Always Free e edge. Evidência de que as superfícies existem — não o trabalho principal.",
        "Always Free and edge. Evidence the surfaces exist — not the main work.");
    public static readonly L ProbeAspnet = new("Probe ASP.NET (fallback)", "ASP.NET probe (fallback)");
    public static readonly L ProbeCloudflare = new("Worker Cloudflare", "Cloudflare Worker");
    public static readonly L GroupProduct = new("Produto", "Product");
    public static readonly L GroupProof = new("Prova", "Proof");
    public static readonly L GroupLabs = new("Labs", "Labs");
    public static readonly L SocialNavAria = new("Redes e perfil", "Social profiles");
    public static readonly L SocialHeroAria = new("Links rápidos", "Quick links");
    public static readonly L SocialFooterAria = new("Links no rodapé", "Footer links");

    public static readonly L MetaHome = new(
        "Danton Galasse — infraestrutura, SRE e DevOps. Um produto (TOTE), uma prova de esteira (Pipeview), labs como evidência.",
        "Danton Galasse — infrastructure, SRE, and DevOps. One product (TOTE), one CI/CD proof (Pipeview), labs as evidence.");
    public static readonly L MetaProjects = new(
        "TOTE (produto), Pipeview (prova CI/CD) e labs Always Free — separados de propósito.",
        "TOTE (product), Pipeview (CI/CD proof), and Always Free labs — separated on purpose.");
    public static readonly L MetaLabs = new(
        "Labs: probes, S3+OAC e um playground de inferência. Evidência, não o cargo.",
        "Labs: probes, S3+OAC, and an inference playground. Evidence, not the job.");
    public static readonly L MetaAbout = new(
        "Sobre Danton Galasse — o que este site é, o que não é, e como revisar em 5 minutos.",
        "About Danton Galasse — what this site is, what it is not, and how to review in 5 minutes.");
    public static readonly L MetaOps = new(
        "AWS Ops Labs em tabela: último ping de cada superfície. JSON cru no anexo.",
        "AWS Ops Labs as a table: last ping per surface. Raw JSON in the appendix.");
    public static readonly L MetaStatic = new(
        "O que static.galasse.dev prova: S3 privado, CloudFront OAC, Terraform.",
        "What static.galasse.dev proves: private S3, CloudFront OAC, Terraform.");
    public static readonly L ProjectsTitle = new("Projetos", "Projects");
    public static readonly L ProjectsLead = new(
        "Agrupado: produto, prova, labs. Labs não competem com o produto na mesma prateleira.",
        "Grouped: product, proof, labs. Labs do not sit on the same shelf as the product.");
    public static readonly L Context = new("Contexto", "Context");
    public static readonly L Stack = new("Stack", "Stack");
    public static readonly L LiveDemo = new("Demo ao vivo", "Live demo");
    public static readonly L TryLive = new("Explorar ao vivo", "Explore live");
    public static readonly L ViewSource = new("Ver o código", "View the code");
    public static readonly L FlowByPlatform = new("Fluxo interativo", "Interactive flow");
    public static readonly L FlowHint = new(
        "Hover ou clique num nó para ver o que faz — em linguagem simples, com um snippet de código.",
        "Hover or click a node to see what it does — plain language, plus a short code snippet.");
    public static readonly L FlowExplain = new("Em palavras simples", "In plain words");
    public static readonly L FlowRecruiter = new("O que isto ilustra", "What this illustrates");
    public static readonly L FlowSnippet = new("Snippet de código", "Code snippet");
    public static readonly L FlowOpenRepo = new("Abrir no repositório", "Open in the repository");
    public static readonly L FlowClose = new("Fechar", "Close");
    public static readonly L FlowCopy = new("Copiar", "Copy");
    public static readonly L FlowCopied = new("Copiado", "Copied");
    public static readonly L AboutTitle = new("Sobre", "About");
    public static readonly L Location = new("Localização", "Location");
    public static readonly L Contact = new("Contato", "Contact");
    public static readonly L RecruiterStrip = new(
        "Mapa no próprio site — não no GitHub:",
        "The map lives on this site — not on GitHub:");
    public static readonly L LabsTitle = new("Labs", "Labs");
    public static readonly L LabsLead = new(
        "Evidência de superfícies. Nenhum card desta página é o produto.",
        "Surface evidence. No card on this page is the product.");
    public static readonly L LabsHowTo = new(
        "O pill acima diz a origem do probe. Cloudflare = Worker. ASP.NET = fallback deste host.",
        "The pill above states the probe origin. Cloudflare = Worker. ASP.NET = this host's fallback.");
    public static readonly L LabsObservabilityNote = new(
        "Observabilidade: OTEL → Alloy → Grafana Cloud no host. Grafana não é público. A prova visível daqui é o pill e a tabela em /Labs/Ops.",
        "Observability: OTEL → Alloy → Grafana Cloud on the host. Grafana is not public. Visible proof from here is the pill and the table at /Labs/Ops.");
    public static readonly L LabsProbeLive = new(
        "Worker Edge Status ao vivo",
        "Edge Status Worker live");
    public static readonly L LabsProbeFallback = new(
        "Fallback ASP.NET no ar",
        "ASP.NET fallback live");
    public static readonly L RawJson = new("JSON cru", "Raw JSON");
    public static readonly L RawHtml = new("HTML no CloudFront", "HTML on CloudFront");
    public static readonly L OpsTitle = new("AWS Ops Labs", "AWS Ops Labs");
    public static readonly L OpsLead = new(
        "A Lambda pinga as superfícies a cada 5 minutos. Abaixo está a última leitura, em tabela. Isto não é um dashboard de produção.",
        "Lambda pings the surfaces every 5 minutes. Below is the latest reading, as a table. This is not a production dashboard.");
    public static readonly L StaticTitle = new("AWS Static Demo", "AWS Static Demo");
    public static readonly L StaticLead = new(
        "Se static.galasse.dev abre, o caminho Terraform fechou: bucket privado, OAC, CloudFront, ACM. A página em si é um parágrafo de prova — não um produto.",
        "If static.galasse.dev loads, the Terraform path closed: private bucket, OAC, CloudFront, ACM. The page itself is a proof paragraph — not a product.");
    public static readonly L OpenRaw = new("Abrir a superfície crua", "Open the raw surface");
    public static readonly L FetchFailed = new(
        "Não consegui puxar o JSON agora. O endpoint público continua no link abaixo.",
        "Could not fetch the JSON just now. The public endpoint is still in the link below.");
    public static readonly L Checking = new("Verificando…", "Checking…");
    public static readonly L LocalePtLabel = new("Português (Brasil)", "Portuguese (Brazil)");
    public static readonly L LocaleEnLabel = new("English (US)", "English (US)");
    public static readonly L SwitchLanguage = new("Idioma", "Language");

    public static L ForKind(ProjectKind kind) => kind switch
    {
        ProjectKind.Product => KindProduct,
        ProjectKind.Proof => KindProof,
        _ => KindLab,
    };

    public static string Resolve(L text, string locale) => text.Resolve(locale);
}
