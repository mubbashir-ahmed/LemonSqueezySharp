using LemonSqueezySharp.Clients.Base;
using LemonSqueezySharp.Http;
using LemonSqueezySharp.Interfaces.Clients;
using LemonSqueezySharp.Models.Common;
using LemonSqueezySharp.Models.Users;

namespace LemonSqueezySharp.Clients;

/// <inheritdoc cref="IUsersClient"/>
public sealed class UsersClient : ApiClientBase, IUsersClient
{
    /// <summary>Initializes a new instance of the <see cref="UsersClient"/> class.</summary>
    public UsersClient(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<BaseResult<User>> GetMeAsync(CancellationToken cancellationToken = default)
    {
        var requestUri = $"{ApiEndpoints.Users}/me";
        var response = await GetJsonApiAsync<User>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<User>(response?.Data, response?.Links, response?.Meta);
    }
}
