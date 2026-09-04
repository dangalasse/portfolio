namespace Portfolio.Security;

/// <summary>
/// Browser hardening for every response. HSTS is set even on HTTP because
/// Caddy terminates TLS in front of Kestrel.
/// </summary>
public static class SecurityHeaders
{
    public const string ContentSecurityPolicy =
        "default-src 'self'; " +
        "script-src 'self' 'unsafe-inline'; " +
        "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com; " +
        "font-src 'self' https://fonts.gstatic.com; " +
        "img-src 'self' data: https://cdn.simpleicons.org; " +
        "connect-src 'self' https://*.galasse.dev https://*.workers.dev; " +
        "frame-ancestors 'self'; " +
        "base-uri 'self'; " +
        "form-action 'self'; " +
        "object-src 'none'";

    public static void Apply(HttpResponse response)
    {
        var headers = response.Headers;
        headers["X-Content-Type-Options"] = "nosniff";
        headers["X-Frame-Options"] = "SAMEORIGIN";
        headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
        headers["Permissions-Policy"] = "camera=(), microphone=(), geolocation=()";
        headers["Content-Security-Policy"] = ContentSecurityPolicy;
        headers["Strict-Transport-Security"] = "max-age=31536000; includeSubDomains; preload";
        headers.Remove("Server");
        headers.Remove("X-Powered-By");
    }
}
