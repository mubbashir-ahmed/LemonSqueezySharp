using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Clients.Interfaces;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IUsersClient"/>
public sealed class UsersClient : ApiClientBase, IUsersClient
{
    public UsersClient(HttpClient httpClient) : base(httpClient)
    {
    }
}
