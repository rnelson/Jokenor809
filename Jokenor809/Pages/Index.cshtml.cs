using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jokenor809.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly Random _random = Random.Shared;
    private readonly IJokes _jokes;

    public IndexModel(ILogger<IndexModel> logger, IJokes jokes)
    {
        _jokes = jokes ?? throw new ArgumentNullException(nameof(jokes));
        _logger = logger;
    }

    public void OnGet(int? j)
    {
        var currentJokeId = j ?? _random.Next(0, _jokes.Count());
        var previousJokeId = currentJokeId - 1;
        var nextJokeId = currentJokeId + 1;

        if (previousJokeId < 1) previousJokeId = _jokes.Count();
        if (nextJokeId > _jokes.Count() - 1) nextJokeId = 1;
        
        ViewData["JokesObj"] = _jokes;
        ViewData["JokeId"] = currentJokeId.ToString();
        ViewData["PreviousJokeId"] = previousJokeId.ToString();
        ViewData["NextJokeId"] = nextJokeId.ToString();
    }
}