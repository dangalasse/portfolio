# Recruiter / reviewer map

This site is the **evidence hub** for a Pleno DevOps / SRE / Cloud portfolio.
Every featured project links to a **live URL** and a **public GitHub repo** with IaC, CI/CD, or edge code you can clone and inspect.

## Live surfaces (Free Tier first)

| Proof | Live URL | Repo | IaC / CI |
|-------|----------|------|----------|
| This portfolio | https://portfolio.galasse.dev | [portfolio](https://github.com/dangalasse/portfolio) | Ansible roles · Actions→ECR |
| TOTE product demo | https://demo.tote.galasse.dev | [TOTE](https://github.com/dangalasse/TOTE) | Compose live · [Helm](https://github.com/dangalasse/TOTE/tree/main/k8s/tote-chart) · CDK |
| Pipeline Pulse | https://pipeline.galasse.dev | [pipeline-pulse](https://github.com/dangalasse/pipeline-pulse) | GitHub Actions · Wrangler · Terraform route stub |
| AWS Static Demo | https://static.galasse.dev | [aws-static-demo](https://github.com/dangalasse/aws-static-demo) | Terraform S3+CF · Ansible sync |
| Edge Labs (LLMOps) | https://edge.galasse.dev/ | [edge-labs](https://github.com/dangalasse/edge-labs) | Workers AI · `/coach` SDD/DDD/TDD · TF stub |
| Edge Status probe | Worker URL via Labs | [workers/edge-status](https://github.com/dangalasse/portfolio/tree/main/workers/edge-status) | Cloudflare Worker |

## How to review in 15 minutes

1. Open [portfolio.galasse.dev](https://portfolio.galasse.dev) → **Projects** (click a node on the interactive flow) and **Labs**.
2. Click **Demo ao vivo** on AWS Static (`static.galasse.dev`) and Edge Labs playground (`/`).
3. On [pipeline.galasse.dev](https://pipeline.galasse.dev) use **Correr demo ao vivo** / **Run live demo** (dispatches `live-demo.yml`) and open Actions.
4. Skim Terraform in [aws-static-demo](https://github.com/dangalasse/aws-static-demo) (OAC, public access block).
5. Skim Ansible roles + OTEL in [portfolio](https://github.com/dangalasse/portfolio) (`ansible/roles/`, `Observability/`).
6. Skim Helm in [TOTE k8s/tote-chart](https://github.com/dangalasse/TOTE/tree/main/k8s/tote-chart).

```bash
# Error analysis
curl -sS -X POST https://edge.galasse.dev/analyze-error \
  -H 'content-type: application/json' \
  -d '{"message":"ECONNREFUSED 127.0.0.1:5432","context":"NestJS boot","locale":"pt-BR"}'

# SDD / DDD / TDD coaching
curl -sS -X POST https://edge.galasse.dev/coach \
  -H 'content-type: application/json' \
  -d '{"mode":"sdd","message":"Como modelar Asset vs Movimentação?","locale":"pt-BR"}'
```

## Design principles (what seniors look for)

- **Least privilege on the edge of the internet:** S3 never public; CloudFront OAC only.
- **Secrets out of git:** GitHub Secrets, Wrangler secrets, Ansible Vault placeholders.
- **Observability without a second bill:** Grafana Cloud Free + OTLP + Alloy (documented; enable with env).
- **CI as product:** Pipeline Pulse shows SHA/env and can dispatch a safe live-demo workflow.
- **Honest scope:** Helm chart is demonstrative; production TOTE remains Compose on EC2. Edge coach is prompted coaching, not fine-tuned weights.
- **Free Tier hosting:** AWS (S3/CloudFront/ACM/EC2/ECR) + Cloudflare (Workers, Workers AI, DNS).

## Repo index

| Repo | Role |
|------|------|
| [dangalasse/portfolio](https://github.com/dangalasse/portfolio) | CV site, Ansible roles, OTEL, Actions→ECR |
| [dangalasse/TOTE](https://github.com/dangalasse/TOTE) | Product + Helm chart |
| [dangalasse/pipeline-pulse](https://github.com/dangalasse/pipeline-pulse) | CI/CD → Workers + live-demo |
| [dangalasse/aws-static-demo](https://github.com/dangalasse/aws-static-demo) | Terraform S3+CloudFront + Ansible sync |
| [dangalasse/edge-labs](https://github.com/dangalasse/edge-labs) | LLMOps Worker + coach modes |
