/**
 * Cloudflare Worker — edge status for portfolio Labs.
 * Deploy: npx wrangler deploy (from this folder)
 */
export interface Env {}

export default {
  async fetch(request: Request, _env: Env, _ctx: ExecutionContext): Promise<Response> {
    const cf = (request as Request & { cf?: { colo?: string; country?: string } }).cf;
    const ray = request.headers.get("cf-ray") ?? undefined;

    const body = {
      ok: true,
      region: cf?.colo ?? "unknown",
      country: cf?.country ?? null,
      ray,
      service: "portfolio-edge-status",
      checkedAt: new Date().toISOString(),
    };

    return new Response(JSON.stringify(body), {
      headers: {
        "content-type": "application/json; charset=utf-8",
        "access-control-allow-origin": "*",
        "cache-control": "no-store",
      },
    });
  },
};
