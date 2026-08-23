using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;

namespace LemonSqueezySharp.Extensions;

/// <summary>
/// Opt-in Polly resilience for the SDK's HTTP clients, built on
/// <c>Microsoft.Extensions.Http.Resilience</c>.
/// </summary>
public static class ResilienceExtensions
{
    /// <summary>
    /// Adds the standard resilience pipeline — retry (exponential backoff + jitter), circuit
    /// breaker, total-request timeout, per-attempt timeout, and a concurrency limiter — to every
    /// LemonSqueezy resource client, and only those clients. Call after <c>AddLemonSqueezy(...)</c>.
    /// </summary>
    /// <param name="services">The service collection the SDK was registered into.</param>
    /// <param name="configure">Optional hook to tune the standard pipeline (retry counts, timeouts, …).</param>
    public static IServiceCollection AddLemonSqueezyResilience(
        this IServiceCollection services,
        Action<HttpStandardResilienceOptions>? configure = null)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        foreach (var name in ServiceCollectionExtensions.ResourceClientNames)
        {
            var builder = services.AddHttpClient(name);

            if (configure is null)
            {
                builder.AddStandardResilienceHandler();
            }
            else
            {
                builder.AddStandardResilienceHandler(configure);
            }
        }

        return services;
    }

    /// <summary>
    /// Fluent overload so resilience can be chained directly onto the result of
    /// <c>AddLemonSqueezy(...)</c>.
    /// </summary>
    public static IHttpClientBuilder AddLemonSqueezyResilience(
        this IHttpClientBuilder builder,
        Action<HttpStandardResilienceOptions>? configure = null)
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.Services.AddLemonSqueezyResilience(configure);
        return builder;
    }
}
