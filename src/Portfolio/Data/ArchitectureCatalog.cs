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
                    Column = n.Column,
                    Row = n.Row,
                    PlainExplain = en ? n.PlainExplainEn : n.PlainExplainPt,
                    RecruiterDetail = en ? n.RecruiterDetailEn : n.RecruiterDetailPt,
                    CodeSnippet = n.CodeSnippet,
                    RepoUrl = n.RepoUrl,
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
        public int Column { get; init; }
        public int Row { get; init; }
        public string PlainExplainPt { get; init; } = "";
        public string PlainExplainEn { get; init; } = "";
        public string RecruiterDetailPt { get; init; } = "";
        public string RecruiterDetailEn { get; init; } = "";
        public string CodeSnippet { get; init; } = "";
        public string? RepoUrl { get; init; }
    }

    private static NodeDef N(
        string id,
        string label,
        string subtitlePt,
        string subtitleEn,
        string icon,
        string color,
        string layer,
        int column,
        int row,
        string plainPt,
        string plainEn,
        string recruitPt,
        string recruitEn,
        string snippet,
        string? repoUrl = null) =>
        new()
        {
            Id = id,
            Label = label,
            SubtitlePt = subtitlePt,
            SubtitleEn = subtitleEn,
            Icon = icon,
            Color = color,
            Layer = layer,
            Column = column,
            Row = row,
            PlainExplainPt = plainPt,
            PlainExplainEn = plainEn,
            RecruiterDetailPt = recruitPt,
            RecruiterDetailEn = recruitEn,
            CodeSnippet = snippet,
            RepoUrl = repoUrl,
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
            CaptionPt = "Do browser ao banco: DNS, TLS, app e dados — orquestrado com Docker Compose.",
            CaptionEn = "From browser to database: DNS, TLS, app, and data — orchestrated with Docker Compose.",
            LayerOrder = ["client", "edge", "gateway", "app", "data"],
            Nodes =
            [
                N("browser", "Browser", "Usuário", "User", "googlechrome", "#e8eef1", "client", 0, 0,
                    "A pessoa abre o sistema no navegador.",
                    "Someone opens the system in a browser.",
                    "Entrada do produto no browser.",
                    "Product entry point in the browser.",
                    "GET https://tote.galasse.dev/"),
                N("cf", "Cloudflare DNS", "tote.galasse.dev", "tote.galasse.dev", "cloudflare", "#f6821f", "edge", 1, 0,
                    "O nome do site aponta para o servidor certo.",
                    "The domain name points to the right server.",
                    "DNS na Cloudflare (Free Tier).",
                    "DNS on Cloudflare (Free Tier).",
                    "type = A\nname = tote\nproxied = false"),
                N("ec2", "AWS EC2", "Elastic IP · Compose", "Elastic IP · Compose", "amazonec2", "#ff9900", "edge", 1, 1,
                    "Uma máquina na AWS hospeda os containers.",
                    "One AWS machine hosts the containers.",
                    "Compute Free Tier com IP fixo.",
                    "Free Tier compute with a fixed IP.",
                    "aws ec2 describe-instances \\\n  --instance-ids i-…"),
                N("caddy", "Caddy", "TLS Let's Encrypt", "TLS Let's Encrypt", "nginx", "#3dd6c6", "gateway", 2, 0,
                    "Caddy termina o HTTPS e encaminha o tráfego.",
                    "Caddy terminates HTTPS and proxies traffic.",
                    "TLS com renovação automática.",
                    "TLS with automatic renewal.",
                    "tote.galasse.dev {\n  reverse_proxy frontend:3000\n}"),
                N("docker", "Docker", "Compose full", "Compose full", "docker", "#2496ed", "gateway", 2, 1,
                    "Um comando sobe a pilha inteira.",
                    "One command brings the whole stack up.",
                    "Compose para local e produção.",
                    "Compose for local and production.",
                    "COMPOSE_PROFILES=full \\\n  docker compose up -d"),
                N("next", "Next.js", "Frontend :3000", "Frontend :3000", "nextdotjs", "#e8eef1", "app", 3, 0,
                    "A interface do inventário — telas e formulários.",
                    "The inventory UI — screens and forms.",
                    "Frontend com App Router.",
                    "Frontend with App Router.",
                    "app/(dashboard)/assets/page.tsx",
                    "https://github.com/dangalasse/TOTE"),
                N("nest", "NestJS", "API /v1 :3001", "API /v1 :3001", "nestjs", "#e0234e", "app", 3, 1,
                    "A API aplica regras de negócio e autenticação.",
                    "The API enforces business rules and auth.",
                    "Backend Nest modular, alinhado a DDD.",
                    "Modular Nest backend, DDD-friendly.",
                    "@Controller('assets')\nexport class AssetsController {}",
                    "https://github.com/dangalasse/TOTE"),
                N("pg", "PostgreSQL", "Prisma ORM", "Prisma ORM", "postgresql", "#4169e1", "data", 4, 0,
                    "Onde ficam ativos e usuários.",
                    "Where assets and users live.",
                    "Postgres com migrações Prisma.",
                    "Postgres with Prisma migrations.",
                    "model Asset {\n  id String @id\n}"),
                N("redis", "Redis", "Cache / filas", "Cache / queues", "redis", "#dc382d", "data", 4, 1,
                    "Cache e apoio a trabalhos em fila.",
                    "Cache and backing for queue jobs.",
                    "Sessão/cache ao lado da API.",
                    "Session/cache next to the API.",
                    "SET session:abc … EX 3600"),
            ],
            Edges =
            [
                E("browser", "cf", "HTTPS"),
                E("cf", "ec2", "A · DNS"),
                E("ec2", "caddy", "80/443"),
                E("caddy", "next", "proxy"),
                E("next", "nest", "/v1"),
                E("nest", "pg", "SQL"),
                E("nest", "redis", "cache"),
                E("docker", "caddy", "runs"),
                E("docker", "next", null),
                E("docker", "nest", null),
            ],
        },
        ["portfolio"] = new FlowDef
        {
            ProjectSlug = "portfolio",
            TitlePt = "Fluxo Portfólio",
            TitleEn = "Portfolio flow",
            CaptionPt = "CV ao vivo: DNS → Caddy → ASP.NET, com imagem no ECR e Ansible no host.",
            CaptionEn = "Live resume: DNS → Caddy → ASP.NET, with ECR image and Ansible on the host.",
            LayerOrder = ["client", "edge", "gateway", "app", "build", "iac"],
            Nodes =
            [
                N("browser", "Browser", "CV / demos", "Resume / demos", "googlechrome", "#e8eef1", "client", 0, 0,
                    "Você está a ler esta página no browser.",
                    "You are reading this page in the browser.",
                    "O site público do CV.",
                    "The public resume site.",
                    "GET https://portfolio.galasse.dev/"),
                N("cf", "Cloudflare DNS", "portfolio.galasse.dev", "portfolio.galasse.dev", "cloudflare", "#f6821f", "edge", 1, 0,
                    "O domínio aponta para o servidor do portfólio.",
                    "The domain points at the portfolio server.",
                    "DNS na edge e redirect do apex.",
                    "Edge DNS and apex redirect.",
                    "galasse.dev → 301 portfolio"),
                N("caddy", "Caddy", "TLS LE", "TLS LE", "nginx", "#3dd6c6", "gateway", 2, 0,
                    "HTTPS e proxy para o container ASP.NET.",
                    "HTTPS and proxy to the ASP.NET container.",
                    "Gateway no host EC2.",
                    "Gateway on the EC2 host.",
                    "reverse_proxy portfolio:8080"),
                N("aspnet", "ASP.NET Core", "Razor Pages", "Razor Pages", "dotnet", "#512bd4", "app", 3, 0,
                    "O site do CV é gerado no servidor.",
                    "The resume site is server-rendered.",
                    ".NET 8 com i18n pt-BR / en-US.",
                    ".NET 8 with pt-BR / en-US i18n.",
                    "app.MapRazorPages();",
                    "https://github.com/dangalasse/portfolio"),
                N("ts", "TypeScript", "esbuild", "esbuild", "typescript", "#3178c6", "build", 3, 1,
                    "Scripts do canvas e Labs em TypeScript.",
                    "Canvas and Labs scripts in TypeScript.",
                    "Frontend tipado, sem framework pesado.",
                    "Typed frontend, no heavy framework.",
                    "export function initArchitectureFlows()"),
                N("ecr", "Amazon ECR", "Image", "Image", "amazonwebservices", "#ff9900", "gateway", 2, 1,
                    "A imagem Docker do site fica na AWS.",
                    "The site Docker image lives in AWS.",
                    "CI faz build e push para o ECR.",
                    "CI builds and pushes to ECR.",
                    "docker push …/portfolio:latest"),
                N("ansible", "Ansible", "Host setup", "Host setup", "ansible", "#ee0000", "iac", 4, 0,
                    "Playbooks que preparam Docker, Caddy e o app no host.",
                    "Playbooks that set up Docker, Caddy, and the app on the host.",
                    "Configuração do host versionada com Ansible.",
                    "Host setup versioned with Ansible.",
                    "- name: Ensure portfolio container\n  community.docker.docker_container:",
                    "https://github.com/dangalasse/portfolio/tree/main/ansible"),
            ],
            Edges =
            [
                E("browser", "cf", "HTTPS"),
                E("cf", "caddy", "EIP"),
                E("ecr", "caddy", "pull"),
                E("caddy", "aspnet", ":8080"),
                E("ts", "aspnet", "wwwroot"),
                E("ansible", "caddy", "configure"),
                E("ansible", "ecr", "deploy"),
            ],
        },
        ["edge-status"] = new FlowDef
        {
            ProjectSlug = "edge-status",
            TitlePt = "Fluxo Edge Status",
            TitleEn = "Edge Status flow",
            CaptionPt = "O browser pergunta à edge quem é e de onde responde.",
            CaptionEn = "The browser asks the edge who it is and where it answers from.",
            LayerOrder = ["client", "origin", "edge"],
            Nodes =
            [
                N("browser", "Browser TS", "Labs", "Labs", "typescript", "#3178c6", "client", 0, 0,
                    "O cartão Labs mostra status ao vivo.",
                    "The Labs card shows live status.",
                    "Probe TypeScript no client.",
                    "Client-side TypeScript probe.",
                    "fetch(edgeStatusUrl)"),
                N("aspnet", "ASP.NET /api/status", "Fallback", "Fallback", "dotnet", "#512bd4", "origin", 1, 1,
                    "Se o Worker não estiver ligado, o site local responde.",
                    "If the Worker is offline, the local site answers.",
                    "Fallback local enquanto o Worker não existe.",
                    "Local fallback until the Worker is up.",
                    "MapGet(\"/api/status\", …)"),
                N("worker", "CF Worker", "region · cf-ray", "region · cf-ray", "cloudflareworkers", "#f6821f", "edge", 1, 0,
                    "Um Worker na Cloudflare devolve região e cf-ray.",
                    "A Cloudflare Worker returns region and cf-ray.",
                    "Resposta real da edge (região + cf-ray).",
                    "Real edge response (region + cf-ray).",
                    "return Response.json({ region, ray })",
                    "https://github.com/dangalasse/portfolio/tree/main/workers/edge-status"),
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
            CaptionPt = "Site estático privado atrás de CloudFront — provisionado com Terraform.",
            CaptionEn = "Private static site behind CloudFront — provisioned with Terraform.",
            LayerOrder = ["client", "cdn", "storage", "iac"],
            Nodes =
            [
                N("user", "Usuário", "HTTPS", "HTTPS", "googlechrome", "#e8eef1", "client", 0, 0,
                    "Alguém abre static.galasse.dev.",
                    "Someone opens static.galasse.dev.",
                    "Demo Free Tier acessível pelo browser.",
                    "Free Tier demo reachable in the browser.",
                    "GET https://static.galasse.dev/"),
                N("cfdist", "CloudFront", "CDN + OAC", "CDN + OAC", "amazoncloudfront", "#ff9900", "cdn", 1, 0,
                    "A CDN entrega o file sem expor o bucket.",
                    "The CDN serves files without exposing the bucket.",
                    "OAC + PriceClass_100 para manter custo baixo.",
                    "OAC + PriceClass_100 to keep cost low.",
                    "origin_access_control { … }"),
                N("s3", "Amazon S3", "Privado", "Private", "amazons3", "#569a31", "storage", 2, 0,
                    "Os files ficam num bucket fechado.",
                    "Files live in a locked-down bucket.",
                    "Block Public Access ligado.",
                    "Block Public Access enabled.",
                    "block_public_acls = true"),
                N("tf", "Terraform", "IaC", "IaC", "terraform", "#7B42BC", "iac", 1, 1,
                    "Toda a infra está descrita em código.",
                    "All infrastructure is described as code.",
                    "apply / destroy reprodutível.",
                    "Reproducible apply / destroy.",
                    "terraform apply",
                    "https://github.com/dangalasse/aws-static-demo"),
                N("ansible", "Ansible sync", "S3 + invalidate", "S3 + invalidate", "ansible", "#ee0000", "iac", 2, 1,
                    "Um playbook publica o site e limpa a cache.",
                    "A playbook publishes the site and clears cache.",
                    "Sync e invalidação depois do Terraform.",
                    "Sync and invalidation after Terraform.",
                    "- ansible.builtin.command: aws s3 sync …"),
            ],
            Edges =
            [
                E("user", "cfdist", "HTTPS"),
                E("cfdist", "s3", "OAC"),
                E("tf", "cfdist", "apply"),
                E("tf", "s3", "apply"),
                E("ansible", "s3", "sync"),
                E("ansible", "cfdist", "invalidate"),
            ],
        },
        ["edge-labs"] = new FlowDef
        {
            ProjectSlug = "edge-labs",
            TitlePt = "Fluxo Edge Labs (LLMOps)",
            TitleEn = "Edge Labs (LLMOps) flow",
            CaptionPt = "Cliente → Worker → Workers AI (ou Gemini) → JSON com provider, model e analyzedAt.",
            CaptionEn = "Client → Worker → Workers AI (or Gemini) → JSON with provider, model, and analyzedAt.",
            LayerOrder = ["client", "edge", "ai"],
            Nodes =
            [
                N("client", "Cliente", "Playground", "Playground", "googlechrome", "#e8eef1", "client", 0, 0,
                    "Cola um erro ou um cenário SDD/DDD/TDD.",
                    "Paste an error or an SDD/DDD/TDD scenario.",
                    "UI bilíngue; a resposta traz provider/model/analyzedAt.",
                    "Bilingual UI; response includes provider/model/analyzedAt.",
                    "POST /analyze-error | /coach",
                    "https://edge.galasse.dev/"),
                N("worker", "CF Worker", "edge-labs", "edge-labs", "cloudflare", "#f6821f", "edge", 1, 0,
                    "O Worker valida o pedido e chama o LLM.",
                    "The Worker validates the request and calls the LLM.",
                    "LLMOps no Free Tier, sem VM dedicada.",
                    "LLMOps on Free Tier, no dedicated VM.",
                    "export default { async fetch() {…} }",
                    "https://github.com/dangalasse/edge-labs"),
                N("ai", "Workers AI", "llama fp8", "llama fp8", "cloudflare", "#f6821f", "ai", 2, 0,
                    "O modelo devolve resumo, causa e sugestão (ou coaching).",
                    "The model returns summary, cause, and suggestion (or coaching).",
                    "Inferência real — analyzedAt muda a cada chamada.",
                    "Real inference — analyzedAt changes every call.",
                    "ai.run('@cf/meta/llama-3.1-8b-instruct-fp8', …)"),
            ],
            Edges =
            [
                E("client", "worker", "JSON"),
                E("worker", "ai", "inference"),
            ],
        },
        ["pipeview"] = new FlowDef
        {
            ProjectSlug = "pipeview",
            TitlePt = "Fluxo Pipeview",
            TitleEn = "Pipeview flow",
            CaptionPt = "Push → GitHub Actions → testes → revisão AI → deploy Workers (staging/prod).",
            CaptionEn = "Push → GitHub Actions → tests → AI review → Workers deploy (staging/prod).",
            LayerOrder = ["source", "ci", "ai", "edge", "iac"],
            Nodes =
            [
                N("push", "Git Push", "main / PR", "main / PR", "git", "#f05032", "source", 0, 0,
                    "O código entra no repositório.",
                    "Code lands in the repository.",
                    "Gatilho da esteira.",
                    "Pipeline trigger.",
                    "git push origin main",
                    "https://github.com/dangalasse/pipeline-pulse"),
                N("gha", "GitHub Actions", "CI", "CI", "githubactions", "#2088FF", "ci", 1, 0,
                    "A esteira corre lint, types, testes e build.",
                    "The pipeline runs lint, types, tests, and build.",
                    "CI com jobs reais — dá para abrir os runs no GitHub.",
                    "CI with real jobs — you can open the runs on GitHub.",
                    "jobs:\n  verify:\n    runs-on: ubuntu-latest"),
                N("test", "Tests", "vitest", "vitest", "vitest", "#729b1b", "ci", 1, 1,
                    "Testes automatizados falham cedo se algo quebrar.",
                    "Automated tests fail early when something breaks.",
                    "Gate de qualidade antes do deploy.",
                    "Quality gate before deploy.",
                    "npm test"),
                N("ai", "AI Review", "Edge Labs", "Edge Labs", "cloudflare", "#f6821f", "ai", 2, 0,
                    "Se falhar, a IA resume o log para humanos.",
                    "On failure, AI summarizes the log for humans.",
                    "Chamada ao Edge Labs quando o job falha.",
                    "Call to Edge Labs when the job fails.",
                    "POST edge.galasse.dev/analyze-error"),
                N("worker", "CF Worker", "pipeview.galasse.dev", "pipeview.galasse.dev", "cloudflareworkers", "#f6821f", "edge", 3, 0,
                    "O painel ao vivo mostra SHA, env e CF-Ray.",
                    "The live dashboard shows SHA, env, and CF-Ray.",
                    "Meta do deploy na edge.",
                    "Deploy meta at the edge.",
                    "GET /api/deploy-meta",
                    "https://pipeview.galasse.dev/"),
                N("tf", "Terraform", "DNS / route", "DNS / route", "terraform", "#7B42BC", "iac", 3, 1,
                    "DNS e rota do Worker descritos em IaC.",
                    "Worker DNS and route described as IaC.",
                    "Cloudflare IaC no mesmo repo.",
                    "Cloudflare IaC in the same repo.",
                    "resource \"cloudflare_workers_route\" …"),
            ],
            Edges =
            [
                E("push", "gha", "dispatch"),
                E("gha", "test", "run"),
                E("test", "ai", "on fail"),
                E("gha", "worker", "deploy"),
                E("ai", "worker", "review"),
                E("tf", "worker", "route"),
            ],
        },
    };
}
