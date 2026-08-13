namespace Portfolio.Models;

public enum ProjectKind
{
    /// <summary>A real product with domain, not a lab.</summary>
    Product,

    /// <summary>Live proof of an operations practice (CI/CD, etc.).</summary>
    Proof,

    /// <summary>Always Free / edge evidence. Not the main work.</summary>
    Lab,
}

public sealed record SiteProfile
{
    public required string Name { get; init; }
    public required string Role { get; init; }
    public string Tagline { get; init; } = "";
    public string TaglinePtBr { get; init; } = "";
    public string TaglineEnUs { get; init; } = "";
    public required string Location { get; init; }
    public required string Email { get; init; }
    public required string LinkedInUrl { get; init; }
    public required string GitHubUrl { get; init; }
    public required string SiteUrl { get; init; }
    public string? CvPdfPath { get; init; }
}

public sealed record ProjectItem
{
    public required string Slug { get; init; }
    public required string Title { get; init; }
    public string? TitleEn { get; init; }
    public required string Summary { get; init; }
    public required string Description { get; init; }
    public string? SummaryEn { get; init; }
    public string? DescriptionEn { get; init; }
    public required IReadOnlyList<string> Stack { get; init; }
    public required string Accent { get; init; }
    public string? LiveUrl { get; init; }
    public string? RepoUrl { get; init; }
    public string? DemoNote { get; init; }
    public string? DemoNoteEn { get; init; }
    public bool Featured { get; init; }
    public ProjectKind Kind { get; init; } = ProjectKind.Lab;
    public string? Problem { get; init; }
    public string? ProblemEn { get; init; }
    public string? HonestLimit { get; init; }
    public string? HonestLimitEn { get; init; }
    public string? VerifyHint { get; init; }
    public string? VerifyHintEn { get; init; }
    public string? LiveCta { get; init; }
    public string? LiveCtaEn { get; init; }
}

public sealed record LabIndicator
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public required string Provider { get; init; }
    public required string Description { get; init; }
    public string? DescriptionEn { get; init; }
    public required string Proof { get; init; }
    public string? ProofEn { get; init; }
    public string? DocsUrl { get; init; }
}
