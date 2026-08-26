/**
 * Subtle motion and live lab indicators for the portfolio.
 */

import { initArchitectureFlows } from "./architecture";

function prefersReducedMotion(): boolean {
  return window.matchMedia("(prefers-reduced-motion: reduce)").matches;
}

function initReveal(): void {
  const nodes = document.querySelectorAll<HTMLElement>(".reveal");
  if (nodes.length === 0) {
    return;
  }

  if (prefersReducedMotion()) {
    nodes.forEach((node) => node.classList.add("is-visible"));
    return;
  }

  const observer = new IntersectionObserver(
    (entries) => {
      for (const entry of entries) {
        if (entry.isIntersecting) {
          entry.target.classList.add("is-visible");
          observer.unobserve(entry.target);
        }
      }
    },
    { threshold: 0.12, rootMargin: "0px 0px -8% 0px" },
  );

  nodes.forEach((node) => observer.observe(node));
}

interface EdgeStatusPayload {
  ok: boolean;
  region?: string;
  ray?: string;
  latencyMs?: number;
  source: "live" | "simulated";
  checkedAt: string;
}

async function fetchEdgeStatus(): Promise<EdgeStatusPayload> {
  const started = performance.now();
  const endpoint = document.body.dataset.edgeStatusUrl;

  if (endpoint) {
    try {
      const response = await fetch(endpoint, { cache: "no-store" });
      const latencyMs = Math.round(performance.now() - started);
      if (response.ok) {
        const data = (await response.json()) as Record<string, unknown>;
        return {
          ok: true,
          region: typeof data.region === "string" ? data.region : "edge",
          ray: typeof data.ray === "string" ? data.ray : undefined,
          latencyMs,
          source: "live",
          checkedAt: new Date().toISOString(),
        };
      }
    } catch {
      // Fall through to local probe.
    }
  }

  // Local ASP.NET health probe — proves the stack is alive until Cloudflare Worker is wired.
  try {
    const response = await fetch("/api/status", { cache: "no-store" });
    const latencyMs = Math.round(performance.now() - started);
    if (response.ok) {
      const data = (await response.json()) as {
        region?: string;
        runtime?: string;
      };
      return {
        ok: true,
        region: data.region ?? "local",
        ray: data.runtime,
        latencyMs,
        source: "simulated",
        checkedAt: new Date().toISOString(),
      };
    }
  } catch {
    // ignore
  }

  return {
    ok: false,
    source: "simulated",
    checkedAt: new Date().toISOString(),
  };
}

function renderStatus(target: HTMLElement, payload: EdgeStatusPayload): void {
  const locale = document.body.dataset.locale ?? "pt-BR";
  const isEn = locale.toLowerCase().startsWith("en");
  const dot = target.querySelector<HTMLElement>("[data-status-dot]");
  const label = target.querySelector<HTMLElement>("[data-status-label]");
  const meta = target.querySelector<HTMLElement>("[data-status-meta]");

  if (dot) {
    dot.classList.toggle("bg-signal", payload.ok);
    dot.classList.toggle("bg-red-400", !payload.ok);
  }

  if (label) {
    label.textContent = payload.ok ? "Online" : "Offline";
  }

  if (meta) {
    const parts: string[] = [];
    if (payload.region) {
      parts.push(payload.region);
    }
    if (typeof payload.latencyMs === "number") {
      parts.push(`${payload.latencyMs} ms`);
    }
    parts.push(
      payload.source === "live"
        ? "Cloudflare"
        : isEn
          ? "ASP.NET probe"
          : "Probe ASP.NET",
    );
    meta.textContent = parts.join(" · ");
  }
}

async function initLabIndicators(): Promise<void> {
  const cards = document.querySelectorAll<HTMLElement>("[data-lab-status]");
  if (cards.length === 0) {
    return;
  }

  const payload = await fetchEdgeStatus();
  cards.forEach((card) => renderStatus(card, payload));
}

function initYear(): void {
  const el = document.querySelector<HTMLElement>("[data-year]");
  if (el) {
    el.textContent = String(new Date().getFullYear());
  }
}

function initMobileNav(): void {
  const toggle = document.querySelector<HTMLButtonElement>("[data-nav-toggle]");
  const panel = document.querySelector<HTMLElement>("[data-nav-panel]");
  if (!toggle || !panel) {
    return;
  }

  toggle.addEventListener("click", () => {
    const open = panel.dataset.open === "true";
    panel.dataset.open = open ? "false" : "true";
    panel.classList.toggle("hidden", open);
    toggle.setAttribute("aria-expanded", open ? "false" : "true");
  });
}

interface AwsOpsRow {
  lab?: string;
  url?: string;
  ok?: boolean | null;
  httpStatus?: number;
  latencyMs?: number;
  error?: string | null;
  checkedAt?: string | null;
}

