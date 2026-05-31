using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Http;
using LemonSqueezy.NET.Interfaces.Clients;
using LemonSqueezy.NET.Models.Users;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IUsersClient"/>
public sealed class UsersClient : ApiClientBase, IUsersClient
{
    public UsersClient(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<User?> GetMeAsync(CancellationToken cancellationToken = default)
    {
        var response = await GetJsonApiAsync<User>(ApiEndpoints.Me, cancellationToken).ConfigureAwait(false);
        return response?.Data;
    }

    /// <inheritdoc />
    public async Task<User?> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("User id must be provided.", nameof(id));
        }

        var response = await GetJsonApiAsync<User>($"{ApiEndpoints.Users}/{Uri.EscapeDataString(id)}", cancellationToken)
            .ConfigureAwait(false);
        return response?.Data;
    }
}
