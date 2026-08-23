using System.Reflection;
using LemonSqueezySharp.Configurations;
using Microsoft.Extensions.Options;

namespace LemonSqueezySharp.Http;

/// <summary>
/// Delegating handler that sets a stable User-Agent header so requests can be
/// identified in LemonSqueezy server logs.
/// </summary>
internal sealed class LemonSqueezyUserAgentHandler : DelegatingHandler
{
    private static readonly string DefaultUserAgent = BuildDefaultUserAgent();

    private readonly IOptionsMonitor<LemonSqueezyOptions> _options;

    public LemonSqueezyUserAgentHandler(IOptionsMonitor<LemonSqueezyOptions> options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var userAgent = _options.CurrentValue.UserAgent ?? DefaultUserAgent;

        if (request.Headers.UserAgent.Count == 0)
        {
            request.Headers.UserAgent.ParseAdd(userAgent);
        }

        return base.SendAsync(request, cancellationToken);
    }

    private static string BuildDefaultUserAgent()
    {
        var assembly = typeof(LemonSqueezyUserAgentHandler).Assembly;
        var version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
            ?? assembly.GetName().Version?.ToString()
            ?? "0.0.0";

        return $"LemonSqueezySharp/{version}";
    }
}
