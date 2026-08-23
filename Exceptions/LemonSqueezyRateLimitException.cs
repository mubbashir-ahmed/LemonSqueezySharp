using System.Net;

namespace LemonSqueezySharp.Exceptions;

/// <summary>
/// Thrown for 429 Too Many Requests responses. Exposes the server-suggested retry delay
/// (if any) via <see cref="RetryAfter"/>.
/// </summary>
public sealed class LemonSqueezyRateLimitException : LemonSqueezyApiException
{
    /// <summary>Initializes a new instance of the <see cref="LemonSqueezyRateLimitException"/> class.</summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="retryAfter">The server-suggested delay before the next attempt.</param>
    /// <param name="rawResponseBody">The raw response body, preserved for diagnostics.</param>
    /// <param name="requestId">The optional request correlation id echoed by the server.</param>
    public LemonSqueezyRateLimitException(
        string message,
        TimeSpan? retryAfter = null,
        string? rawResponseBody = null,
        string? requestId = null)
        : base((HttpStatusCode)429, message, rawResponseBody, requestId)
    {
        RetryAfter = retryAfter;
    }

    /// <summary>
    /// Server-suggested delay before the next attempt, parsed from the <c>Retry-After</c> header.
    /// </summary>
    public TimeSpan? RetryAfter { get; }
}
