using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AoCGetter;

public class Puzzle
{
    readonly AoCOptions _options = null!;
    readonly IHttpClientFactory _httpClientFactory = null!;
    static readonly int CURRENT_YEAR = DateTime.Today.Year;
    static readonly string AOC_WEBSITE_URL = "https://adventofcode.com";

    internal Puzzle(IOptions<AoCOptions> options, IHttpClientFactory httpClientFactory)
    {
        _options = options.Value;
        _httpClientFactory = httpClientFactory;
    }

    public string GetInput(int day) => GetInput(CURRENT_YEAR, day);

    public async string GetInput(int year, int day)
    {
        using HttpClient client = _httpClientFactory.CreateClient();
        string url = BuildAoCUrl(year, day);

        string input = await client.GetStringAsync(url);

        return input;
    }

    static string BuildAoCUrl(int year, int day) => $"{AOC_WEBSITE_URL}/{year}/day/{day}/input";
}
