using System.Net.Http.Json;
using LemonSqueezySharp.Models.Common;
using LemonSqueezySharp.Serialization;

namespace LemonSqueezySharp.Extensions;

/// <summary>
/// Convenience helpers for reading JSON:API envelopes off an
/// <see cref="HttpResponseMessage"/> using the shared serializer options.
/// </summary>
public static class HttpResponseMessageExtensions
{
    /// <summary>
    /// Reads the response body as a single-resource JSON:API envelope
    /// (<see cref="JsonApiResponse{TResource}"/>) using the shared serializer options.
    /// </summary>
    /// <typeparam name="TResource">The resource type contained in the envelope.</typeparam>
    /// <param name="response">The HTTP response whose content is read.</param>
    /// <param name="cancellationToken">A token to cancel the read operation.</param>
    /// <returns>The deserialized envelope, or <c>null</c> if the content is empty.</returns>
    public static Task<JsonApiResponse<TResource>?> ReadJsonApiAsync<TResource>(
        this HttpResponseMessage response,
        CancellationToken cancellationToken = default)
    {
        if (response is null)
        {
            throw new ArgumentNullException(nameof(response));
        }

        return response.Content.ReadFromJsonAsync<JsonApiResponse<TResource>>(
            LemonSqueezyJsonOptions.Default,
            cancellationToken);
    }

    /// <summary>
    /// Reads the response body as a multi-resource JSON:API envelope
    /// (<see cref="JsonApiCollectionResponse{TResource}"/>) using the shared serializer options.
    /// </summary>
    /// <typeparam name="TResource">The resource type contained in the collection envelope.</typeparam>
    /// <param name="response">The HTTP response whose content is read.</param>
    /// <param name="cancellationToken">A token to cancel the read operation.</param>
    /// <returns>The deserialized collection envelope, or <c>null</c> if the content is empty.</returns>
    public static Task<JsonApiCollectionResponse<TResource>?> ReadJsonApiCollectionAsync<TResource>(
        this HttpResponseMessage response,
        CancellationToken cancellationToken = default)
    {
        if (response is null)
        {
            throw new ArgumentNullException(nameof(response));
        }

        return response.Content.ReadFromJsonAsync<JsonApiCollectionResponse<TResource>>(
            LemonSqueezyJsonOptions.Default,
            cancellationToken);
    }
}
