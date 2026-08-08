# Recruiter / reviewer map

This site is the **evidence hub** for a Pleno DevOps / SRE / Cloud portfolio.
Every featured project links to a **live URL** and a **public GitHub repo** with IaC, CI/CD, or edge code you can clone and inspect.

## Live surfaces (what you can load / what is capped)

| Proof | Live URL | You can load | Hard limit (by design) |
|-------|----------|--------------|------------------------|
| Portfolio | https://portfolio.galasse.dev | Full site, architecture flow, Labs | Read-mostly; honeypots wink |
| TOTE vitrine | https://demo.tote.galasse.dev | Real ADMIN UI (schema, EAV, audit, trash, obs, identity) | Turnstile → session; 1 IP / 30 min; branding **per workspace**; no password for `demo@…`; host `tote.galasse.dev` does **not** mint demo |
| Pipeview | https://pipeview.galasse.dev | Last real `live-demo.yml` run on open; canvas | Dispatch needs Turnstile + ticket + KV (1/IP/15m, 8/day) + `GITHUB_TOKEN` behind gate |
| Edge Labs | https://edge.galasse.dev/ | Playground Analyze / Coach (Workers AI) | Turnstile + ticket + KV (5/IP/h, 80/day); body ≤4KB; CORS strict |
| AWS Static | https://static.galasse.dev | Static HTML | No mutations |
| Edge Status | Labs / Worker | Uptime probes | Read-only |

**Principle:** you see a **true reflection** (real JSON, real Actions run, real Nest UI). Abuse is blocked by **contract** (Turnstile → HMAC ticket → quota), not a soft in-memory rate limit.

## How to review in 15 minutes

1. Open [portfolio.galasse.dev](https://portfolio.galasse.dev) → **Projects** / **Labs** → “Explorar ao vivo”.
2. [static.galasse.dev](https://static.galasse.dev) — static evidence; skim Terraform OAC in the repo.
3. [edge.galasse.dev](https://edge.galasse.dev/) — complete Turnstile, then Analyze / Coach; check `provider` / `model` / `analyzedAt` in the JSON.
4. [pipeview.galasse.dev](https://pipeview.galasse.dev) — last run is visible without clicking; “Iniciar uma demo” only after human check (and only if dispatch secret is configured).
5. [demo.tote.galasse.dev](https://demo.tote.galasse.dev) — human check → ephemeral ADMIN workspace (~2h). Try Colunas, Integridade EAV, Identidade; `/reports`, `/users`, aprovações stay hidden.
6. Skim Helm [TOTE k8s/tote-chart](https://github.com/dangalasse/TOTE/tree/main/k8s/tote-chart) and Ansible/OTEL in [portfolio](https://github.com/dangalasse/portfolio).

Open `POST /analyze-error` or `/coach` without a ticket → **403**. That is intentional.

## Design principles (what seniors look for)

- **Least privilege on the edge of the internet:** S3 never public; CloudFront OAC only.
- **Secrets out of git:** GitHub Secrets, Wrangler secrets, Turnstile + HMAC ticket secrets on Workers/Nest.
- **Demo Gate:** expensive mutations (LLM, Actions dispatch, vitrine mint) require Turnstile → one-shot ticket → KV/Redis quotas.
- **Observability without a second bill:** Grafana stays private; Edge Status + Labs copy show the path.
- **Honest scope:** Helm chart is demonstrative; production TOTE remains Compose on EC2. Edge coach is prompted coaching, not fine-tuned weights.
- **Free Tier hosting:** AWS (S3/CloudFront/ACM/EC2/ECR) + Cloudflare (Workers, Workers AI, Turnstile, KV, DNS).

## Repo index

| Repo | Role |
|------|------|
| [dangalasse/portfolio](https://github.com/dangalasse/portfolio) | CV site, Ansible roles, OTEL, Actions→ECR |
| [dangalasse/TOTE](https://github.com/dangalasse/TOTE) | Product + Helm chart |
| [dangalasse/pipeline-pulse](https://github.com/dangalasse/pipeline-pulse) | CI/CD → Workers + gated live-demo |
| [dangalasse/aws-static-demo](https://github.com/dangalasse/aws-static-demo) | Terraform S3+CloudFront + Ansible sync |
| [dangalasse/edge-labs](https://github.com/dangalasse/edge-labs) | LLMOps Worker + coach modes + Demo Gate |
