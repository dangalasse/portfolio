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
                    SnippetLang = n.SnippetLang,
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
        public string SnippetLang { get; init; } = "txt";
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
        string? repoUrl = null,
        string snippetLang = "txt") =>
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
            SnippetLang = snippetLang,
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
                    """
                    const res = await fetch("https://demo.tote.galasse.dev/", {
                      credentials: "include",
                    });
                    if (!res.ok) {
                      throw new Error(`demo HTTP ${res.status}`);
                    }
                    """,
                    snippetLang: "ts"),
                N("cf", "Cloudflare DNS", "tote.galasse.dev", "tote.galasse.dev", "cloudflare", "#f6821f", "edge", 1, 0,
                    "O nome do site aponta para o servidor certo.",
                    "The domain name points to the right server.",
                    "DNS na Cloudflare (Free Tier).",
                    "DNS on Cloudflare (Free Tier).",
                    """
                    # Cloudflare DNS (zone galasse.dev) — A record, DNS-only.
                    # Proxied=false so Caddy on EC2 terminates TLS (Let's Encrypt).
                    type     = "A"
                    name     = "tote"
                    proxied  = false
                    ttl      = 1
                    """,
                    snippetLang: "hcl"),
                N("ec2", "AWS EC2", "Elastic IP · Compose", "Elastic IP · Compose", "amazonec2", "#ff9900", "edge", 1, 1,
                    "Uma máquina na AWS hospeda os containers.",
                    "One AWS machine hosts the containers.",
                    "Compute Free Tier com IP fixo.",
                    "Free Tier compute with a fixed IP.",
                    """
                    # docker-compose.yml — DB is bound to loopback, not 0.0.0.0.
                    db:
                      image: postgres:16-alpine
                      ports:
                        - "127.0.0.1:${DB_PORT:-5432}:5432"
                      healthcheck:
                        test: ["CMD-SHELL", "pg_isready -U tote -d tote"]
                    """,
                    snippetLang: "yaml"),
                N("caddy", "Caddy", "TLS Let's Encrypt", "TLS Let's Encrypt", "nginx", "#3dd6c6", "gateway", 2, 0,
                    "Caddy termina o HTTPS e encaminha o tráfego.",
                    "Caddy terminates HTTPS and proxies traffic.",
                    "TLS com renovação automática.",
                    "TLS with automatic renewal.",
                    """
                    :443 {
                      tls /certs/fullchain.pem /certs/privkey.pem
                      reverse_proxy frontend:3000 {
                        health_uri /api/setup/status
                        health_interval 30s
                        health_timeout 5s
                      }
                    }
                    """,
                    snippetLang: "caddy"),
                N("docker", "Docker", "Compose full", "Compose full", "docker", "#2496ed", "gateway", 2, 1,
                    "Um comando sobe a pilha inteira.",
                    "One command brings the whole stack up.",
                    "Compose para local e produção.",
                    "Compose for local and production.",
                    """
                    # docker-compose.yml — public entry is Caddy :80/:443 only.
                    caddy:
                      image: caddy:2-alpine
                      ports:
                        - "80:80"
                        - "443:443"
                      volumes:
                        - ./deploy/Caddyfile:/etc/caddy/Caddyfile:ro
                      depends_on:
                        frontend:
                          condition: service_healthy
                    """,
                    snippetLang: "yaml"),
                N("next", "Next.js", "Frontend :3000", "Frontend :3000", "nextdotjs", "#e8eef1", "app", 3, 0,
                    "A interface do inventário — telas e formulários.",
                    "The inventory UI — screens and forms.",
                    "Frontend com App Router.",
                    "Frontend with App Router.",
                    """
                    'use client';
                    export default function AssetsPage() {
                      const table = useAssetTable();
                      return (
                        <DashboardPageHeader title="Ativos">
                          <AssetToolbar table={table} />
                          <AssetTable table={table} />
                        </DashboardPageHeader>
                      );
                    }
                    """,
                    "https://github.com/dangalasse/TOTE/blob/main/tote-frontend/app/(dashboard)/assets/page.tsx",
                    "tsx"),
                N("nest", "NestJS", "API /v1 :3001", "API /v1 :3001", "nestjs", "#e0234e", "app", 3, 1,
                    "A API aplica regras de negócio e autenticação.",
                    "The API enforces business rules and auth.",
                    "Backend Nest modular, alinhado a DDD.",
                    "Modular Nest backend, DDD-friendly.",
                    """
                    @Controller('assets')
                    @UseGuards(JwtAuthGuard)
                    export class AssetsController {
                      @Post()
                      @UseGuards(RolesGuard)
                      @Roles(Role.EDITOR, Role.ADMIN)
                      create(
                        @Body() dto: CreateAssetDto,
                        @CurrentUser() user: AuthUser,
                      ) { return this.assetsService.create(dto, user); }
                    }
                    """,
                    "https://github.com/dangalasse/TOTE/blob/main/tote-backend/src/modules/assets/assets.controller.ts",
                    "ts"),
                N("pg", "PostgreSQL", "Prisma ORM", "Prisma ORM", "postgresql", "#4169e1", "data", 4, 0,
                    "Onde ficam ativos e usuários.",
                    "Where assets and users live.",
                    "Postgres com migrações Prisma.",
                    "Postgres with Prisma migrations.",
                    """
                    model Asset {
                      id         String      @id @default(uuid()) @db.Uuid
                      patrimony  String      @unique @db.VarChar(50)
                      serial     String?     @unique @db.VarChar(100)
                      name       String      @db.VarChar(255)
                      status     AssetStatus @default(ACTIVE)
                      deletedAt  DateTime?   @map("deleted_at")
                    }
                    """,
                    snippetLang: "prisma"),
                N("redis", "Redis", "Cache / filas", "Cache / queues", "redis", "#dc382d", "data", 4, 1,
                    "Cache e apoio a trabalhos em fila.",
                    "Cache and backing for queue jobs.",
                    "Sessão/cache ao lado da API.",
                    "Session/cache next to the API.",
                    """
                    redis:
                      image: redis:7-alpine
                      command: >
                        redis-server
                        --requirepass ${REDIS_PASSWORD:?must be set}
                        --protected-mode yes
                      ports:
                        - "127.0.0.1:${REDIS_PORT:-6379}:6379"
                    """,
                    snippetLang: "yaml")
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
                    """
                    app.MapGet("/api/status", (HttpContext http) =>
                    {
                        return Results.Json(new
                        {
                            ok = true,
                            service = "portfolio-status",
                            locale = Locale.Current(http),
                            checkedAt = DateTimeOffset.UtcNow,
                        });
                    });
                    """,
                    snippetLang: "cs"),
                N("cf", "Cloudflare DNS", "portfolio.galasse.dev", "portfolio.galasse.dev", "cloudflare", "#f6821f", "edge", 1, 0,
                    "O domínio aponta para o servidor do portfólio.",
                    "The domain points at the portfolio server.",
                    "DNS na edge e redirect do apex.",
                    "Edge DNS and apex redirect.",
                    """
                    # ansible/templates/Caddyfile.j2
                    {{ portfolio_domain }} {
                      encode gzip
                      reverse_proxy portfolio:8080
                    }
                    """,
                    snippetLang: "caddy"),
                N("caddy", "Caddy", "TLS LE", "TLS LE", "nginx", "#3dd6c6", "gateway", 2, 0,
                    "HTTPS e proxy para o container ASP.NET.",
                    "HTTPS and proxy to the ASP.NET container.",
                    "Gateway no host EC2.",
                    "Gateway on the EC2 host.",
                    """
                    portfolio.galasse.dev {
                      encode gzip
                      reverse_proxy portfolio:8080
                    }
                    """,
                    snippetLang: "caddy"),
                N("aspnet", "ASP.NET Core", "Razor Pages", "Razor Pages", "dotnet", "#512bd4", "app", 3, 0,
                    "O site do CV é gerado no servidor.",
                    "The resume site is server-rendered.",
                    ".NET 8 com i18n pt-BR / en-US.",
                    ".NET 8 with pt-BR / en-US i18n.",
                    """
                    app.UseStaticFiles();
                    app.UseRouting();
                    app.MapGet("/api/status", ...);
                    app.MapPost("/api/locale", ...);
                    app.MapGet("/api/aws-ops-status", ...);
                    app.MapRazorPages();
                    """,
                    "https://github.com/dangalasse/portfolio/blob/main/src/Portfolio/Program.cs",
                    "cs"),
                N("ts", "TypeScript", "esbuild", "esbuild", "typescript", "#3178c6", "build", 3, 1,
                    "Scripts do canvas e Labs em TypeScript.",
                    "Canvas and Labs scripts in TypeScript.",
                    "Frontend tipado, sem framework pesado.",
                    "Typed frontend, no heavy framework.",
                    """
                    export function initArchitectureFlows(): void {
                      const sections = document.querySelectorAll("[data-arch-flow]");
                      sections.forEach((section) => {
                        bindInteractions(section);
                        drawFlow(section);
                      });
                    }
                    """,
                    snippetLang: "ts"),
                N("ecr", "Amazon ECR", "Image", "Image", "amazonwebservices", "#ff9900", "gateway", 2, 1,
                    "A imagem Docker do site fica na AWS.",
                    "The site Docker image lives in AWS.",
                    "CI faz build e push para o ECR.",
                    "CI builds and pushes to ECR.",
                    """
                    # ansible/roles/portfolio_app/tasks/main.yml
                    - name: Recreate portfolio container
                      community.docker.docker_container:
                        name: "{{ portfolio_container_name }}"
                        image: "{{ portfolio_image }}"
                        state: started
                        restart_policy: unless-stopped
                        env:
                          EDGE_STATUS_URL: "{{ edge_status_url | default('') }}"
                    """,
                    snippetLang: "yaml"),
                N("ansible", "Ansible", "Host setup", "Host setup", "ansible", "#ee0000", "iac", 4, 0,
                    "Playbooks que preparam Docker, Caddy e o app no host.",
                    "Playbooks that set up Docker, Caddy, and the app on the host.",
                    "Configuração do host versionada com Ansible.",
                    "Host setup versioned with Ansible.",
                    """
                    - name: Recreate portfolio container
                      community.docker.docker_container:
                        name: "{{ portfolio_container_name }}"
                        image: "{{ portfolio_image }}"
                        state: started
                        networks:
                          - name: "{{ portfolio_network }}"
                            aliases: [portfolio]
                    """,
                    "https://github.com/dangalasse/portfolio/blob/main/ansible/roles/portfolio_app/tasks/main.yml",
                    "yaml"),
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
                    """
                    async function fetchEdgeStatus(): Promise<EdgeStatusPayload> {
                      const endpoint = document.body.dataset.edgeStatusUrl;
                      if (endpoint) {
                        const response = await fetch(endpoint, { cache: "no-store" });
                        if (response.ok) return { ok: true, source: "live", ... };
                      }
                      return fetch("/api/status"); // ASP.NET fallback
                    }
                    """,
                    snippetLang: "ts"),
                N("aspnet", "ASP.NET /api/status", "Fallback", "Fallback", "dotnet", "#512bd4", "origin", 1, 1,
                    "Se o Worker não estiver ligado, o site local responde.",
                    "If the Worker is offline, the local site answers.",
                    "Fallback local enquanto o Worker não existe.",
                    "Local fallback until the Worker is up.",
                    """
                    app.MapGet("/api/status", (HttpContext http) =>
                    {
                        var locale = Locale.Current(http);
                        return Results.Json(new
                        {
                            ok = true,
                            region = Environment.GetEnvironmentVariable("TOTE_REGION") ?? "local-aspnet",
                            runtime = $".NET {Environment.Version}",
                            service = "portfolio-status",
                        });
                    });
                    """,
                    snippetLang: "cs"),
                N("worker", "CF Worker", "region · cf-ray", "region · cf-ray", "cloudflareworkers", "#f6821f", "edge", 1, 0,
                    "Um Worker na Cloudflare devolve região e cf-ray.",
                    "A Cloudflare Worker returns region and cf-ray.",
                    "Resposta real da edge (região + cf-ray).",
                    "Real edge response (region + cf-ray).",
                    """
                    export default {
                      async fetch(request: Request): Promise<Response> {
                        const cf = request.cf;
                        const body = {
                          ok: true,
                          region: cf?.colo ?? "unknown",
                          ray: request.headers.get("cf-ray"),
                          service: "portfolio-edge-status",
                          checkedAt: new Date().toISOString(),
                        };
                        return Response.json(body);
                      },
                    };
                    """,
                    "https://github.com/dangalasse/portfolio/blob/main/workers/edge-status/src/index.ts",
                    "ts"),
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
                    """
                    origin {
                      domain_name              = aws_s3_bucket.site.bucket_regional_domain_name
                      origin_id                = "s3-${aws_s3_bucket.site.id}"
                      origin_access_control_id = aws_cloudfront_origin_access_control.site.id
                    }
                    default_cache_behavior {
                      viewer_protocol_policy = "redirect-to-https"
                      cached_methods         = ["GET", "HEAD"]
                    }
                    """,
                    snippetLang: "hcl"),
                N("cfdist", "CloudFront", "CDN + OAC", "CDN + OAC", "amazoncloudfront", "#ff9900", "cdn", 1, 0,
                    "A CDN entrega o file sem expor o bucket.",
                    "The CDN serves files without exposing the bucket.",
                    "OAC + PriceClass_100 para manter custo baixo.",
                    "OAC + PriceClass_100 to keep cost low.",
                    """
                    resource "aws_cloudfront_origin_access_control" "site" {
                      name                              = "${var.project_name}-oac"
                      origin_access_control_origin_type = "s3"
                      signing_behavior                  = "always"
                      signing_protocol                  = "sigv4"
                    }
                    """,
                    snippetLang: "hcl"),
                N("s3", "Amazon S3", "Privado", "Private", "amazons3", "#569a31", "storage", 2, 0,
                    "Os files ficam num bucket fechado.",
                    "Files live in a locked-down bucket.",
                    "Block Public Access ligado.",
                    "Block Public Access enabled.",
                    """
                    resource "aws_s3_bucket_public_access_block" "site" {
                      bucket = aws_s3_bucket.site.id
                      block_public_acls       = true
                      block_public_policy     = true
                      ignore_public_acls      = true
                      restrict_public_buckets = true
                    }
                    """,
                    snippetLang: "hcl"),
                N("tf", "Terraform", "IaC", "IaC", "terraform", "#7B42BC", "iac", 1, 1,
                    "Toda a infra está descrita em código.",
                    "All infrastructure is described as code.",
                    "apply / destroy reprodutível.",
                    "Reproducible apply / destroy.",
                    """
                    resource "aws_cloudfront_distribution" "site" {
                      enabled             = true
                      default_root_object = "index.html"
                      price_class         = var.price_class
                      origin {
                        origin_access_control_id = aws_cloudfront_origin_access_control.site.id
                      }
                    }
                    """,
                    "https://github.com/dangalasse/aws-static-demo/blob/main/terraform/main.tf",
                    "hcl"),
                N("ansible", "Ansible sync", "S3 + invalidate", "S3 + invalidate", "ansible", "#ee0000", "iac", 2, 1,
                    "Um playbook publica o site e limpa a cache.",
                    "A playbook publishes the site and clears cache.",
                    "Sync e invalidação depois do Terraform.",
                    "Sync and invalidation after Terraform.",
                    """
                    - name: Sync site to private S3
                      ansible.builtin.command:
                        cmd: aws s3 sync {{ site_dir }} s3://{{ bucket }} --delete
                    - name: Invalidate CloudFront
                      ansible.builtin.command:
                        cmd: aws cloudfront create-invalidation --distribution-id {{ dist }} --paths "/*"
                    """,
                    snippetLang: "yaml"),
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
        ["aws-ops-labs"] = new FlowDef
        {
            ProjectSlug = "aws-ops-labs",
            TitlePt = "Fluxo AWS Ops Labs",
            TitleEn = "AWS Ops Labs flow",
            CaptionPt = "Probe Always Free: Function URL + DynamoDB + EventBridge + KMS sem CMK.",
            CaptionEn = "Always Free probe: Function URL + DynamoDB + EventBridge + KMS with no CMK.",
            LayerOrder = ["client", "compute", "data", "schedule"],
            Nodes =
            [
                N("user", "Recrutador", "GET /status", "GET /status", "googlechrome", "#e8eef1", "client", 0, 0,
                    "Alguém abre a Function URL e vê o último check de cada lab.",
                    "Someone opens the Function URL and sees the latest check per lab.",
                    "Superfície pública sem API Gateway (Always Free).",
                    "Public surface with no API Gateway (Always Free).",
                    """
                    def latest_status() -> dict[str, Any]:
                        results = []
                        for name, url in _targets():
                            resp = table.query(
                                KeyConditionExpression=Key("pk").eq(f"LAB#{name}"),
                                ScanIndexForward=False,
                                Limit=1,
                            )
                            items = resp.get("Items") or []
                            results.append(_row(name, url, items))
                        return {"ok": all(r["ok"] is True for r in results), "results": results}
                    """,
                    snippetLang: "python"),
                N("fn", "Lambda 128 MB", "Function URL", "Function URL", "amazonwebservices", "#ff9900", "compute", 1, 0,
                    "Uma função Python responde HTTP e também corre no schedule.",
                    "One Python function serves HTTP and also runs on a schedule.",
                    "AuthType NONE + CORS; 15 s; sem NAT.",
                    "AuthType NONE + CORS; 15 s; no NAT.",
                    """
                    ProbeUrl:
                      Type: AWS::Lambda::Url
                      Properties:
                        AuthType: NONE
                        Cors:
                          AllowOrigins: ["*"]
                          AllowMethods: [GET, POST]
                        TargetFunctionArn: !GetAtt ProbeFn.Arn
                    """,
                    "https://github.com/dangalasse/portfolio/blob/main/labs/always-free/template.yaml",
                    "yaml"),
                N("ddb", "DynamoDB", "PAY_PER_REQUEST", "PAY_PER_REQUEST", "amazondynamodb", "#4053D6", "data", 2, 0,
                    "Cada probe grava pk/sk e some em 7 dias (TTL).",
                    "Each probe writes pk/sk and expires in 7 days (TTL).",
                    "25 GB Always Free; tabela minúscula de propósito.",
                    "25 GB Always Free; the table stays tiny on purpose.",
                    """
                    ChecksTable:
                      Type: AWS::DynamoDB::Table
                      Properties:
                        TableName: galasse-ops-labs
                        BillingMode: PAY_PER_REQUEST
                        TimeToLiveSpecification:
                          AttributeName: expireAt
                          Enabled: true
                    """,
                    snippetLang: "yaml"),
                N("kms", "KMS", "GenerateRandom", "GenerateRandom", "amazonwebservices", "#dd344c", "data", 2, 1,
                    "Entropia vem do HSM — sem chave gerenciada.",
                    "Entropy comes from the HSM — no customer-managed key.",
                    "CMK custaria $1/mês; este lab não cria CMK.",
                    "A CMK would be $1/mo; this lab never creates one.",
                    """
                    def kms_demo() -> dict[str, Any]:
                        # Always Free: GenerateRandom does not need a CMK (CMK = $1/mo).
                        resp = kms.generate_random(NumberOfBytes=32)
                        blob = resp["Plaintext"]
                        return {
                            "ok": True,
                            "api": "GenerateRandom",
                            "fingerprint": blob[:8].hex(),
                        }
                    """,
                    snippetLang: "python"),
                N("eb", "EventBridge", "rate(5 minutes)", "rate(5 minutes)", "amazonwebservices", "#e7157b", "schedule", 1, 1,
                    "A cada 5 minutos a regra invoca a mesma Lambda.",
                    "Every 5 minutes the rule invokes the same Lambda.",
                    "~288 invokes/dia — bem abaixo de 1M Always Free.",
                    "~288 invokes/day — well under the 1M Always Free cap.",
                    """
                    ProbeSchedule:
                      Type: AWS::Events::Rule
                      Properties:
                        Name: galasse-ops-labs-every-5m
                        ScheduleExpression: rate(5 minutes)
                        State: ENABLED
                        Targets:
                          - Id: ProbeFn
                            Arn: !GetAtt ProbeFn.Arn
                    """,
                    snippetLang: "yaml"),
            ],
            Edges =
            [
                E("user", "fn", "HTTPS"),
                E("eb", "fn", "invoke"),
                E("fn", "ddb", "PutItem"),
                E("fn", "kms", "GenerateRandom"),
            ],
        },
        ["edge-labs"] = new FlowDef
        {
            ProjectSlug = "edge-labs",
            TitlePt = "Fluxo Edge Labs",
            TitleEn = "Edge Labs flow",
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
                    """
                    const res = await fetch("/analyze-error", {
                      method: "POST",
                      headers: {
                        "content-type": "application/json",
                        "x-demo-ticket": ticket,
                      },
                      body: JSON.stringify({ message, context, locale, mode }),
                    });
                    """,
                    "https://edge.galasse.dev/",
                    "ts"),
                N("worker", "CF Worker", "edge-labs", "edge-labs", "cloudflare", "#f6821f", "edge", 1, 0,
                    "O Worker valida o pedido e chama o LLM.",
                    "The Worker validates the request and calls the LLM.",
                    "Coach de erros no edge, Free Tier, sem VM dedicada.",
                    "Edge error coach on Free Tier, no dedicated VM.",
                    """
                    if (request.method === "POST" && url.pathname === "/analyze-error") {
                      return handleAnalyze(request, env);
                    }
                    async function handleAnalyze(request: Request, env: Env) {
                      const denied = await assertMutationGate(request, env, "edge.analyze");
                      if (denied) return denied;
                      const payload = normalizeErrorPayload(body); // includes mode
                      return json(await analyzeErrorLog(payload, llmEnv(env)));
                    }
                    """,
                    "https://github.com/dangalasse/edge-labs/blob/main/src/index.ts",
                    "ts"),
                N("ai", "Workers AI", "llama fp8", "llama fp8", "cloudflare", "#f6821f", "ai", 2, 0,
                    "O modelo devolve resumo, causa e sugestão (ou coaching).",
                    "The model returns summary, cause, and suggestion (or coaching).",
                    "Inferência real — analyzedAt muda a cada chamada.",
                    "Real inference — analyzedAt changes every call.",
                    """
                    const result = await ai.run(
                      "@cf/meta/llama-3.1-8b-instruct-fp8",
                      {
                        messages: [
                          { role: "system", content: system },
                          { role: "user", content: user },
                        ],
                        max_tokens: 768,
                      },
                    );
                    """,
                    snippetLang: "ts"),
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
                    """
                    on:
                      pull_request:
                      push:
                        branches: [main]
                    jobs:
                      ci:
                        name: lint · typecheck · test · build
                        runs-on: ubuntu-latest
                    """,
                    "https://github.com/dangalasse/pipeline-pulse/blob/main/.github/workflows/ci.yml",
                    "yaml"),
                N("gha", "GitHub Actions", "CI", "CI", "githubactions", "#2088FF", "ci", 1, 0,
                    "A esteira corre lint, types, testes e build.",
                    "The pipeline runs lint, types, tests, and build.",
                    "CI com jobs reais — dá para abrir os runs no GitHub.",
                    "CI with real jobs — you can open the runs on GitHub.",
                    """
                    jobs:
                      ci:
                        name: lint · typecheck · test · build
                        runs-on: ubuntu-latest
                        steps:
                          - run: npm run lint
                          - run: npm run typecheck
                          - run: npm test
                          - run: npm run build
                    """,
                    snippetLang: "yaml"),
                N("test", "Tests", "vitest", "vitest", "vitest", "#729b1b", "ci", 1, 1,
                    "Testes automatizados falham cedo se algo quebrar.",
                    "Automated tests fail early when something breaks.",
                    "Gate de qualidade antes do deploy.",
                    "Quality gate before deploy.",
                    """
                    - name: Unit tests
                      run: npm test
                    - name: Build
                      env:
                        VITE_GIT_SHA: ${{ github.sha }}
                        VITE_DEPLOY_ENV: ci
                      run: npm run build
                    """,
                    snippetLang: "yaml"),
                N("ai", "AI Review", "Edge Labs", "Edge Labs", "cloudflare", "#f6821f", "ai", 2, 0,
                    "Se falhar, a IA resume o log para humanos.",
                    "On failure, AI summarizes the log for humans.",
                    "Chamada ao Edge Labs quando o job falha.",
                    "Call to Edge Labs when the job fails.",
                    """
                    const EDGE_ANALYZE_URL = "https://edge.galasse.dev/analyze-error";
                    const edgeRes = await fetch(EDGE_ANALYZE_URL, {
                      method: "POST",
                      headers: {
                        "Content-Type": "application/json",
                        "X-Demo-Service": "pipeview",
                        "X-Demo-Service-Ts": auth.ts,
                        "X-Demo-Service-Sig": auth.sig,
                      },
                      body: JSON.stringify({ message, context, locale }),
                    });
                    """,
                    snippetLang: "ts"),
                N("worker", "CF Worker", "pipeview.galasse.dev", "pipeview.galasse.dev", "cloudflareworkers", "#f6821f", "edge", 3, 0,
                    "O painel ao vivo mostra SHA, env e CF-Ray.",
                    "The live dashboard shows SHA, env, and CF-Ray.",
                    "Meta do deploy na edge.",
                    "Deploy meta at the edge.",
                    """
                    app.get("/api/deploy-meta", (c) => {
                      const meta: DeployMeta = {
                        service: "pipeview",
                        env: c.env.DEPLOY_ENV,
                        gitSha: c.env.GIT_SHA,
                        githubRunUrl: c.env.GITHUB_RUN_URL || null,
                        region: c.req.header("cf-ray") ?? null,
                      };
                      return c.json(meta);
                    });
                    """,
                    "https://pipeview.galasse.dev/",
                    "ts"),
                N("tf", "Terraform", "DNS / route", "DNS / route", "terraform", "#7B42BC", "iac", 3, 1,
                    "DNS e rota do Worker descritos em IaC.",
                    "Worker DNS and route described as IaC.",
                    "Cloudflare IaC no mesmo repo.",
                    "Cloudflare IaC in the same repo.",
                    """
                    resource "cloudflare_workers_route" "production" {
                      zone_id     = var.zone_id
                      pattern     = "${var.production_hostname}/*"
                      script_name = "pipeline-pulse"
                    }
                    """,
                    snippetLang: "hcl"),
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
