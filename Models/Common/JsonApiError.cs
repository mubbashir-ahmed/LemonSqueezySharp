using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Common;

/// <summary>
/// JSON:API error object. Returned in the <c>errors</c> array on non-2xx responses.
/// </summary>
public sealed class JsonApiError
{
    /// <summary>
    /// Unique identifier for this particular occurrence of the error (<c>id</c>).
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// HTTP status code applicable to this error, as a string (<c>status</c>).
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Application-specific error code (<c>code</c>).
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    /// <summary>
    /// Short, human-readable summary of the problem (<c>title</c>).
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Human-readable explanation specific to this occurrence (<c>detail</c>).
    /// </summary>
    [JsonPropertyName("detail")]
    public string? Detail { get; set; }

    /// <summary>
    /// Object identifying the source of the error (<c>source</c>).
    /// </summary>
    [JsonPropertyName("source")]
    public JsonApiErrorSource? Source { get; set; }
}

/// <summary>
/// JSON:API error <c>source</c> object — points at the request element that caused the error.
/// </summary>
public sealed class JsonApiErrorSource
{
    /// <summary>
    /// JSON Pointer to the offending value in the request document (<c>pointer</c>).
    /// </summary>
    [JsonPropertyName("pointer")]
    public string? Pointer { get; set; }

    /// <summary>
    /// Name of the offending query-string parameter (<c>parameter</c>).
    /// </summary>
    [JsonPropertyName("parameter")]
    public string? Parameter { get; set; }
}

/// <summary>
/// Envelope wrapping the error array.
/// </summary>
public sealed class JsonApiErrorResponse
{
    /// <summary>
    /// The collection of errors returned by the API (<c>errors</c>).
    /// </summary>
    [JsonPropertyName("errors")]
    public IReadOnlyList<JsonApiError> Errors { get; set; } = Array.Empty<JsonApiError>();
}
