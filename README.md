# Portfolio

Portfólio pessoal (CV) com **ASP.NET Core 8**, **TypeScript** e **Tailwind CSS**.

Inclui:

- Home com hero e indicador de status ao vivo
- Projectos (lista + detalhe)
- Labs (Cloudflare / AWS / GitHub) com probe TypeScript
- Links LinkedIn, GitHub, site e CV PDF
- API `GET /api/status` para prova local até ligar um Worker Cloudflare

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

## Personalizar

Editar `src/Portfolio/Data/PortfolioCatalog.cs`:

- Nome, role, tagline, email
- URLs LinkedIn / GitHub / site
- Lista de projectos e labs

Colocar o PDF do CV em `wwwroot/files/cv.pdf`.

## Cloudflare Worker (opcional)

Exemplo em `workers/edge-status/`. Depois de deploy, passar a URL no layout:

```csharp
ViewData["EdgeStatusUrl"] = "https://edge-status.seu-subdominio.workers.dev";
```

(ou via configuração / env).

## AWS demo (opcional)

Ver notas no projecto `aws-static-demo`: S3 privado + CloudFront. Ligar a URL pública no catálogo.

## Licença

MIT
