using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Data;
using Portfolio.I18n;
using Portfolio.Models;

namespace Portfolio.Pages.Projects;

public class DetailsModel : PageModel
{
    public ProjectItem Project { get; private set; } = null!;

    public ArchitectureFlow? Architecture { get; private set; }

    public IActionResult OnGet(string slug)
    {
        if (string.Equals(slug, "pipeline-pulse", StringComparison.OrdinalIgnoreCase))
        {
            return RedirectPermanent("/Projects/pipeview");
        }

        var locale = Locale.Current(HttpContext);
        var project = PortfolioCatalog.FindProject(slug, locale);
        if (project is null)
        {
            return NotFound();
        }

        Project = project;
        Architecture = project.Kind is ProjectKind.Product or ProjectKind.Proof
            ? ArchitectureCatalog.ForProject(slug, locale)
            : null;
        return Page();
    }
}
