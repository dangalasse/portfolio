/**
 * Draws SVG connectors between architecture nodes and highlights paths.
 */

interface ArchEdgeDto {
  from: string;
  to: string;
  label?: string | null;
}

function prefersReducedMotion(): boolean {
  return window.matchMedia("(prefers-reduced-motion: reduce)").matches;
}

function nodeCenter(el: HTMLElement, root: DOMRect): { x: number; y: number } {
  const r = el.getBoundingClientRect();
  return {
    x: r.left - root.left + r.width / 2,
    y: r.top - root.top + r.height / 2,
  };
}

function clearHighlight(root: HTMLElement): void {
  root.querySelectorAll(".arch-node.is-hot, .arch-node.is-dim").forEach((n) => {
    n.classList.remove("is-hot", "is-dim");
  });
  root.querySelectorAll(".arch-wire.is-hot").forEach((w) => w.classList.remove("is-hot"));
  root.querySelectorAll(".arch-legend-item.is-hot").forEach((i) => i.classList.remove("is-hot"));
}

function highlightEdge(root: HTMLElement, from: string, to: string): void {
  const nodes = root.querySelectorAll<HTMLElement>("[data-arch-node]");
  nodes.forEach((node) => {
    const id = node.dataset.archNode;
    if (id === from || id === to) {
      node.classList.add("is-hot");
      node.classList.remove("is-dim");
    } else {
      node.classList.add("is-dim");
      node.classList.remove("is-hot");
    }
  });

  root.querySelectorAll<SVGPathElement>(".arch-wire").forEach((wire) => {
    const match = wire.dataset.from === from && wire.dataset.to === to;
    wire.classList.toggle("is-hot", match);
  });

  root.querySelectorAll<HTMLElement>(".arch-legend-item").forEach((item) => {
    const match =
      item.dataset.archEdgeFrom === from && item.dataset.archEdgeTo === to;
    item.classList.toggle("is-hot", match);
  });
}

function drawFlow(section: HTMLElement): void {
  const canvas = section.querySelector<HTMLElement>("[data-arch-canvas]");
  const svg = section.querySelector<SVGSVGElement>("[data-arch-wires]");
  const edgesJson = section.querySelector<HTMLScriptElement>("[data-arch-edges]");
  if (!canvas || !svg || !edgesJson?.textContent) {
    return;
  }

  let edges: ArchEdgeDto[] = [];
  try {
    edges = JSON.parse(edgesJson.textContent) as ArchEdgeDto[];
  } catch {
    return;
  }

  const rootBox = canvas.getBoundingClientRect();
  svg.setAttribute("width", String(rootBox.width));
  svg.setAttribute("height", String(rootBox.height));
  svg.setAttribute("viewBox", `0 0 ${rootBox.width} ${rootBox.height}`);
  svg.replaceChildren();

  const ns = "http://www.w3.org/2000/svg";

  for (const edge of edges) {
    const fromEl = section.querySelector<HTMLElement>(`[data-arch-node="${edge.from}"]`);
    const toEl = section.querySelector<HTMLElement>(`[data-arch-node="${edge.to}"]`);
    if (!fromEl || !toEl) {
      continue;
    }

    const a = nodeCenter(fromEl, rootBox);
    const b = nodeCenter(toEl, rootBox);
    const midY = (a.y + b.y) / 2;
    const path = document.createElementNS(ns, "path");
    const d =
      Math.abs(b.y - a.y) < 12
        ? `M ${a.x} ${a.y} L ${b.x} ${b.y}`
        : `M ${a.x} ${a.y} C ${a.x} ${midY}, ${b.x} ${midY}, ${b.x} ${b.y}`;

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

    if (!prefersReducedMotion()) {
      const length = path.getTotalLength();
      path.style.strokeDasharray = String(length);
      path.style.strokeDashoffset = String(length);
      requestAnimationFrame(() => {
        path.classList.add("is-drawn");
        path.style.strokeDashoffset = "0";
      });
    } else {
      path.classList.add("is-drawn");
    }
  }
}

function bindLegend(section: HTMLElement): void {
  section.querySelectorAll<HTMLElement>(".arch-legend-item").forEach((item) => {
    const from = item.dataset.archEdgeFrom;
    const to = item.dataset.archEdgeTo;
    if (!from || !to) {
      return;
    }

    item.addEventListener("mouseenter", () => highlightEdge(section, from, to));
    item.addEventListener("focus", () => highlightEdge(section, from, to));
    item.addEventListener("mouseleave", () => clearHighlight(section));
    item.addEventListener("blur", () => clearHighlight(section));
    item.addEventListener("click", () => highlightEdge(section, from, to));
  });

  section.querySelectorAll<HTMLElement>("[data-arch-node]").forEach((node) => {
    node.addEventListener("mouseenter", () => {
      const id = node.dataset.archNode;
      if (!id) {
        return;
      }
      const related = new Set<string>([id]);
      section.querySelectorAll<SVGPathElement>(".arch-wire").forEach((wire) => {
        if (wire.dataset.from === id || wire.dataset.to === id) {
          related.add(wire.dataset.from ?? "");
          related.add(wire.dataset.to ?? "");
          wire.classList.add("is-hot");
        } else {
          wire.classList.remove("is-hot");
        }
      });
      section.querySelectorAll<HTMLElement>("[data-arch-node]").forEach((n) => {
        const nid = n.dataset.archNode ?? "";
        n.classList.toggle("is-hot", related.has(nid));
        n.classList.toggle("is-dim", !related.has(nid));
      });
    });
    node.addEventListener("mouseleave", () => clearHighlight(section));
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
    bindLegend(section);
    drawFlow(section);
  });

  window.addEventListener("resize", () => {
    window.requestAnimationFrame(redraw);
  });

  window.addEventListener("load", () => {
    window.setTimeout(redraw, 120);
  });
}
