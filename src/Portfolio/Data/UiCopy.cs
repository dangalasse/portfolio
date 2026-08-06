using Portfolio.I18n;

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
    public static readonly L SeeProjects = new("Ver projetos", "View projects");
    public static readonly L LiveLabs = new("Labs ao vivo", "Live labs");
    public static readonly L DownloadCv = new("Baixar CV", "Download CV");
    public static readonly L StackInUse = new("Stack em uso", "Stack in use");
    public static readonly L FeaturedProjects = new("Projetos em destaque", "Featured projects");
    public static readonly L FeaturedLead = new(
        "Demos com prova técnica — não só screenshots.",
        "Demos with technical proof — not just screenshots.");
    public static readonly L AllProjects = new("Todos os projetos →", "All projects →");
    public static readonly L Presence = new("Presença", "Presence");
    public static readonly L PresenceLead = new(
        "CV · LinkedIn · GitHub · este site.",
        "Resume · LinkedIn · GitHub · this site.");
    public static readonly L ProjectsTitle = new("Projetos", "Projects");
    public static readonly L ProjectsLead = new(
        "Produto, labs e demos com stack explícita e prova ao vivo quando existir.",
        "Product, labs, and demos with an explicit stack and live proof when available.");
    public static readonly L Context = new("Contexto", "Context");
    public static readonly L Stack = new("Stack", "Stack");
    public static readonly L LiveDemo = new("Demo ao vivo", "Live demo");
    public static readonly L ViewSource = new("Ver código", "View source");
    public static readonly L FlowByPlatform = new("Fluxo interativo", "Interactive flow");
    public static readonly L FlowHint = new(
        "Passe o rato ou clique num nó para ver o que faz — em linguagem simples e com um trecho de código.",
        "Hover or click a node to see what it does — in plain language plus a code snippet.");
    public static readonly L FlowExplain = new("Em palavras simples", "In plain words");
    public static readonly L FlowRecruiter = new("O que isto prova", "What this proves");
    public static readonly L FlowSnippet = new("Trecho de código", "Code snippet");
    public static readonly L FlowOpenRepo = new("Ver no repositório", "View in repository");
    public static readonly L FlowClose = new("Fechar", "Close");
    public static readonly L FlowCopy = new("Copiar", "Copy");
    public static readonly L FlowCopied = new("Copiado", "Copied");
    public static readonly L AboutTitle = new("Sobre", "About");
    public static readonly L Location = new("Localização", "Location");
    public static readonly L Contact = new("Contato", "Contact");
    public static readonly L LabsTitle = new("Labs", "Labs");
    public static readonly L LabsLead = new(
        "Provas ao vivo de edge e cloud — status atualiza no browser.",
        "Live edge and cloud proofs — status updates in the browser.");
    public static readonly L LabsHowTo = new(
        "Como ativar o Worker Cloudflare: faça deploy de workers/edge-status e defina EDGE_STATUS_URL.",
        "How to enable the Cloudflare Worker: deploy workers/edge-status and set EDGE_STATUS_URL.");
    public static readonly L Checking = new("Consultando…", "Checking…");
    public static readonly L LocalePtLabel = new("Português (Brasil)", "Portuguese (Brazil)");
    public static readonly L LocaleEnLabel = new("English (US)", "English (US)");
    public static readonly L SwitchLanguage = new("Idioma", "Language");

    public static string Resolve(L text, string locale) => text.Resolve(locale);
}
