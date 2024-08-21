using Microsoft.Extensions.Options;

namespace AoCGetter;

public class Puzzle
{
    readonly AoCOptions _options = null!;
    readonly IHttpClientFactory _httpClientFactory = null!;
    static readonly int CURRENT_YEAR = DateTime.Today.Year;

    internal Puzzle(IOptions<AoCOptions> options, IHttpClientFactory httpClientFactory)
    {
        _options = options.Value;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<string>> GetInput(int day) => await GetInputLines(CURRENT_YEAR, day);

    public async Task<IEnumerable<string>> GetInputLines(int year, int day)
    {
        string dayCacheFile = Path.Combine(_options.CacheDir, year.ToString(), $"day{day}.txt");

        if (File.Exists(dayCacheFile))
            return GetInputFromFile(year, day);

        return await GetInputFromUrl(year, day);
    }

    IEnumerable<string> GetInputFromFile(int year, int day) => File.ReadAllLines(Path.Combine(_options.CacheDir, year.ToString(), $"day{day}.txt"));

    async Task<IEnumerable<string>> GetInputFromUrl(int year, int day)
    {
        using HttpClient client = _httpClientFactory.CreateClient();
        string url = GetPuzzleUrl(year, day);

        string input = await client.GetStringAsync(url);

        CacheInput(input, year, day);

        return input.Split('\n');
    }

    void CacheInput(string input, int year, int day)
    {
        string dayCacheFile = Path.Combine(_options.CacheDir, year.ToString(), $"day{day}.txt");
        File.WriteAllLines(dayCacheFile, input.Split('\n'));
    }

    static string GetPuzzleUrl(int year, int day) => $"{year}/day/{day}/input";
}
