using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Interfaces.Clients;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IFilesClient"/>
public sealed class FilesClient : ApiClientBase, IFilesClient
{
    public FilesClient(HttpClient httpClient) : base(httpClient)
    {
    }
}
