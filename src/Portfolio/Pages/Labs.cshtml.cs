using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Portfolio.Pages;

public class LabsModel : PageModel
{
    public LabsModel(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void OnGet()
    {
    }
}
