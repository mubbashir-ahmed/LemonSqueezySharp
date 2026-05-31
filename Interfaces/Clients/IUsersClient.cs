using LemonSqueezy.NET.Interfaces.Clients.Base;
using LemonSqueezy.NET.Models.Users;

namespace LemonSqueezy.NET.Interfaces.Clients;

/// <summary>
/// Client for the LemonSqueezy <c>/v1/users</c> endpoints.
/// See https://docs.lemonsqueezy.com/api/users.
/// </summary>
public interface IUsersClient : IApiClient
{
    /// <summary>
    /// Retrieves the authenticated user — <c>GET /v1/users/me</c>.
    /// </summary>
    Task<User?> GetMeAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a user by id — <c>GET /v1/users/{id}</c>.
    /// </summary>
    /// <param name="id">The user resource id.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    Task<User?> GetAsync(string id, CancellationToken cancellationToken = default);
}
