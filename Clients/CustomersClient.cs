using LemonSqueezySharp.Clients.Base;
using LemonSqueezySharp.Http;
using LemonSqueezySharp.Interfaces.Clients;
using LemonSqueezySharp.Models.Common;
using LemonSqueezySharp.Models.Customers;

namespace LemonSqueezySharp.Clients;

/// <inheritdoc cref="ICustomersClient"/>
public sealed class CustomersClient : ApiClientBase, ICustomersClient
{
    private const string ResourceType = "customers";
    private const string StoreRelationship = "store";
    private const string StoreResourceType = "stores";

    /// <summary>Initializes a new instance of the <see cref="CustomersClient"/> class.</summary>
    public CustomersClient(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<BaseResult<Customer>> CreateAsync(
        CreateCustomerRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Name is required.", nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            throw new ArgumentException("Email is required.", nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.StoreId))
        {
            throw new ArgumentException("StoreId is required.", nameof(request));
        }

        var body = new JsonApiWriteRequest<CustomerAttributes>
        {
            Data = new JsonApiWriteData<CustomerAttributes>
            {
                Type = ResourceType,
                Attributes = new CustomerAttributes
                {
                    Name = request.Name,
                    Email = request.Email,
                    City = request.City,
                    Region = request.Region,
                    Country = request.Country,
                },
                Relationships = new Dictionary<string, JsonApiRelationship>
                {
                    [StoreRelationship] = JsonApiRelationship.To(StoreResourceType, request.StoreId),
                },
            },
        };

        var requestUri = ApiEndpoints.Customers;
        var response = await PostJsonApiAsync<Customer, JsonApiWriteRequest<CustomerAttributes>>(requestUri, body, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<Customer>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<BaseResult<Customer>> GetAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Customer id must be provided.", nameof(id));
        }

        var requestUri = $"{ApiEndpoints.Customers}/{Uri.EscapeDataString(id)}";
        var response = await GetJsonApiAsync<Customer>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<Customer>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<BaseResult<Customer>> UpdateAsync(
        string id,
        UpdateCustomerRequest request,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Customer id must be provided.", nameof(id));
        }

        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var body = new JsonApiWriteRequest<CustomerAttributes>
        {
            Data = new JsonApiWriteData<CustomerAttributes>
            {
                Type = ResourceType,
                Id = id,
                Attributes = new CustomerAttributes
                {
                    Name = request.Name,
                    Email = request.Email,
                    City = request.City,
                    Region = request.Region,
                    Country = request.Country,
                    Status = request.Status,
                },
            },
        };

        var requestUri = $"{ApiEndpoints.Customers}/{Uri.EscapeDataString(id)}";
        var response = await PatchJsonApiAsync<Customer, JsonApiWriteRequest<CustomerAttributes>>(requestUri, body, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<Customer>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<PagedBaseResult<Customer>> ListAsync(
        CustomerListFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        var query = BuildListQueryBuilder(filter);
        if (filter is not null)
        {
            Append(query, "filter[store_id]", filter.StoreId);
            Append(query, "filter[email]", filter.Email);
        }

        var requestUri = $"{ApiEndpoints.Customers}{query}";
        var response = await GetJsonApiCollectionAsync<Customer>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new PagedBaseResult<Customer>(
            response?.Data ?? Array.Empty<Customer>(),
            response?.Links,
            response?.Meta);
    }
}
