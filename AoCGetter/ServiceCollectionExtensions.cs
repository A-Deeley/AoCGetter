using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AoCGetter;

public static class ServiceCollectionExtensions
{
    public static void AddAoCGetter(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AoCOptions>(configuration.GetSection(AoCOptions.AdventOfCode));

        services.AddHttpClient<Puzzle>(AddPuzzleClient);
    }

    static void AddPuzzleClient(IServiceProvider provider, HttpClient client)
    {
        var settings = provider.GetRequiredService<IOptions<AoCOptions>>().Value;
        client.DefaultRequestHeaders.Add("Cookie", $"session={settings.SessionToken}");
        client.BaseAddress = AoCOptions.WebsiteBaseUri;
    }
}
