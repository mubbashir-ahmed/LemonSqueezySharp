using System.Net;

namespace LemonSqueezy.NET.Exceptions;

/// <summary>
/// Thrown for 401 Unauthorized responses — typically a missing or invalid API key.
/// </summary>
public sealed class LemonSqueezyAuthenticationException : LemonSqueezyApiException
{
    public LemonSqueezyAuthenticationException(
        string message,
        string? rawResponseBody = null,
        string? requestId = null)
        : base(HttpStatusCode.Unauthorized, message, rawResponseBody, requestId)
    {
    }
}
