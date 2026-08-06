namespace Portfolio.Models;

/// <summary>Node in a per-project architecture flow (n8n-inspired canvas).</summary>
public sealed class ArchNode
{
    public required string Id { get; init; }
    public required string Label { get; init; }
    public required string Subtitle { get; init; }
    /// <summary>simpleicons.org slug (e.g. nodedotjs, amazonwebservices).</summary>
    public required string Icon { get; init; }
    public required string Color { get; init; }
    /// <summary>Logical swimlane id (client, edge, …) — used for legend grouping.</summary>
    public required string Layer { get; init; }
    /// <summary>0-based column in the n8n-style grid (left → right).</summary>
    public int Column { get; init; }
    /// <summary>0-based row within the column (top → bottom).</summary>
    public int Row { get; init; }
    /// <summary>Plain-language explanation for non-technical readers.</summary>
    public string PlainExplain { get; init; } = "";
    /// <summary>What this proves on a CV / for recruiters.</summary>
    public string RecruiterDetail { get; init; } = "";
    /// <summary>Short code snippet (YAML/TF/TS/C#) relevant to this node.</summary>
    public string CodeSnippet { get; init; } = "";
    /// <summary>Optional deep-link into source.</summary>
    public string? RepoUrl { get; init; }
}

public sealed class ArchEdge
{
    public required string From { get; init; }
    public required string To { get; init; }
    public string? Label { get; init; }
}

public sealed class ArchitectureFlow
{
    public required string ProjectSlug { get; init; }
    public required string Title { get; init; }
    public required string Caption { get; init; }
    public required IReadOnlyList<string> LayerOrder { get; init; }
    public required IReadOnlyList<ArchNode> Nodes { get; init; }
    public required IReadOnlyList<ArchEdge> Edges { get; init; }
}