interface AwsOpsStatus {
  ok?: boolean;
  checkedAt?: string;
  results?: AwsOpsRow[];
  error?: string;
}

function parseJsonScript<T>(el: HTMLScriptElement | null): T | null {
  if (!el?.textContent) {
    return null;
  }
  try {
    return JSON.parse(el.textContent) as T;
  } catch {
    return null;
  }
}

function escapeHtml(value: string): string {
  return value
    .replaceAll("&", "&amp;")
    .replaceAll("<", "&lt;")
    .replaceAll(">", "&gt;")
    .replaceAll('"', "&quot;");
}

function escapeAttr(value: string): string {
  return escapeHtml(value).replaceAll("'", "&#39;");
}

function initAwsOps(): void {
  const root = document.querySelector<HTMLElement>("[data-aws-ops]");
  if (!root) {
    return;
  }

  const i18n = parseJsonScript<{ evidence: string; failed: string; loading: string }>(
    document.querySelector("script[data-aws-ops-i18n]"),
  ) ?? { evidence: "View evidence", failed: "Could not read status.", loading: "Loading…" };

  const grid = root.querySelector<HTMLElement>("[data-aws-ops-grid]");
  const kmsBtn = root.querySelector<HTMLButtonElement>("[data-aws-ops-kms]");
  const kmsOut = root.querySelector<HTMLElement>("[data-aws-ops-kms-out]");

  const render = (data: AwsOpsStatus): void => {
    if (!grid) {
      return;
    }
    const rows = data.results ?? [];
    if (rows.length === 0) {
      grid.innerHTML = `<p class="font-mono text-sm text-mist">${i18n.failed}</p>`;
      return;
    }
    grid.innerHTML = rows
      .map((row) => {
        const ok = row.ok === true;
        const pending = row.ok == null;
        const tone = pending ? "text-mist" : ok ? "text-signal" : "text-red-400";
        const state = pending ? "—" : ok ? "ok" : "down";
        const when = row.checkedAt
          ? new Date(row.checkedAt).toISOString().replace("T", " ").replace("Z", " UTC")
          : "—";
        const href = row.url
          ? `href="${escapeAttr(row.url)}" target="_blank" rel="noopener noreferrer"`
          : "";
        return `<article class="lab-card">
          <p class="font-mono text-[11px] uppercase tracking-wider text-mist">${escapeHtml(row.lab ?? "lab")}</p>
          <p class="mt-2 font-display text-xl font-semibold ${tone}">${state}</p>
          <dl class="mt-3 space-y-1 font-mono text-xs text-mist">
            <div>HTTP ${Number(row.httpStatus ?? 0)} · ${Number(row.latencyMs ?? 0)} ms</div>
            <div>${escapeHtml(when)}</div>
            ${row.error ? `<div class="text-red-400">${escapeHtml(String(row.error))}</div>` : ""}
          </dl>
          ${row.url ? `<a class="mt-5 inline-block text-sm text-paper underline-offset-4 hover:text-signal hover:underline" ${href}>${escapeHtml(i18n.evidence)}</a>` : ""}
        </article>`;
      })
      .join("");
    grid.className = "mt-10 grid gap-4 md:grid-cols-2";
  };

  void (async () => {
    try {
      const res = await fetch("/api/aws-ops-status", { cache: "no-store" });
      const data = (await res.json()) as AwsOpsStatus;
      if (!res.ok && !data.results) {
        throw new Error(data.error || "status failed");
      }
      render(data);
    } catch {
      if (grid) {
        grid.innerHTML = `<p class="font-mono text-sm text-mist">${i18n.failed}</p>`;
      }
    }
  })();

  kmsBtn?.addEventListener("click", async () => {
    if (!kmsOut) {
      return;
    }
    kmsOut.classList.remove("hidden");
    kmsOut.textContent = i18n.loading;
    try {
      const res = await fetch("/api/aws-ops-kms", { cache: "no-store" });
      const data = (await res.json()) as {
        ok?: boolean;
        api?: string;
        fingerprint?: string;
        bytes?: number;
        note?: string;
        checkedAt?: string;
      };
      kmsOut.textContent = [
        `ok: ${data.ok === true}`,
        `api: ${data.api ?? "GenerateRandom"}`,
        `bytes: ${data.bytes ?? 32}`,
        `fingerprint: ${data.fingerprint ?? "—"}`,
        data.note ?? "",
        data.checkedAt ? `checkedAt: ${data.checkedAt}` : "",
      ]
        .filter(Boolean)
        .join("\n");
    } catch (err) {
      kmsOut.textContent = String(err);
    }
  });
}

document.addEventListener("DOMContentLoaded", () => {
  initYear();
  initMobileNav();
  initReveal();
  initArchitectureFlows();
  void initLabIndicators();
  initAwsOps();
});
