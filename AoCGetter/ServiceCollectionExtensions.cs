using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AoCGetter;

public static class ServiceCollectionExtensions
{
    public static void AddAoCGetter(this IServiceCollection services)
    {
        var serviceCollection = services.BuildServiceProvider();
        try
        {
            var options = serviceCollection.GetRequiredService<IOptions<AoCOptions>>().Value;
            services.AddHttpClient("AoCClient", client =>
            {
                client.DefaultRequestHeaders.Add("Cookie", $"session={options.SessionToken}");
            });
        }
        catch (InvalidOperationException)
        {
            throw new Exception("Could not retrieve required options. Make sure to call AddAocGetter() AFTER configuring your AoC Session Key using ConfigureAoCSessionKey()!!");
        }
        finally
        {
            services.AddSingleton<Puzzle>();
        }
    }

    public static IServiceCollection ConfigureAoCSessionKey(this IServiceCollection services, Action<AoCOptions> configureOptions)
    {
        services.Configure(configureOptions);

        return services;
    }

    public static IServiceCollection ConfigureAoCSessionKey(this IServiceCollection services, IConfiguration configuration)
    {
        var aocSection = configuration.GetRequiredSection(AoCOptions.Section);

        services.Configure<AoCOptions>(aocSection);

        return services;
    }
}
