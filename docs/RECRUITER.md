# Recruiter / reviewer map

This site is the **evidence hub** for a Pleno DevOps / SRE / Cloud portfolio.
Every featured project links to a **live URL** and a **public GitHub repo** with IaC, CI/CD, or edge code you can clone and inspect.

## Live surfaces (Free Tier first)

| Proof | Live URL | Repo |
|-------|----------|------|
| This portfolio | https://portfolio.galasse.dev | [portfolio](https://github.com/dangalasse/portfolio) |
| TOTE product demo | https://demo.tote.galasse.dev | [TOTE](https://github.com/dangalasse/TOTE) (+ [Helm chart](https://github.com/dangalasse/TOTE/tree/main/k8s/tote-chart)) |
| Pipeline Pulse | https://pipeline.galasse.dev | [pipeline-pulse](https://github.com/dangalasse/pipeline-pulse) |
| AWS Static Demo | https://static.galasse.dev | [aws-static-demo](https://github.com/dangalasse/aws-static-demo) |
| Edge Labs (LLMOps) | https://edge.galasse.dev/health | [edge-labs](https://github.com/dangalasse/edge-labs) |
| Edge Status probe | Labs page / `/api/status` | [workers/edge-status](https://github.com/dangalasse/portfolio/tree/main/workers/edge-status) |

## How to review in 15 minutes

1. Open [portfolio.galasse.dev](https://portfolio.galasse.dev) → **Projects** and **Labs**.
2. Click **Demo ao vivo** on AWS Static (`static.galasse.dev`) and Edge Labs (`/health` + sample POST below).
3. Open GitHub Actions on [pipeline-pulse](https://github.com/dangalasse/pipeline-pulse/actions).
4. Skim Terraform in [aws-static-demo](https://github.com/dangalasse/aws-static-demo) (OAC, public access block).
5. Skim Ansible + OTEL in [portfolio](https://github.com/dangalasse/portfolio) (`ansible/`, `Observability/`).
6. Skim Helm in [TOTE k8s/tote-chart](https://github.com/dangalasse/TOTE/tree/main/k8s/tote-chart).

```bash
curl -sS -X POST https://edge.galasse.dev/analyze-error \
  -H 'content-type: application/json' \
  -d '{"message":"ECONNREFUSED 127.0.0.1:5432","context":"NestJS boot"}'
```

## Design principles (what seniors look for)

- **Least privilege on the edge of the internet:** S3 never public; CloudFront OAC only.
- **Secrets out of git:** GitHub Secrets, Wrangler secrets, Ansible Vault placeholders.
- **Observability without a second bill:** Grafana Cloud Free + OTLP + Alloy (documented; enable with env).
- **CI as product:** Pipeline Pulse shows SHA/env from the deploy that shipped.
- **Honest scope:** Helm chart is demonstrative; production TOTE remains Compose on EC2.
- **Free Tier hosting:** AWS (S3/CloudFront/ACM/EC2/ECR) + Cloudflare (Workers, Workers AI, DNS).

## Repo index

| Repo | Role |
|------|------|
| [dangalasse/portfolio](https://github.com/dangalasse/portfolio) | CV site, Ansible, OTEL, Actions→ECR |
| [dangalasse/TOTE](https://github.com/dangalasse/TOTE) | Product + Helm chart |
| [dangalasse/pipeline-pulse](https://github.com/dangalasse/pipeline-pulse) | CI/CD → Workers |
| [dangalasse/aws-static-demo](https://github.com/dangalasse/aws-static-demo) | Terraform S3+CloudFront |
| [dangalasse/edge-labs](https://github.com/dangalasse/edge-labs) | LLMOps Worker |
