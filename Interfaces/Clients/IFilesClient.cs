using LemonSqueezy.NET.Interfaces.Clients.Base;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.Files;
using File = LemonSqueezy.NET.Models.Files.File;

namespace LemonSqueezy.NET.Interfaces.Clients;

/// <summary>
/// Client for the LemonSqueezy <c>/v1/files</c> endpoints — the downloadable digital
/// goods attached to variants.
/// See https://docs.lemonsqueezy.com/api/files.
/// </summary>
public interface IFilesClient : IApiClient
{
    /// <summary>
    /// Retrieves a file by id — <c>GET /v1/files/{id}</c>.
    /// </summary>
    Task<BaseResult<File>> GetAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists files, optionally filtered by variant — <c>GET /v1/files</c>.
    /// </summary>
    Task<PagedBaseResult<File>> ListAsync(
        FileListFilter? filter = null,
        CancellationToken cancellationToken = default);
}
