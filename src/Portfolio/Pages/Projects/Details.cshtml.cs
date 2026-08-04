using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Data;
using Portfolio.Models;

namespace Portfolio.Pages.Projects;

public class DetailsModel : PageModel
{
    public ProjectItem Project { get; private set; } = null!;

    public string ArchitectureDiagram { get; private set; } = string.Empty;

    public IActionResult OnGet(string slug)
    {
        var project = PortfolioCatalog.FindProject(slug);
        if (project is null)
        {
            return NotFound();
        }

        Project = project;
        ArchitectureDiagram = project.Slug switch
        {
            "edge-status" =>
                """
                Browser (TS) → /api/status (ASP.NET)
                             → Worker Cloudflare (live region / cf-ray)
                """,
            "aws-static-demo" =>
                """
                User → CloudFront → S3 (private bucket)
                     ↘ optional: ACM cert + custom domain
                """,
            _ => string.Empty,
        };

        return Page();
    }
}
