using Microsoft.Extensions.DependencyInjection;

namespace LemonSqueezySharp.Extensions;

/// <summary>
/// Hook for callers to attach additional handlers (Polly retry, logging,
/// telemetry, etc.) to the SDK's <see cref="HttpClient"/> registrations.
/// </summary>
public static class HttpClientBuilderExtensions
{
    /// <summary>
    /// Marker method — callers chain their own <c>AddPolicyHandler(...)</c>,
    /// <c>AddStandardResilienceHandler(...)</c> or similar onto the returned builder.
    /// </summary>
    public static IHttpClientBuilder WithLemonSqueezyDefaults(this IHttpClientBuilder builder)
    {
        return builder ?? throw new ArgumentNullException(nameof(builder));
    }
}
