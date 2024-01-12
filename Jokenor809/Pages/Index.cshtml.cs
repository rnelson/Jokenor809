using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jokenor809.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IJokes _jokes;

    public IndexModel(ILogger<IndexModel> logger,IJokes jokes)
    {
        _jokes = jokes ?? throw new ArgumentNullException(nameof(jokes));
        _logger = logger;
    }

    public void OnGet()
    {
        ViewData["JokesObj"] = _jokes;
    }
}