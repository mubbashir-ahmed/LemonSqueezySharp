namespace LemonSqueezy.NET.Models.Customers;

/// <summary>
/// Inputs accepted by <c>POST /v1/customers</c>.
/// </summary>
public sealed class CreateCustomerRequest
{
    /// <summary>Customer full name. Required.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Customer email. Required.</summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>Store the customer belongs to. Required.</summary>
    public string StoreId { get; set; } = string.Empty;

    /// <summary>Optional city.</summary>
    public string? City { get; set; }

    /// <summary>Optional state / region.</summary>
    public string? Region { get; set; }

    /// <summary>Optional ISO-3166 alpha-2 country code.</summary>
    public string? Country { get; set; }
}
