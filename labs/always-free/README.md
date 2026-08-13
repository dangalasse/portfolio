# AWS Always Free ops lab

Public probe of live labs using **Lambda Function URL + DynamoDB + EventBridge + KMS `GenerateRandom`**.

No customer-managed KMS key (that would be **$1/month**). No API Gateway, NAT, or extra EC2.

## Routes

Live: https://4notqcazblkzqyd3avwjrkxtki0grnho.lambda-url.sa-east-1.on.aws/

| Method | Path | What |
|--------|------|------|
| GET | `/status` | Latest check per lab (DynamoDB) |
| POST | `/probe` | Run checks now and persist |
| POST | `/ack` | `{"lab":"edge","note":"looking"}` |
| GET | `/kms/random` | 32 bytes from KMS HSM (fingerprint only) |

EventBridge invokes the same function every 5 minutes (`source=aws.events`).

## Deploy

```bash
cd labs/always-free
./deploy.sh
```

Requires AWS CLI credentials and an existing **sa-east-1** bucket for the zip
(default: `cdk-hnb659fds-assets-168288133533-sa-east-1`). Lambda code must live in the same region.

## Cost envelope

- Lambda: 128 MB, 15 s, ~288 scheduled invokes/day + occasional human hits
- DynamoDB: on-demand + 7-day TTL
- CloudWatch Logs: 7-day retention, one-line EMF
- KMS: `GenerateRandom` only (Always Free 20k req/month)
