using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StackExchange.Redis;
using System.Text;

namespace Valuator.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IDatabase _db;

    public IndexModel(ILogger<IndexModel> logger, IConnectionMultiplexer redis)
    {
        _logger = logger;
        _db = redis.GetDatabase();
    }

    public void OnGet()
    {

    }

    public IActionResult OnPost(string text)
    {
        _logger.LogDebug(text);

        string id = Guid.NewGuid().ToString();

        string similarityKey = "SIMILARITY-" + id;
        string textKey = "TEXT-" + id;
        string rankKey = "RANK-" + id;

        // TODO: (pa1) посчитать similarity и сохранить в БД (Redis) по ключу similarityKey
        int similarity = CalculateSimilarity(text);
        _db.StringSet(similarityKey, similarity);
        
        // TODO: (pa1) сохранить в БД (Redis) text по ключу textKey
        _db.StringSet(textKey, text);

        // TODO: (pa1) посчитать rank и сохранить в БД (Redis) по ключу rankKey
        double rank = CalculateRank(text);
        _db.StringSet(rankKey, rank);

        return Redirect($"summary?id={id}");
    }

    private double CalculateRank(string text)
    {
        if (string.IsNullOrEmpty(text))
            return 0;

        int total = text.Length;
        int nonLetters = 0;

        foreach (char c in text)
        {
            if (!char.IsLetter(c))
                nonLetters++;
        }

        return Math.Round((double)nonLetters / total, 3);;
    }

    private int CalculateSimilarity(string text)
    {
        var server = _db.Multiplexer.GetServer("localhost", 6379);

        foreach (var key in server.Keys(pattern: "TEXT-*"))
        {
            var existingText = _db.StringGet(key);
            if (existingText == text)
                return 1;
        }

        return 0;
    }


}
