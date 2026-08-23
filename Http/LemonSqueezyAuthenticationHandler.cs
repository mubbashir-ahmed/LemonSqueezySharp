using System.Net.Http.Headers;
using LemonSqueezySharp.Configurations;
using Microsoft.Extensions.Options;

namespace LemonSqueezySharp.Http;

/// <summary>
/// Delegating handler that attaches the bearer token and standard Accept/Content-Type
/// headers required by the LemonSqueezy JSON:API.
/// </summary>
internal sealed class LemonSqueezyAuthenticationHandler : DelegatingHandler
{
    private const string JsonApiMediaType = "application/vnd.api+json";

    private readonly IOptionsMonitor<LemonSqueezyOptions> _options;

    public LemonSqueezyAuthenticationHandler(IOptionsMonitor<LemonSqueezyOptions> options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var options = _options.CurrentValue;

        request.Headers.Authorization ??= new AuthenticationHeaderValue("Bearer", options.ApiKey);

        if (!request.Headers.Accept.Any(h => h.MediaType == JsonApiMediaType))
        {
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonApiMediaType));
        }

        if (request.Content is not null && request.Content.Headers.ContentType is null)
        {
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(JsonApiMediaType);
        }

        return base.SendAsync(request, cancellationToken);
    }
}
