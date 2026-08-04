namespace Portfolio.Models;

public sealed class SiteProfile
{
    public required string Name { get; init; }
    public required string Role { get; init; }
    public required string Tagline { get; init; }
    public required string Location { get; init; }
    public required string Email { get; init; }
    public required string LinkedInUrl { get; init; }
    public required string GitHubUrl { get; init; }
    public required string SiteUrl { get; init; }
    public string? CvPdfPath { get; init; }
}

public sealed class ProjectItem
{
    public required string Slug { get; init; }
    public required string Title { get; init; }
    public required string Summary { get; init; }
    public required string Description { get; init; }
    public required IReadOnlyList<string> Stack { get; init; }
    public required string Accent { get; init; }
    public string? LiveUrl { get; init; }
    public string? RepoUrl { get; init; }
    public string? DemoNote { get; init; }
    public bool Featured { get; init; }
}

public sealed class LabIndicator
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public required string Provider { get; init; }
    public required string Description { get; init; }
    public required string Proof { get; init; }
    public string? DocsUrl { get; init; }
}
