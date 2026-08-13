"""Always Free ops lab: probe + DynamoDB + KMS GenerateRandom + CW EMF.

No customer-managed KMS key (that would be $1/mo). Entropy comes from
kms:GenerateRandom (counts toward the 20k Always Free KMS requests).
"""

from __future__ import annotations

import json
import os
import time
import urllib.error
import urllib.request
from datetime import datetime, timedelta, timezone
from decimal import Decimal
from typing import Any

import boto3
from boto3.dynamodb.conditions import Key

TABLE_NAME = os.environ["TABLE_NAME"]
TARGETS_RAW = os.environ.get(
    "TARGETS",
    "portfolio|https://portfolio.galasse.dev/api/status,"
    "pipeview|https://pipeview.galasse.dev/,"
    "edge|https://edge.galasse.dev/health,"
    "static|https://static.galasse.dev/",
)
TTL_DAYS = int(os.environ.get("TTL_DAYS", "7"))
TIMEOUT_S = float(os.environ.get("PROBE_TIMEOUT", "4"))

ddb = boto3.resource("dynamodb")
table = ddb.Table(TABLE_NAME)
kms = boto3.client("kms")


def _now() -> datetime:
    return datetime.now(timezone.utc)


def _iso(dt: datetime) -> str:
    return dt.isoformat().replace("+00:00", "Z")


def _targets() -> list[tuple[str, str]]:
    out: list[tuple[str, str]] = []
    for part in TARGETS_RAW.split(","):
        part = part.strip()
        if not part or "|" not in part:
            continue
        name, url = part.split("|", 1)
        out.append((name.strip(), url.strip()))
    return out


def _respond(status: int, body: Any) -> dict[str, Any]:
    return {
        "statusCode": status,
        "headers": {
            "content-type": "application/json; charset=utf-8",
            "access-control-allow-origin": "*",
            "access-control-allow-methods": "GET,POST,OPTIONS",
            "access-control-allow-headers": "content-type",
            "cache-control": "no-store",
        },
        "body": "" if body == "" else json.dumps(body, default=_json_default),
    }


def _json_default(value: Any) -> Any:
    if isinstance(value, Decimal):
        return int(value) if value % 1 == 0 else float(value)
    raise TypeError(type(value))


def _emf(lab: str, up: int, latency_ms: int) -> None:
    # One-line structured log + Embedded Metric Format (no extra custom-metric bill).
    payload = {
        "_aws": {
            "Timestamp": int(time.time() * 1000),
            "CloudWatchMetrics": [
                {
                    "Namespace": "Galasse/Labs",
                    "Dimensions": [["Lab"]],
                    "Metrics": [
                        {"Name": "Up", "Unit": "Count"},
                        {"Name": "LatencyMs", "Unit": "Milliseconds"},
                    ],
                }
            ],
        },
        "Lab": lab,
        "Up": up,
        "LatencyMs": latency_ms,
    }
    print(json.dumps(payload, separators=(",", ":")))


def _probe_one(name: str, url: str) -> dict[str, Any]:
    started = time.perf_counter()
    status = 0
    ok = False
    err = None
    try:
        req = urllib.request.Request(url, method="GET", headers={"user-agent": "galasse-ops-labs/1.0"})
        with urllib.request.urlopen(req, timeout=TIMEOUT_S) as resp:
            status = int(resp.status)
            ok = 200 <= status < 400
    except urllib.error.HTTPError as exc:
        status = int(exc.code)
        err = f"http_{status}"
    except Exception as exc:  # noqa: BLE001 — surface timeout/dns to the lab JSON
        err = type(exc).__name__
    latency_ms = int((time.perf_counter() - started) * 1000)
    _emf(name, 1 if ok else 0, latency_ms)
    now = _now()
    item = {
        "pk": f"LAB#{name}",
        "sk": f"TS#{_iso(now)}",
        "lab": name,
        "url": url,
        "ok": ok,
        "httpStatus": status,
        "latencyMs": latency_ms,
        "error": err,
        "checkedAt": _iso(now),
        "expireAt": int((now + timedelta(days=TTL_DAYS)).timestamp()),
    }
    table.put_item(Item=item)
    return {
        "lab": name,
        "url": url,
        "ok": ok,
        "httpStatus": status,
        "latencyMs": latency_ms,
        "error": err,
        "checkedAt": item["checkedAt"],
    }


