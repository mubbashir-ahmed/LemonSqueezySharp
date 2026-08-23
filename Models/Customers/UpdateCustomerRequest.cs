namespace LemonSqueezySharp.Models.Customers;

/// <summary>
/// Inputs accepted by <c>PATCH /v1/customers/{id}</c>. Only set the properties you
/// want to update; <see langword="null"/> values are not sent to the server.
/// </summary>
/// <remarks>
/// To archive a customer set <see cref="Status"/> to <c>"archived"</c>. To unarchive,
/// set it to an empty string <c>""</c>.
/// </remarks>
public sealed class UpdateCustomerRequest
{
    /// <summary>New customer full name.</summary>
    public string? Name { get; set; }

    /// <summary>New customer email address.</summary>
    public string? Email { get; set; }

    /// <summary>New city.</summary>
    public string? City { get; set; }

    /// <summary>New state / region.</summary>
    public string? Region { get; set; }

    /// <summary>New ISO-3166 alpha-2 country code.</summary>
    public string? Country { get; set; }

    /// <summary>
    /// Archive (<c>"archived"</c>) or unarchive (<c>""</c>) the customer.
    /// </summary>
    public string? Status { get; set; }
}
