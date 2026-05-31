using System.Net;

namespace LemonSqueezy.NET.Exceptions;

/// <summary>
/// Thrown for 422 Unprocessable Entity responses — typically a malformed JSON:API payload
/// or business-rule rejection.
/// </summary>
public sealed class LemonSqueezyValidationException : LemonSqueezyApiException
{
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
