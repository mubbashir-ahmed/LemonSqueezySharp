using LemonSqueezySharp.Clients.Base;
using LemonSqueezySharp.Http;
using LemonSqueezySharp.Interfaces.Clients;
using LemonSqueezySharp.Models.Common;
using LemonSqueezySharp.Models.Files;
using File = LemonSqueezySharp.Models.Files.File;

namespace LemonSqueezySharp.Clients;

/// <inheritdoc cref="IFilesClient"/>
public sealed class FilesClient : ApiClientBase, IFilesClient
{
    /// <summary>Initializes a new instance of the <see cref="FilesClient"/> class.</summary>
    public FilesClient(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<BaseResult<File>> GetAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("File id must be provided.", nameof(id));
        }

        var requestUri = $"{ApiEndpoints.Files}/{Uri.EscapeDataString(id)}";
        var response = await GetJsonApiAsync<File>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<File>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<PagedBaseResult<File>> ListAsync(
        FileListFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        var query = BuildListQueryBuilder(filter);
        if (filter is not null)
        {
            Append(query, "filter[variant_id]", filter.VariantId);
        }

        var requestUri = $"{ApiEndpoints.Files}{query}";
        var response = await GetJsonApiCollectionAsync<File>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new PagedBaseResult<File>(
            response?.Data ?? Array.Empty<File>(),
            response?.Links,
            response?.Meta);
    }
}
