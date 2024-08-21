namespace AoCGetter;

public class AoCOptions
{
    internal static readonly string AdventOfCode = "AdventOfCode";
    static readonly string WebsiteBaseUrl = "https://adventofcode.com";
    internal static readonly Uri WebsiteBaseUri = new(WebsiteBaseUrl);

    public string SessionToken { get; set; } = string.Empty;
    public string CacheDir { get; set; } = "./Cache";
}
