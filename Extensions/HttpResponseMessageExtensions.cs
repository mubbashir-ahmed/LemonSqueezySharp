using System.Net.Http.Json;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Serialization;

namespace LemonSqueezy.NET.Extensions;

/// <summary>
/// Convenience helpers for reading JSON:API envelopes off an
/// <see cref="HttpResponseMessage"/> using the shared serializer options.
/// </summary>
public static class HttpResponseMessageExtensions
{
    public static Task<JsonApiResponse<TAttributes>?> ReadJsonApiAsync<TAttributes>(
        this HttpResponseMessage response,
        CancellationToken cancellationToken = default)
    {
        if (response is null)
        {
            throw new ArgumentNullException(nameof(response));
        }

        return response.Content.ReadFromJsonAsync<JsonApiResponse<TAttributes>>(
            LemonSqueezyJsonOptions.Default,
            cancellationToken);
    }

    public static Task<JsonApiCollectionResponse<TAttributes>?> ReadJsonApiCollectionAsync<TAttributes>(
        this HttpResponseMessage response,
        CancellationToken cancellationToken = default)
    {
        if (response is null)
        {
            throw new ArgumentNullException(nameof(response));
        }

        return response.Content.ReadFromJsonAsync<JsonApiCollectionResponse<TAttributes>>(
            LemonSqueezyJsonOptions.Default,
            cancellationToken);
    }
}
