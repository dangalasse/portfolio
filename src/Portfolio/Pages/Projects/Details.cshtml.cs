using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Data;
using Portfolio.Models;

namespace Portfolio.Pages.Projects;

public class DetailsModel : PageModel
{
    public ProjectItem Project { get; private set; } = null!;

    public ArchitectureFlow? Architecture { get; private set; }

    public IActionResult OnGet(string slug)
    {
        var project = PortfolioCatalog.FindProject(slug);
        if (project is null)
        {
            return NotFound();
        }

        Project = project;
        Architecture = ArchitectureCatalog.ForProject(slug);
        return Page();
    }
}
