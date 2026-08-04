namespace Portfolio.I18n;

public static class AppLocales
{
    public const string CookieName = "portfolio_locale";
    public const string PtBr = "pt-BR";
    public const string EnUs = "en-US";

    public static bool IsEnglish(string? locale) =>
        locale is not null
        && locale.StartsWith("en", StringComparison.OrdinalIgnoreCase);

    public static string Normalize(string? locale) =>
        IsEnglish(locale) ? EnUs : PtBr;
}

public readonly record struct L(string PtBr, string EnUs)
{
    public string Resolve(string locale) =>
        AppLocales.IsEnglish(locale) ? EnUs : PtBr;

    public static implicit operator L((string pt, string en) t) => new(t.pt, t.en);
}

public static class Locale
{
    public static string Current(HttpContext http) =>
        AppLocales.Normalize(http.Request.Cookies[AppLocales.CookieName]);

    public static string T(HttpContext http, string ptBr, string enUs) =>
        AppLocales.IsEnglish(Current(http)) ? enUs : ptBr;

    public static string T(HttpContext http, L text) => text.Resolve(Current(http));
}
