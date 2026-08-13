using Portfolio.Models;

namespace Portfolio.Data;

/// <summary>Runtime facts the UI must not lie about (CV file, edge probe URL).</summary>
public static class SiteFacts
{
    public const string CvRelativePath = "files/cv.pdf";

    public const string EdgeStatusWorkerUrl =
        "https://portfolio-edge-status.dantonguerragalasse.workers.dev";

    public const string AwsOpsStatusUrl =
        "https://4notqcazblkzqyd3avwjrkxtki0grnho.lambda-url.sa-east-1.on.aws/status";

    public const string AwsOpsKmsUrl =
        "https://4notqcazblkzqyd3avwjrkxtki0grnho.lambda-url.sa-east-1.on.aws/kms/random";

    public const string AwsStaticUrl = "https://static.galasse.dev/";

    public static bool HasCv(HttpContext http) =>
        http.Items["HasCv"] is true;

    public static string? CvUrl(HttpContext http, SiteProfile profile) =>
        HasCv(http) ? profile.CvPdfPath : null;

    public static SiteProfile WithPublishedCv(HttpContext http, SiteProfile profile) =>
        profile with { CvPdfPath = CvUrl(http, profile) };

    public static string? ResolveEdgeStatusUrl(IConfiguration config)
    {
        var env = Environment.GetEnvironmentVariable("EDGE_STATUS_URL");
        if (!string.IsNullOrWhiteSpace(env))
        {
            return env.Trim();
        }

        var cfg = config["EdgeStatusUrl"];
        return string.IsNullOrWhiteSpace(cfg) ? null : cfg.Trim();
    }
}
