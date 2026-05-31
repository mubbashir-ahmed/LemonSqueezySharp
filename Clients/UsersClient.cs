using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Http;
using LemonSqueezy.NET.Interfaces.Clients;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.Users;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IUsersClient"/>
public sealed class UsersClient : ApiClientBase, IUsersClient
{
    public UsersClient(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<BaseResult<User>> GetMeAsync(CancellationToken cancellationToken = default)
    {
        var requestUri = $"{ApiEndpoints.Users}/me";
        var response = await GetJsonApiAsync<User>(requestUri, cancellationToken).ConfigureAwait(false);

        return new BaseResult<User>(response?.Data, response?.Links, response?.Meta);
    }
}
