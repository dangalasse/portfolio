using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Data;

namespace Portfolio.Pages.Labs;

public class OpsModel : PageModel
{
    private readonly IHttpClientFactory _http;

    public OpsModel(IHttpClientFactory http)
    {
        _http = http;
    }

    public string RawUrl { get; } = SiteFacts.AwsOpsStatusUrl;

    public string KmsUrl { get; } = SiteFacts.AwsOpsKmsUrl;

    public string? FetchError { get; private set; }

    public DateTimeOffset? CheckedAt { get; private set; }

    public bool? Ok { get; private set; }

    public IReadOnlyList<ProbeRow> Rows { get; private set; } = [];

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        try
        {
            var client = _http.CreateClient("ops-labs");
            using var response = await client.GetAsync(RawUrl, cancellationToken);
            var json = await response.Content.ReadAsStringAsync(cancellationToken);
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            Ok = root.TryGetProperty("ok", out var okEl) && okEl.GetBoolean();
            if (root.TryGetProperty("checkedAt", out var checkedEl)
                && checkedEl.GetString() is { } checkedRaw
                && DateTimeOffset.TryParse(checkedRaw, out var parsed))
            {
                CheckedAt = parsed;
            }

            var rows = new List<ProbeRow>();
            if (root.TryGetProperty("results", out var results) && results.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in results.EnumerateArray())
                {
                    rows.Add(new ProbeRow(
                        Lab: item.TryGetProperty("lab", out var lab) ? lab.GetString() ?? "?" : "?",
                        Url: item.TryGetProperty("url", out var url) ? url.GetString() ?? "" : "",
                        Ok: item.TryGetProperty("ok", out var itemOk) && itemOk.GetBoolean(),
                        HttpStatus: item.TryGetProperty("httpStatus", out var st) && st.TryGetInt32(out var status)
                            ? status
                            : null,
                        LatencyMs: item.TryGetProperty("latencyMs", out var lat) && lat.TryGetInt32(out var ms)
                            ? ms
                            : null,
                        Error: item.TryGetProperty("error", out var err) && err.ValueKind == JsonValueKind.String
                            ? err.GetString()
                            : null,
                        CheckedAt: item.TryGetProperty("checkedAt", out var at) ? at.GetString() : null));
                }
            }

            Rows = rows;
        }
        catch
        {
            FetchError = "fetch-failed";
        }
    }
}

public sealed record ProbeRow(
    string Lab,
    string Url,
    bool Ok,
    int? HttpStatus,
    int? LatencyMs,
    string? Error,
    string? CheckedAt);
