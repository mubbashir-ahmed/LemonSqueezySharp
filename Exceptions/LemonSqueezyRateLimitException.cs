using System.Net;

namespace LemonSqueezy.NET.Exceptions;

/// <summary>
/// Thrown for 429 Too Many Requests responses. Exposes the server-suggested retry delay
/// (if any) via <see cref="RetryAfter"/>.
/// </summary>
public sealed class LemonSqueezyRateLimitException : LemonSqueezyApiException
{
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
