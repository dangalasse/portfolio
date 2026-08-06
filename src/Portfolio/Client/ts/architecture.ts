/**
 * n8n-inspired architecture canvas: animated edges, hover paths, click drawer.
 */

interface ArchEdgeDto {
  from: string;
  to: string;
  label?: string | null;
}

interface ArchNodeDto {
  id: string;
  label: string;
  subtitle: string;
  plain: string;
  recruiter: string;
  snippet: string;
  repoUrl?: string | null;
  color: string;
}

interface ArchI18n {
  copy: string;
  copied: string;
  close: string;
}

function prefersReducedMotion(): boolean {
  return window.matchMedia("(prefers-reduced-motion: reduce)").matches;
}

function nodeAnchor(
  el: HTMLElement,
  root: DOMRect,
  side: "out" | "in",
): { x: number; y: number } {
  const r = el.getBoundingClientRect();
  const y = r.top - root.top + r.height / 2;
  const x =
    side === "out"
      ? r.right - root.left - 2
      : r.left - root.left + 2;
  return { x, y };
}

function parseJson<T>(el: HTMLScriptElement | null): T | null {
  if (!el?.textContent) {
    return null;
  }
  try {
    return JSON.parse(el.textContent) as T;
  } catch {
    return null;
  }
}

function clearHighlight(root: HTMLElement): void {
  root.querySelectorAll(".arch-node.is-hot, .arch-node.is-dim").forEach((n) => {
    n.classList.remove("is-hot", "is-dim");
  });
  root.querySelectorAll(".arch-wire.is-hot").forEach((w) => w.classList.remove("is-hot"));
}

function highlightNeighborhood(section: HTMLElement, id: string): void {
  const related = new Set<string>([id]);
  section.querySelectorAll<SVGPathElement>(".arch-wire").forEach((wire) => {
    const match = wire.dataset.from === id || wire.dataset.to === id;
    wire.classList.toggle("is-hot", match);
    if (match) {
      related.add(wire.dataset.from ?? "");
      related.add(wire.dataset.to ?? "");
    }
  });
  section.querySelectorAll<HTMLElement>("[data-arch-node]").forEach((n) => {
    const nid = n.dataset.archNode ?? "";
    n.classList.toggle("is-hot", related.has(nid));
    n.classList.toggle("is-dim", !related.has(nid));
  });
}

function drawFlow(section: HTMLElement): void {
  const canvas = section.querySelector<HTMLElement>("[data-arch-canvas]");
  const svg = section.querySelector<SVGSVGElement>("[data-arch-wires]");
  const edges = parseJson<ArchEdgeDto[]>(
    section.querySelector<HTMLScriptElement>("[data-arch-edges]"),
  );
  if (!canvas || !svg || !edges) {
    return;
  }

  const rootBox = canvas.getBoundingClientRect();
  svg.setAttribute("width", String(rootBox.width));
  svg.setAttribute("height", String(Math.max(rootBox.height, 120)));
  svg.setAttribute("viewBox", `0 0 ${rootBox.width} ${Math.max(rootBox.height, 120)}`);
  svg.replaceChildren();

  const ns = "http://www.w3.org/2000/svg";
  const reduce = prefersReducedMotion();

  for (const edge of edges) {
    const fromEl = section.querySelector<HTMLElement>(`[data-arch-node="${edge.from}"]`);
    const toEl = section.querySelector<HTMLElement>(`[data-arch-node="${edge.to}"]`);
    if (!fromEl || !toEl) {
      continue;
    }

    const a = nodeAnchor(fromEl, rootBox, "out");
    const b = nodeAnchor(toEl, rootBox, "in");
    const dx = Math.max(40, Math.abs(b.x - a.x) * 0.45);
    const path = document.createElementNS(ns, "path");
    const d = `M ${a.x} ${a.y} C ${a.x + dx} ${a.y}, ${b.x - dx} ${b.y}, ${b.x} ${b.y}`;

    path.setAttribute("d", d);
    path.setAttribute("class", "arch-wire");
    path.dataset.from = edge.from;
    path.dataset.to = edge.to;
    if (edge.label) {
      path.dataset.label = edge.label;
    }

    const color = fromEl.dataset.archColor || "#3dd6c6";
    path.style.setProperty("--wire-color", color);
    svg.appendChild(path);

    if (!reduce) {
      path.classList.add("is-animated");
    } else {
      path.classList.add("is-drawn");
    }
  }
}

