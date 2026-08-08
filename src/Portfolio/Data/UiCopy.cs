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
    public static readonly L SeeProjects = new("Explorar projetos", "Explore projects");
    public static readonly L LiveLabs = new("Conhecer os labs", "Visit the labs");
    public static readonly L DownloadCv = new("Descarregar CV", "Download CV");
    public static readonly L StackInUse = new("Stack deste site", "Stack behind this site");
    public static readonly L FeaturedProjects = new("Projetos em destaque", "Featured work");
    public static readonly L FeaturedLead = new(
        "Uma seleção com demos públicas — sinta-se à vontade para explorar com calma.",
        "A short selection with public demos — feel free to explore at your own pace.");
    public static readonly L AllProjects = new("Ver todos os projetos →", "See all projects →");
    public static readonly L Presence = new("Onde me encontrar", "Where to find me");
    public static readonly L PresenceLead = new(
        "CV, LinkedIn, GitHub e este site.",
        "Resume, LinkedIn, GitHub, and this site.");
    public static readonly L ProjectsTitle = new("Projetos", "Projects");
    public static readonly L ProjectsLead = new(
        "Produto, labs e demos — com a stack à vista e, quando possível, um ambiente ao vivo.",
        "Product work, labs, and demos — stack listed, with a live surface when one exists.");
    public static readonly L Context = new("Contexto", "Context");
    public static readonly L Stack = new("Stack", "Stack");
    public static readonly L LiveDemo = new("Demo ao vivo", "Live demo");
    public static readonly L TryLive = new("Explorar ao vivo", "Explore live");
    public static readonly L ViewSource = new("Ver o código", "View the code");
    public static readonly L FlowByPlatform = new("Fluxo interativo", "Interactive flow");
    public static readonly L FlowHint = new(
        "Passe o rato ou clique num nó para ver o que faz — em linguagem simples, com um trecho de código.",
        "Hover or click a node to see what it does — plain language, plus a short code snippet.");
    public static readonly L FlowExplain = new("Em palavras simples", "In plain words");
    public static readonly L FlowRecruiter = new("O que isto ilustra", "What this illustrates");
    public static readonly L FlowSnippet = new("Trecho de código", "Code snippet");
    public static readonly L FlowOpenRepo = new("Abrir no repositório", "Open in the repository");
    public static readonly L FlowClose = new("Fechar", "Close");
    public static readonly L FlowCopy = new("Copiar", "Copy");
    public static readonly L FlowCopied = new("Copiado", "Copied");
    public static readonly L AboutTitle = new("Sobre", "About");
    public static readonly L Location = new("Localização", "Location");
    public static readonly L Contact = new("Contacto", "Contact");
    public static readonly L RecruiterStrip = new(
        "Se tiver uns minutos, este mapa pode ajudar a navegar:",
        "If you have a few minutes, this map can help you find your way:");
    public static readonly L LabsTitle = new("Labs", "Labs");
    public static readonly L LabsLead = new(
        "Superfícies de edge e cloud ligadas a este site. O indicador no topo atualiza sozinho.",
        "Edge and cloud surfaces wired into this site. The status pill at the top updates on its own.");
    public static readonly L LabsHowTo = new(
        "Para ligar o Worker Cloudflare: faça deploy de workers/edge-status e defina EDGE_STATUS_URL no contentor.",
        "To enable the Cloudflare Worker: deploy workers/edge-status and set EDGE_STATUS_URL on the container.");
    public static readonly L LabsObservabilityNote = new(
        "Observabilidade: traces e métricas seguem OTEL → Alloy → Grafana Cloud no host. O Grafana em si não é público — o probe Edge Status nesta página é a prova que se pode ver daqui.",
        "Observability: traces and metrics go OTEL → Alloy → Grafana Cloud on the host. Grafana itself is not public — the Edge Status probe on this page is the proof you can see from here.");
    public static readonly L Checking = new("A verificar…", "Checking…");
    public static readonly L LocalePtLabel = new("Português (Brasil)", "Portuguese (Brazil)");
    public static readonly L LocaleEnLabel = new("English (US)", "English (US)");
    public static readonly L SwitchLanguage = new("Idioma", "Language");

    public static string Resolve(L text, string locale) => text.Resolve(locale);
}
