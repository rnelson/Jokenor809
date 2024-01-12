using System.Text.Json.Nodes;

namespace Jokenor809;

public interface IJokes
{
    int Count();
    
    string GetJoke(int number);
    string GetRandomJoke();
    string[] GetAllJokes();
    
    List<string> LoadJokes();
}

public class Jokes : IJokes
{
    private readonly ILogger<Jokes> _logger;
    private readonly IWebHostEnvironment _env;
    private readonly Random _random;
    private readonly List<string> _jokes;
    
    public Jokes(ILogger<Jokes> logger, IWebHostEnvironment environment)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _env = environment ?? throw new ArgumentNullException(nameof(environment));
        _random = new Random(DateTime.UtcNow.Microsecond);
        _jokes = [];

        EnsureJokes();
    }

    public int Count() => _jokes.Count;

    public string GetJoke(int number)
    {
        EnsureJokes();
        var max = _jokes.Count;

        if (number < 1)
            throw new ArgumentException($"{number} is invalid, must be between 1 and {max}", nameof(number));
        if (number > max)
            throw new ArgumentException($"{number} is invalid, must be between 1 and {max}", nameof(number));

        return _jokes[number-1];
    }

    public string GetRandomJoke()
    {
        EnsureJokes();

        return _jokes[_random.Next(0, _jokes.Count)];
    }

    public string[] GetAllJokes()
    {
        EnsureJokes();

        return _jokes.ToArray();
    }

    public List<string> LoadJokes()
    {
        var filename = Path.Combine(_env.WebRootPath, "Jokes.json");

        using var stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
        var array = JsonNode.Parse(stream)?.AsArray() ??
                    throw new InvalidOperationException($"unable to read {filename}");

        var list = array.Select(joke => joke!.ToString()).ToList();
        return list;
    }

    private void EnsureJokes()
    {
        if (_jokes.Count < 1)
            _jokes.AddRange(LoadJokes());
    }
}