function openDrawer(section: HTMLElement, node: ArchNodeDto): void {
  const drawer = section.querySelector<HTMLElement>("[data-arch-drawer]");
  if (!drawer) {
    return;
  }
  const title = drawer.querySelector<HTMLElement>("[data-arch-drawer-title]");
  const sub = drawer.querySelector<HTMLElement>("[data-arch-drawer-sub]");
  const plain = drawer.querySelector<HTMLElement>("[data-arch-drawer-plain]");
  const recruiter = drawer.querySelector<HTMLElement>("[data-arch-drawer-recruiter]");
  const snippet = drawer.querySelector<HTMLElement>("[data-arch-drawer-snippet]");
  const repoWrap = drawer.querySelector<HTMLElement>(".arch-drawer-repo");
  const repo = drawer.querySelector<HTMLAnchorElement>("[data-arch-drawer-repo]");

  if (title) title.textContent = node.label;
  if (sub) sub.textContent = node.subtitle;
  if (plain) plain.textContent = node.plain || "—";
  if (recruiter) recruiter.textContent = node.recruiter || "—";
  if (snippet) snippet.textContent = node.snippet || "—";

  if (repo && repoWrap) {
    if (node.repoUrl) {
      repo.href = node.repoUrl;
      repoWrap.hidden = false;
    } else {
      repoWrap.hidden = true;
    }
  }

  drawer.hidden = false;
  drawer.classList.add("is-open");
  section.classList.add("has-drawer");
}

function closeDrawer(section: HTMLElement): void {
  const drawer = section.querySelector<HTMLElement>("[data-arch-drawer]");
  if (!drawer) {
    return;
  }
  drawer.classList.remove("is-open");
  drawer.hidden = true;
  section.classList.remove("has-drawer");
  clearHighlight(section);
}

function bindInteractions(section: HTMLElement): void {
  const nodes = parseJson<ArchNodeDto[]>(
    section.querySelector<HTMLScriptElement>("[data-arch-nodes]"),
  );
  const i18n = parseJson<ArchI18n>(
    section.querySelector<HTMLScriptElement>("[data-arch-i18n]"),
  );
  const byId = new Map((nodes ?? []).map((n) => [n.id, n]));

  section.querySelectorAll<HTMLElement>("[data-arch-node]").forEach((nodeEl) => {
    const id = nodeEl.dataset.archNode;
    if (!id) {
      return;
    }

    nodeEl.addEventListener("mouseenter", () => highlightNeighborhood(section, id));
    nodeEl.addEventListener("mouseleave", () => {
      if (!section.classList.contains("has-drawer")) {
        clearHighlight(section);
      }
    });
    nodeEl.addEventListener("focus", () => highlightNeighborhood(section, id));
    nodeEl.addEventListener("click", () => {
      const data = byId.get(id);
      if (!data) {
        return;
      }
      highlightNeighborhood(section, id);
      openDrawer(section, data);
    });
  });

  section
    .querySelector<HTMLElement>("[data-arch-drawer-close]")
    ?.addEventListener("click", () => closeDrawer(section));

  const copyBtn = section.querySelector<HTMLButtonElement>("[data-arch-drawer-copy]");
  copyBtn?.addEventListener("click", async () => {
    const code = section.querySelector<HTMLElement>("[data-arch-drawer-snippet]")?.textContent ?? "";
    try {
      await navigator.clipboard.writeText(code);
      if (copyBtn && i18n) {
        copyBtn.textContent = i18n.copied;
        window.setTimeout(() => {
          copyBtn.textContent = i18n.copy;
        }, 1400);
      }
    } catch {
      /* ignore */
    }
  });

  document.addEventListener("keydown", (ev) => {
    if (ev.key === "Escape" && section.classList.contains("has-drawer")) {
      closeDrawer(section);
    }
  });
}

function bindIconFallbacks(section: HTMLElement): void {
  section.querySelectorAll<HTMLImageElement>(".arch-icon-img").forEach((img) => {
    img.addEventListener("error", () => {
      img.style.display = "none";
      const fallback = img.nextElementSibling;
      if (fallback instanceof HTMLElement) {
        fallback.style.display = "inline";
      }
    });
  });
}

export function initArchitectureFlows(): void {
  const sections = document.querySelectorAll<HTMLElement>("[data-arch-flow]");
  if (sections.length === 0) {
    return;
  }

  const redraw = (): void => {
    sections.forEach((section) => drawFlow(section));
  };

  sections.forEach((section) => {
    bindIconFallbacks(section);
    bindInteractions(section);
    drawFlow(section);
  });

  window.addEventListener("resize", () => {
    window.requestAnimationFrame(redraw);
  });

  window.addEventListener("load", () => {
    window.setTimeout(redraw, 120);
  });
}
