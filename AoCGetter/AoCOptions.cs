namespace AoCGetter;

public class AoCOptions
{
    internal static readonly string Section = "AoC";

    public string SessionToken { get; set; } = string.Empty;
    public string CacheDir { get; set; } = "./Cache";
}
