using System.Text.Json.Serialization;
using LemonSqueezy.NET.Models.Common;

namespace LemonSqueezy.NET.Models.Orders;

public sealed class Order : JsonApiResource<OrderAttributes>
{
}

/// <summary>
/// Order attribute payload. See https://docs.lemonsqueezy.com/api/orders.
/// </summary>
public sealed class OrderAttributes
{
    [JsonPropertyName("identifier")]
    public string? Identifier { get; set; }

    [JsonPropertyName("order_number")]
    public long? OrderNumber { get; set; }

    [JsonPropertyName("user_email")]
    public string? UserEmail { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("total")]
    public long? Total { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("refunded")]
    public bool? Refunded { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
