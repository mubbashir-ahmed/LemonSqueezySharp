using LemonSqueezySharp.Interfaces.Clients.Base;
using LemonSqueezySharp.Models.Common;
using LemonSqueezySharp.Models.Customers;

namespace LemonSqueezySharp.Interfaces.Clients;

/// <summary>
/// Client for the LemonSqueezy <c>/v1/customers</c> endpoints.
/// See https://docs.lemonsqueezy.com/api/customers.
/// </summary>
public interface ICustomersClient : IApiClient
{
    /// <summary>
    /// Creates a new customer — <c>POST /v1/customers</c>.
    /// </summary>
    Task<BaseResult<Customer>> CreateAsync(
        CreateCustomerRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a customer by id — <c>GET /v1/customers/{id}</c>.
    /// </summary>
    Task<BaseResult<Customer>> GetAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a customer (PATCH semantics — only set the fields you want to change)
    /// — <c>PATCH /v1/customers/{id}</c>.
    /// </summary>
    Task<BaseResult<Customer>> UpdateAsync(
        string id,
        UpdateCustomerRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists customers, optionally filtered by store/email and paged — <c>GET /v1/customers</c>.
    /// </summary>
    Task<PagedBaseResult<Customer>> ListAsync(
        CustomerListFilter? filter = null,
        CancellationToken cancellationToken = default);
}
