using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Common;

/// <summary>
/// JSON:API error object. Returned in the <c>errors</c> array on non-2xx responses.
/// </summary>
public sealed class JsonApiError
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("detail")]
    public string? Detail { get; set; }

    [JsonPropertyName("source")]
    public JsonApiErrorSource? Source { get; set; }
}

public sealed class JsonApiErrorSource
{
    [JsonPropertyName("pointer")]
    public string? Pointer { get; set; }

    [JsonPropertyName("parameter")]
    public string? Parameter { get; set; }
}

/// <summary>
/// Envelope wrapping the error array.
/// </summary>
public sealed class JsonApiErrorResponse
{
    [JsonPropertyName("errors")]
    public IReadOnlyList<JsonApiError> Errors { get; set; } = Array.Empty<JsonApiError>();
}
