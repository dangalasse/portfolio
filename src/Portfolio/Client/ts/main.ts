/**
 * Subtle motion and live lab indicators for the portfolio.
 */

import { initArchitectureFlows } from "./architecture";

function initReveal(): void {
  // Content is visible in CSS. Keep the observer only so existing markup stays valid.
  document.querySelectorAll<HTMLElement>(".reveal").forEach((node) => {
    node.classList.add("is-visible");
  });
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
        ? isEn
          ? "Cloudflare Worker"
          : "Worker Cloudflare"
        : isEn
          ? "ASP.NET fallback"
          : "Fallback ASP.NET",
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

document.addEventListener("DOMContentLoaded", () => {
  initYear();
  initMobileNav();
  initReveal();
  initArchitectureFlows();
  void initLabIndicators();
});
