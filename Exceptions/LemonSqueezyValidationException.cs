using System.Net;

namespace LemonSqueezy.NET.Exceptions;

/// <summary>
/// Thrown for 422 Unprocessable Entity responses — typically a malformed JSON:API payload
/// or business-rule rejection.
/// </summary>
public sealed class LemonSqueezyValidationException : LemonSqueezyApiException
{
    /// <summary>Initializes a new instance of the <see cref="LemonSqueezyValidationException"/> class.</summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="errors">The human-readable validation error messages returned by the API.</param>
    /// <param name="rawResponseBody">The raw response body, preserved for diagnostics.</param>
    /// <param name="requestId">The optional request correlation id echoed by the server.</param>
    public LemonSqueezyValidationException(
        string message,
        IReadOnlyList<string>? errors = null,
        string? rawResponseBody = null,
        string? requestId = null)
        : base((HttpStatusCode)422, message, rawResponseBody, requestId)
    {
        Errors = errors ?? Array.Empty<string>();
    }

    /// <summary>
    /// Human-readable validation error messages extracted from the JSON:API <c>errors</c> array.
    /// </summary>
    public IReadOnlyList<string> Errors { get; }
}
