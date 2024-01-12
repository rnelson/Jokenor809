using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jokenor809.Pages;

public class Jokes : PageModel
{
    private readonly IJokes _jokes;

    public Jokes(IJokes jokes)
    {
        _jokes = jokes ?? throw new ArgumentNullException(nameof(jokes));
    }
    
    public void OnGet()
    {
        ViewData["Jokes"] = _jokes.GetAllJokes();
    }
}