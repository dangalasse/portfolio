namespace Portfolio.Models;

/// <summary>Node in a per-project architecture flow.</summary>
public sealed class ArchNode
{
    public required string Id { get; init; }
    public required string Label { get; init; }
    public required string Subtitle { get; init; }
    /// <summary>simpleicons.org slug (e.g. nodedotjs, amazonwebservices).</summary>
    public required string Icon { get; init; }
    public required string Color { get; init; }
    public required string Layer { get; init; }
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
