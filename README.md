# Portfolio

Portfólio pessoal (CV) com **ASP.NET Core 8**, **TypeScript** e **Tailwind CSS**.

Inclui:

- Home com **dois** destaques (TOTE = produto, Pipeview = prova). Labs não competem na prateleira.
- Projetos agrupados: produto / prova / labs
- Labs com páginas humanas (`/Labs/Ops`, `/Labs/Static`) — JSON/HTML crus são anexo
- Mapa de recrutador **no site** (`/` e `/About`), não só no GitHub
- CV PDF só se `wwwroot/files/cv.pdf` existir (sem botão 404)
- Probe Edge: Worker em `appsettings.json`; fallback ASP.NET se a URL falhar

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Node.js 20+

## Desenvolvimento

```bash
cd src/Portfolio
npm install
npm run build
dotnet run
```

Abrir `http://localhost:5xxx` (porta em `Properties/launchSettings.json`).

Scripts frontend:

| Script | Função |
|--------|--------|
| `npm run build` | CSS (Tailwind) + JS (esbuild) → `wwwroot/` |
| `npm run watch:css` | Tailwind em watch |
| `npm run watch:js` | TypeScript em watch |

O alvo MSBuild `NpmBuild` corre `npm run build` antes de cada `dotnet build`.

## Observabilidade (Grafana Cloud Free Tier)

**App (traces + métricas):** `OpenTelemetryExtensions` exporta OTLP quando `OpenTelemetry:Enabled=true` (ou env). Configure:

```bash
export OpenTelemetry__Enabled=true
export OTEL_EXPORTER_OTLP_ENDPOINT="https://otlp-gateway-prod-<REGION>.grafana.net/otlp"
export OTEL_EXPORTER_OTLP_HEADERS="Authorization=Basic <GRAFANA_OTLP_BASIC_AUTH>"
```

**Host (EC2):** playbook Ansible instala Node Exporter + Grafana Alloy (sucessor do Agent) e faz remote_write para o Prometheus do Grafana Cloud. Ver `ansible/`.

WHY Alloy (não Agent legado): mesmo Free Tier, binário suportado, config River unificada.

## Deploy (Ansible + GitHub Actions)

1. Copiar `ansible/inventory.example.ini` → `inventory.ini` e `group_vars/all.yml.example` → secrets locais / Vault.
2. Bootstrap host: `ansible-playbook -i inventory.ini ansible/portfolio-server-setup.yml`
3. Secrets no GitHub (repo): `AWS_ACCESS_KEY_ID`, `AWS_SECRET_ACCESS_KEY`, `AWS_REGION`, `ECR_REPOSITORY`, `EC2_HOST`, `EC2_USER`, `EC2_SSH_KEY`, e opcionalmente `GRAFANA_*`.
4. Push em `main` dispara `.github/workflows/deploy.yml` (build → ECR → Ansible recreate container).

## Personalizar

Editar `src/Portfolio/Data/PortfolioCatalog.cs`:

- Nome, role, tagline, email
- URLs LinkedIn / GitHub / site
- Lista de projectos e labs

Colocar o PDF do CV em `wwwroot/files/cv.pdf`. Enquanto o arquivo não existir, o site **não** mostra “Baixar CV”.

## Cloudflare Worker (opcional)

Exemplo em `workers/edge-status/`. Depois de deploy, passar a URL no layout:

```csharp
ViewData["EdgeStatusUrl"] = "https://edge-status.seu-subdominio.workers.dev";
```

(ou via configuração / env).

## AWS demo + Edge Labs

- AWS Static: [static.galasse.dev](https://static.galasse.dev) — Terraform in [aws-static-demo](https://github.com/dangalasse/aws-static-demo)
- AWS Ops Labs: [Function URL](https://4notqcazblkzqyd3avwjrkxtki0grnho.lambda-url.sa-east-1.on.aws/status) (Lambda + DynamoDB + EventBridge + KMS GenerateRandom) — [labs/always-free](labs/always-free)
- Edge Labs: [edge.galasse.dev/health](https://edge.galasse.dev/health) — [edge-labs](https://github.com/dangalasse/edge-labs)
- Reviewer map: [docs/RECRUITER.md](docs/RECRUITER.md) (also rendered on `/` and `/About`)

## Licença

MIT
