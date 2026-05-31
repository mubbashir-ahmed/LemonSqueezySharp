using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Http;
using LemonSqueezy.NET.Interfaces.Clients;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.Files;
using File = LemonSqueezy.NET.Models.Files.File;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IFilesClient"/>
public sealed class FilesClient : ApiClientBase, IFilesClient
{
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
