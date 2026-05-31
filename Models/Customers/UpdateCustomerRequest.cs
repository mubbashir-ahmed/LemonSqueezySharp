namespace LemonSqueezy.NET.Models.Customers;

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
    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? City { get; set; }

    public string? Region { get; set; }

    public string? Country { get; set; }

    /// <summary>
    /// Archive (<c>"archived"</c>) or unarchive (<c>""</c>) the customer.
    /// </summary>
    public string? Status { get; set; }
}
