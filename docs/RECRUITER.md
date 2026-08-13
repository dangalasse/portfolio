# Recruiter / reviewer map

This site is the **evidence hub** for an infra / SRE / DevOps profile.
It is **not** an AI-developer pitch. Edge Labs is a playground, listed as a lab.

## Hierarchy (what to open first)

| Order | What | Why | Honest limit |
|-------|------|-----|----------------|
| 1 | [TOTE](https://portfolio.galasse.dev/Projects/tote) | The product | Turnstile checkbox mints an ephemeral ADMIN workspace (~2h). No password. |
| 2 | [Pipeview](https://pipeview.galasse.dev) | CI/CD proof: SHA, env, CF-Ray, workflow | Top card = published build. Belt = last `live-demo.yml` (goes red if lint fails). |
| 3 | [Labs](https://portfolio.galasse.dev/Labs) | Always Free / edge evidence | Not the job. Human pages: [/Labs/Ops](https://portfolio.galasse.dev/Labs/Ops), [/Labs/Static](https://portfolio.galasse.dev/Labs/Static). |
| — | CV PDF | Only if `wwwroot/files/cv.pdf` exists | If the button is missing, the file is **not** published (it was 404). |

Home shows **two** featured items. Everything else is grouped under Labs.

## Live surfaces

| Proof | URL | You can load | Hard limit |
|-------|-----|--------------|------------|
| Portfolio | https://portfolio.galasse.dev | Full site, recruiter map | Read-mostly |
| TOTE demo | https://demo.tote.galasse.dev | Turnstile → ephemeral ADMIN session | ~2h workspace; reports/users/approvals hidden |
| Pipeview | https://pipeview.galasse.dev | Last real run | Dispatch gated; run may be red |
| Edge Labs | https://edge.galasse.dev/ | Analyze / Coach after Turnstile | Playground. Gemini **not** configured. Not LLMOps. |
| AWS Static (human) | https://portfolio.galasse.dev/Labs/Static | What OAC proves | Raw HTML is two paragraphs |
| AWS Ops (human) | https://portfolio.galasse.dev/Labs/Ops | Table of last pings | Raw JSON still public; not Grafana |
| Edge Status | Worker + pill | Region / cf-ray when wired | Fallback is ASP.NET if the Worker URL is empty |

**Still outside a green Pipeview belt until CI is green:**

1. **CV PDF** — this agent cannot read `C:\Users\...`. Drop `src/Portfolio/wwwroot/files/cv.pdf` into the repo (or attach the file in chat).
2. **Pipeview belt** — last `live-demo.yml` failed on Biome quotes in CSS. Fix is in `pipeline-pulse` (format + push + dispatch live-demo).
3. **Production roll of this site** — push `main` to trigger Actions → ECR → Ansible.

## What this profile is not claiming

- AI developer / LLMOps as a role
- Helm as TOTE production (chart is demonstrative; Compose on EC2)
- Public Grafana
- Gemini on Edge Labs

## Repo index

| Repo | Role |
|------|------|
| [dangalasse/portfolio](https://github.com/dangalasse/portfolio) | This site, Ansible, OTEL, Always Free ops lab |
| [dangalasse/TOTE](https://github.com/dangalasse/TOTE) | Product |
| [dangalasse/pipeline-pulse](https://github.com/dangalasse/pipeline-pulse) | CI/CD proof |
| [dangalasse/aws-static-demo](https://github.com/dangalasse/aws-static-demo) | Terraform S3+CloudFront |
| [dangalasse/edge-labs](https://github.com/dangalasse/edge-labs) | Inference playground |
