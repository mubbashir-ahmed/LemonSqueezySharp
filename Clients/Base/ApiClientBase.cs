using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using LemonSqueezy.NET.Exceptions;
using LemonSqueezy.NET.Interfaces.Clients.Base;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Serialization;

namespace LemonSqueezy.NET.Clients.Base;

/// <summary>
/// Shared HTTP plumbing for every resource-specific client: dispatching requests,
/// deserializing JSON:API envelopes, and translating non-success statuses to typed
/// exceptions. Resource clients should call <c>SendAsync</c> / <c>GetJsonApiAsync</c> /
/// <c>GetJsonApiCollectionAsync</c> rather than touching <see cref="HttpClient"/> directly.
/// </summary>
public abstract class ApiClientBase : IApiClient
{
    private const string RequestIdHeader = "x-request-id";

    protected ApiClientBase(HttpClient httpClient)
    {
        HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    /// <summary>
    /// Configured <see cref="HttpClient"/> — base address, default headers, handlers
    /// are already attached by the DI registration.
    /// </summary>
    protected HttpClient HttpClient { get; }

    /// <summary>
    /// Sends a request, validates the response, and returns the deserialized envelope
    /// for a single-resource endpoint.
    /// </summary>
    protected async Task<JsonApiResponse<TResource>?> GetJsonApiAsync<TResource>(
        string requestUri,
        CancellationToken cancellationToken)
    {
        using var response = await HttpClient.GetAsync(requestUri, cancellationToken).ConfigureAwait(false);
        await EnsureSuccessAsync(response, cancellationToken).ConfigureAwait(false);

        return await response.Content
            .ReadFromJsonAsync<JsonApiResponse<TResource>>(LemonSqueezyJsonOptions.Default, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Sends a request, validates the response, and returns the deserialized envelope
    /// for a list endpoint.
    /// </summary>
    protected async Task<JsonApiCollectionResponse<TResource>?> GetJsonApiCollectionAsync<TResource>(
        string requestUri,
        CancellationToken cancellationToken)
    {
        using var response = await HttpClient.GetAsync(requestUri, cancellationToken).ConfigureAwait(false);
        await EnsureSuccessAsync(response, cancellationToken).ConfigureAwait(false);

        return await response.Content
            .ReadFromJsonAsync<JsonApiCollectionResponse<TResource>>(LemonSqueezyJsonOptions.Default, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Translates a non-success HTTP response into the appropriate typed exception.
    /// </summary>
    protected static async Task EnsureSuccessAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

#if NET10_0_OR_GREATER
        var body = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
#else
        var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
#endif

        var requestId = response.Headers.TryGetValues(RequestIdHeader, out var ids)
            ? ids.FirstOrDefault()
            : null;

        var (message, errors) = TryReadErrorBody(body);

        throw response.StatusCode switch
        {
            HttpStatusCode.Unauthorized =>
                new LemonSqueezyAuthenticationException(message, body, requestId),
            HttpStatusCode.NotFound =>
                new LemonSqueezyNotFoundException(message, body, requestId),
            (HttpStatusCode)422 =>
                new LemonSqueezyValidationException(message, errors, body, requestId),
            (HttpStatusCode)429 =>
                new LemonSqueezyRateLimitException(message, response.Headers.RetryAfter?.Delta, body, requestId),
            _ => new LemonSqueezyApiException(response.StatusCode, message, body, requestId),
        };
    }

    private static (string Message, IReadOnlyList<string> Errors) TryReadErrorBody(string body)
    {
        if (string.IsNullOrWhiteSpace(body))
        {
            return ("The LemonSqueezy API returned an error response.", Array.Empty<string>());
        }

        try
        {
            var parsed = JsonSerializer.Deserialize<JsonApiErrorResponse>(body, LemonSqueezyJsonOptions.Default);
            if (parsed?.Errors is { Count: > 0 } errors)
            {
                var messages = errors
                    .Select(e => e.Detail ?? e.Title ?? e.Code ?? "Unknown error")
                    .ToArray();
                return (string.Join("; ", messages), messages);
            }
        }
        catch (JsonException)
        {
            // Non-JSON or unexpected shape — fall through to a generic message.
        }

        return ("The LemonSqueezy API returned an error response.", Array.Empty<string>());
    }
}
