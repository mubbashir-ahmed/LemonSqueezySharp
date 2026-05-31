using LemonSqueezy.NET.Interfaces.Clients.Base;
using LemonSqueezy.NET.Models.Common;
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
    Task<BaseResult<User>> GetMeAsync(CancellationToken cancellationToken = default);
}
