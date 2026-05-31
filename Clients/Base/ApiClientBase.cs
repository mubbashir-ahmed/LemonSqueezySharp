using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using LemonSqueezy.NET.Exceptions;
using LemonSqueezy.NET.Interfaces.Clients.Base;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.Requests;
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
    private const string JsonApiMediaType = "application/vnd.api+json";
    private static readonly HttpMethod PatchMethod = new("PATCH");

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
        using var response = await HttpClient.GetAsync(requestUri, cancellationToken)
            .ConfigureAwait(false);

        await EnsureSuccessAsync(response, cancellationToken)
            .ConfigureAwait(false);

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
        using var response = await HttpClient.GetAsync(requestUri, cancellationToken)
            .ConfigureAwait(false);

        await EnsureSuccessAsync(response, cancellationToken)
            .ConfigureAwait(false);

        return await response.Content
            .ReadFromJsonAsync<JsonApiCollectionResponse<TResource>>(LemonSqueezyJsonOptions.Default, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Sends a <c>POST</c> with a JSON:API body and deserialises the single-resource response.
    /// </summary>
    protected async Task<JsonApiResponse<TResource>?> PostJsonApiAsync<TResource, TBody>(
        string requestUri,
        TBody body,
        CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
        {
            Content = CreateJsonApiContent(body),
        };

        return await SendAndReadAsync<TResource>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Sends a <c>PATCH</c> with a JSON:API body and deserialises the single-resource response.
    /// </summary>
    protected async Task<JsonApiResponse<TResource>?> PatchJsonApiAsync<TResource, TBody>(
        string requestUri,
        TBody body,
        CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(PatchMethod, requestUri)
        {
            Content = CreateJsonApiContent(body),
        };

        return await SendAndReadAsync<TResource>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Sends a <c>DELETE</c> and deserialises a single-resource JSON:API response —
    /// used for endpoints that mutate state and echo the updated resource back
    /// (e.g. cancel-subscription).
    /// </summary>
    protected async Task<JsonApiResponse<TResource>?> DeleteJsonApiAsync<TResource>(
        string requestUri,
        CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);

        return await SendAndReadAsync<TResource>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Sends a <c>DELETE</c> for endpoints that return <c>204 No Content</c>.
    /// </summary>
    protected async Task DeleteJsonAsync(string requestUri, CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        using var response = await HttpClient.SendAsync(request, cancellationToken)
            .ConfigureAwait(false);

        await EnsureSuccessAsync(response, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Sends a <c>GET</c> and deserialises a flat (non-JSON:API) JSON response —
    /// for endpoints that don't follow the JSON:API envelope, e.g.
    /// <c>/v1/subscription-items/{id}/current-usage</c> which returns a bare
    /// <c>{ "meta": { … } }</c> object.
    /// </summary>
    protected async Task<TResponse?> GetJsonAsync<TResponse>(
        string requestUri,
        CancellationToken cancellationToken)
    {
        using var response = await HttpClient.GetAsync(requestUri, cancellationToken)
            .ConfigureAwait(false);

        await EnsureSuccessAsync(response, cancellationToken)
            .ConfigureAwait(false);

        return await response.Content
            .ReadFromJsonAsync<TResponse>(LemonSqueezyJsonOptions.Default, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Posts a <c>application/x-www-form-urlencoded</c> body and deserialises a flat
    /// (non-JSON:API) JSON response — used by the License API
    /// (<c>/v1/licenses/*</c>) which doesn't use the JSON:API media type.
    /// </summary>
    protected async Task<TResponse?> PostFormAsync<TResponse>(
        string requestUri,
        IEnumerable<KeyValuePair<string, string>> form,
        CancellationToken cancellationToken)
    {
        using var content = new FormUrlEncodedContent(form);
        using var request = new HttpRequestMessage(HttpMethod.Post, requestUri) { Content = content };

        // The License API responds with plain JSON, not JSON:API — tell the server so.
        request.Headers.Accept.Clear();
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        using var response = await HttpClient.SendAsync(request, cancellationToken)
            .ConfigureAwait(false);

        await EnsureSuccessAsync(response, cancellationToken)
            .ConfigureAwait(false);

        return await response.Content
            .ReadFromJsonAsync<TResponse>(LemonSqueezyJsonOptions.Default, cancellationToken)
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
        var body = await response.Content.ReadAsStringAsync(cancellationToken)
            .ConfigureAwait(false);
#else
        var body = await response.Content.ReadAsStringAsync()
            .ConfigureAwait(false);
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

    /// <summary>
    /// Serialises common list parameters (<c>page[number]</c>, <c>page[size]</c>, <c>include</c>)
    /// into a query string. Derived clients that need to append resource-specific filters
    /// should call <see cref="BuildListQueryBuilder"/> instead and continue writing to the
    /// returned <see cref="StringBuilder"/>.
    /// </summary>
    protected static string BuildListQuery(ListFilter? filter)
        => BuildListQueryBuilder(filter).ToString();

    /// <summary>
    /// Same as <see cref="BuildListQuery"/> but returns the underlying <see cref="StringBuilder"/>
    /// so derived clients can append additional <c>filter[...]</c> parameters via
    /// <see cref="Append"/>.
    /// </summary>
    protected static StringBuilder BuildListQueryBuilder(ListFilter? filter)
    {
        var builder = new StringBuilder();
        if (filter is null)
        {
            return builder;
        }

        Append(builder, "page[number]", filter.PageNumber?.ToString(CultureInfo.InvariantCulture));
        Append(builder, "page[size]", filter.PageSize?.ToString(CultureInfo.InvariantCulture));
        Append(builder, "include", filter.Include);

        return builder;
    }

    /// <summary>
    /// Appends a single <c>key=value</c> pair to <paramref name="builder"/>, URL-encoding both,
    /// and prefixing with <c>?</c> or <c>&amp;</c> as appropriate. No-op when the value is null/empty.
    /// </summary>
    protected static void Append(StringBuilder builder, string key, string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        builder.Append(builder.Length == 0 ? '?' : '&');
        builder.Append(Uri.EscapeDataString(key));
        builder.Append('=');
        builder.Append(Uri.EscapeDataString(value));
    }

    private async Task<JsonApiResponse<TResource>?> SendAndReadAsync<TResource>(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        using var response = await HttpClient.SendAsync(request, cancellationToken)
            .ConfigureAwait(false);

        await EnsureSuccessAsync(response, cancellationToken)
            .ConfigureAwait(false);

        if (response.StatusCode == HttpStatusCode.NoContent ||
            response.Content.Headers.ContentLength == 0)
        {
            return null;
        }

        return await response.Content
            .ReadFromJsonAsync<JsonApiResponse<TResource>>(LemonSqueezyJsonOptions.Default, cancellationToken)
            .ConfigureAwait(false);
    }

    private static JsonContent CreateJsonApiContent<TBody>(TBody body)
    {
        var content = JsonContent.Create(body, mediaType: null, options: LemonSqueezyJsonOptions.Default);
        content.Headers.ContentType = new MediaTypeHeaderValue(JsonApiMediaType);

        return content;
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
