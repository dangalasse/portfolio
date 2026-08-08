namespace Portfolio.Models;

public enum SocialLinksVariant
{
    /// <summary>Compact icon buttons (header / mobile).</summary>
    Nav,

    /// <summary>Icon row under the hero tagline.</summary>
    Hero,

    /// <summary>Labeled tiles in the presence section.</summary>
    Presence,

    /// <summary>About page action row.</summary>
    About,

    /// <summary>Footer icon cluster.</summary>
    Footer,
}

public sealed record SocialLinksModel
{
    public required SiteProfile Profile { get; init; }
    public required string Locale { get; init; }
    public SocialLinksVariant Variant { get; init; } = SocialLinksVariant.Nav;
}
