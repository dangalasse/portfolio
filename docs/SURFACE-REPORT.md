# Surface report — portfolio, Edge Labs, AWS Ops Labs

Date: 2026-08-26  
Scope: recruiter walkthrough of live surfaces (desktop-width Cursor browser + curl), **defensive** review only (no exploit PoCs).  
Live hosts: `portfolio.galasse.dev`, `edge.galasse.dev`, `pipeview.galasse.dev`, `static.galasse.dev`, Lambda Function URL `4notqcazblkzqyd3avwjrkxtki0grnho.lambda-url.sa-east-1.on.aws`.

## What was exercised

| Surface | Result |
|---|---|
| `/` hero, CV, locale, status pill, featured cards | Hero/nav/locale work. **CV PDF 404**. Status pill falls back to ASP.NET (`EDGE_STATUS_URL` empty). |
| Menu (PT-BR) | Opens Início / Projetos / Labs / Sobre + locale toggle. |
| `/Projects` and `/Projects/tote` canvas | Cards and nodes work. Drawer showed **facade** snippet `@Controller('assets') {}` (fixed this session). |
| `/Labs` | Cards + “Como ativar o Worker” (Worker probe not wired). AWS card pointed at raw Function URL JSON (fixed). |
| `/About` | Socials, recruiter map links, email. |
| Locale `POST /api/locale` → `en-US` | Cookie `portfolio_locale`; `/Projects` and `/About` switch to English. |
| `edge.galasse.dev` | Title “LLMOps”; badge “Secret Gemini não configurado”; SDD tab **replaced** the error with a coaching question; submit without Turnstile showed **`ticket_required`** (fixed). |
| Lambda `GET /status` | JSON with four labs, all `ok`. |
| Lambda `POST /probe` | **200 with no auth** (debt). |
| Lambda `GET /kms/random` | Fingerprint + note, no raw hex dump of the full blob. |
| Lambda `OPTIONS` | Reflects `Origin` (CORS `AllowOrigins: ["*"]`). |
| `POST /analyze-error` without ticket | `403 { error: ticket_required, message: "Missing X-Demo-Ticket…" }` — gate works. |
| Honeypot `GET /.env` | Playful 404 JSON, no secrets. |

## Broken / misleading UX (before this session)

1. **Fake architecture snippets** — placeholders (`GET https://…`, `aws ec2 describe-instances --instance-ids i-…`, empty Nest controller, `terraform apply`) presented as “código do projeto”.
2. **“LLMOps” + Gemini fallback copy** — read as a misconfiguration; real path is Workers AI on Free Tier.
3. **SDD / DDD / TDD did not change error analysis** — Erro hit `/analyze-error` with a fixed SRE prompt; other tabs hit `/coach` with **different default payloads**.
4. **`ticket_required` in the UI** — `renderResult` printed `data.error` and ignored `data.message`.
5. **AWS Ops Labs “live demo”** was the Function URL `/status` JSON. No page.
6. **`/files/cv.pdf` → 404** — `wwwroot/files/` only has `.gitkeep`. Links still say “Baixar CV”.
7. **Edge Status Worker not live** — Labs still shows setup steps; pill uses `/api/status` (`region: local-aspnet`).

## Attack surface (defensive notes)

| Finding | Severity | Notes |
|---|---|---|
| Lambda Function URL `AuthType: NONE` | Medium (accepted demo debt) | Anyone can `GET /status`, `GET /kms/random`, **`POST /probe`**, `POST /ack`. Probe writes DynamoDB and emits EMF. Do not harden unless asked. |
| Lambda CORS `AllowOrigins: ["*"]` | Low | Browser from any origin can read `/status`. Portfolio now prefers same-origin proxy `/api/aws-ops-status`. |
| Edge Labs Demo Gate | OK | Mutations require Turnstile → ticket → KV quota. CORS allowlist (not `*`). Honeypots for `/.env`, `/admin`, `/gemini-key`. |
| Edge `GET /health` | Low | Public JSON: `geminiConfigured`, `workersAiBound`, `gate`. Useful for recruiters; no secrets. |
| Edge `HEAD /health` → 404 | Info | Worker only handles `GET`. Scanners using HEAD look “down”; Lambda probe uses GET. |
| Portfolio headers | OK-ish | HSTS, `X-Content-Type-Options`, `X-Frame-Options`, `Referrer-Policy`. **No CSP**. |
| `static.galasse.dev` | Low | CloudFront/S3; no CSP / HSTS visible on the object response. |
| Architecture drawer | OK | Snippets were `textContent`; highlighter now escapes then wraps tokens. Copy uses `dataset.raw`. |
| Locale cookie | Info | `HttpOnly: false`, `SameSite=Lax` — needed for the form; not a session cookie. |
| Simple Icons CDN | Info | Canvas icons from `cdn.simpleicons.org`. |
| `/api/*` | OK | `robots.txt` disallows `/api/`. Status and AWS Ops proxies are read-only. |

No credentials, `.env`, or wrangler secrets were observed in public JSON.

## Corrected in this session

- Real 6–12 line excerpts in `ArchitectureCatalog.cs` + drawer language badge and token colouring.
- Edge Labs renamed to **Coach de erros no edge** / **Edge error coach**. Workers AI badge is healthy (green), Gemini is “upgrade opcional”.
- All tabs send the **same error log** to `POST /analyze-error` with `mode=sre|sdd|ddd|tdd`. Result shows the active mode. `/coach` remains for Pipeview/service auth.
- Gate errors render `message`; `ticket_required` maps to “completa o Turnstile”.
- New page `/Labs/aws-ops` with status cards + formatted KMS fingerprint. `LiveUrl` and Labs AWS card point here. Function URL is footer “API crua”. Proxies: `/api/aws-ops-status`, `/api/aws-ops-kms`.

## Remaining debt (not hardened)

- Public Function URL + unauthenticated `POST /probe` / `POST /ack`.
- Missing `wwwroot/files/cv.pdf`.
- `EDGE_STATUS_URL` unset on the portfolio container.
- No Content-Security-Policy on portfolio or static demo.
- Edge Worker does not answer `HEAD`.
- Portfolio live deploy is CI (`push` to `main`). This session pushed `d0ffd4b`; **GitHub Actions failed** because `AWS_REGION` / `ECR_REPOSITORY` secrets are empty (`aws-region` required). `/Labs/aws-ops` was verified on a local Kestrel (`127.0.0.1:5198`) including `/api/aws-ops-status` and `/api/aws-ops-kms`. Production `portfolio.galasse.dev` still serves the previous image until those secrets are set and the workflow is re-run.

## Skills used

Defensive QA only: live browser walkthrough, header/CORS/authz checks, XSS-safe snippet rendering. No exploit payloads.
