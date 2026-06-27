using System.Net;

namespace LemonSqueezy.NET.Exceptions;

/// <summary>
/// Thrown for 401 Unauthorized responses — typically a missing or invalid API key.
/// </summary>
public sealed class LemonSqueezyAuthenticationException : LemonSqueezyApiException
{
    /// <summary>Initializes a new instance of the <see cref="LemonSqueezyAuthenticationException"/> class.</summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="rawResponseBody">The raw response body, preserved for diagnostics.</param>
    /// <param name="requestId">The optional request correlation id echoed by the server.</param>
    public LemonSqueezyAuthenticationException(
        string message,
        string? rawResponseBody = null,
        string? requestId = null)
        : base(HttpStatusCode.Unauthorized, message, rawResponseBody, requestId)
    {
    }
}
