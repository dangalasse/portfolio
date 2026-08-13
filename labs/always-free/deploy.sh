#!/usr/bin/env bash
set -euo pipefail

ROOT="$(cd "$(dirname "$0")" && pwd)"
REGION="${AWS_REGION:-sa-east-1}"
BUCKET="${CODE_BUCKET:-cdk-hnb659fds-assets-168288133533-sa-east-1}"
KEY="always-free/ops-labs.zip"
STACK="galasse-ops-labs"
ZIP="/tmp/ops-labs.zip"

python3 - <<PY
import zipfile
from pathlib import Path
src = Path("$ROOT/src/handler.py")
out = Path("$ZIP")
out.unlink(missing_ok=True)
with zipfile.ZipFile(out, "w", zipfile.ZIP_DEFLATED) as zf:
    zf.write(src, "handler.py")
PY
aws s3 cp "$ZIP" "s3://${BUCKET}/${KEY}" --region "$REGION" --only-show-errors

aws cloudformation deploy \
  --region "$REGION" \
  --stack-name "$STACK" \
  --template-file "$ROOT/template.yaml" \
  --capabilities CAPABILITY_NAMED_IAM \
  --parameter-overrides "CodeBucket=${BUCKET}" "CodeKey=${KEY}"

aws cloudformation describe-stacks \
  --region "$REGION" \
  --stack-name "$STACK" \
  --query 'Stacks[0].Outputs' \
  --output table
