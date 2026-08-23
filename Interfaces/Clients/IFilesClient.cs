using LemonSqueezySharp.Interfaces.Clients.Base;
using LemonSqueezySharp.Models.Common;
using LemonSqueezySharp.Models.Files;
using File = LemonSqueezySharp.Models.Files.File;

namespace LemonSqueezySharp.Interfaces.Clients;

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