def run_probe() -> dict[str, Any]:
    results = [_probe_one(name, url) for name, url in _targets()]
    return {
        "ok": all(r["ok"] for r in results) if results else False,
        "checkedAt": _iso(_now()),
        "results": results,
    }


def latest_status() -> dict[str, Any]:
    results = []
    for name, url in _targets():
        resp = table.query(
            KeyConditionExpression=Key("pk").eq(f"LAB#{name}"),
            ScanIndexForward=False,
            Limit=1,
        )
        items = resp.get("Items") or []
        if items:
            row = items[0]
            results.append(
                {
                    "lab": name,
                    "url": url,
                    "ok": bool(row.get("ok")),
                    "httpStatus": int(row.get("httpStatus") or 0),
                    "latencyMs": int(row.get("latencyMs") or 0),
                    "error": row.get("error"),
                    "checkedAt": row.get("checkedAt"),
                }
            )
        else:
            results.append(
                {
                    "lab": name,
                    "url": url,
                    "ok": None,
                    "httpStatus": 0,
                    "latencyMs": 0,
                    "error": "no_sample_yet",
                    "checkedAt": None,
                }
            )
    return {
        "ok": all(r["ok"] is True for r in results) if results else False,
        "service": "aws-ops-labs",
        "checkedAt": _iso(_now()),
        "results": results,
    }


def ack(event: dict[str, Any]) -> dict[str, Any]:
    try:
        body = json.loads(event.get("body") or "{}")
    except json.JSONDecodeError:
        return {"error": "invalid_json"}
    lab = str(body.get("lab") or "").strip()
    note = str(body.get("note") or "acked")[:280]
    if not lab:
        return {"error": "lab_required"}
    now = _now()
    table.put_item(
        Item={
            "pk": f"ACK#{lab}",
            "sk": f"TS#{_iso(now)}",
            "lab": lab,
            "note": note,
            "ackedAt": _iso(now),
            "expireAt": int((now + timedelta(days=TTL_DAYS)).timestamp()),
        }
    )
    return {"ok": True, "lab": lab, "note": note, "ackedAt": _iso(now)}


def kms_demo() -> dict[str, Any]:
    # Always Free: GenerateRandom does not need a CMK (CMK = $1/mo).
    resp = kms.generate_random(NumberOfBytes=32)
    blob = resp["Plaintext"]
    return {
        "ok": True,
        "provider": "kms",
        "api": "GenerateRandom",
        "bytes": 32,
        "fingerprint": blob[:8].hex(),
        "note": "HSM entropy only — no customer-managed key (avoids $1/mo CMK).",
        "checkedAt": _iso(_now()),
    }


def _path_method(event: dict[str, Any]) -> tuple[str, str]:
    http = (event.get("requestContext") or {}).get("http") or {}
    method = (http.get("method") or event.get("httpMethod") or "GET").upper()
    path = event.get("rawPath") or http.get("path") or event.get("path") or "/"
    return path.rstrip("/") or "/", method


def lambda_handler(event: dict[str, Any], _context: Any) -> dict[str, Any]:
    if event.get("source") == "aws.events":
        return run_probe()

    path, method = _path_method(event)
    if method == "OPTIONS":
        return _respond(204, "")

    if method == "GET" and path in ("/", "/health", "/status"):
        return _respond(200, latest_status())
    if method == "POST" and path == "/probe":
        return _respond(200, run_probe())
    if method == "POST" and path == "/ack":
        result = ack(event)
        return _respond(400 if "error" in result else 200, result)
    if method in ("GET", "POST") and path in ("/kms", "/kms/random"):
        return _respond(200, kms_demo())

    return _respond(
        404,
        {
            "error": "not_found",
            "routes": ["GET /status", "POST /probe", "POST /ack", "GET /kms/random"],
        },
    )
