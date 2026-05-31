using System.Net;

namespace LemonSqueezy.NET.Exceptions;

/// <summary>
/// Thrown for 404 Not Found responses.
/// </summary>
public sealed class LemonSqueezyNotFoundException : LemonSqueezyApiException
{
    public LemonSqueezyNotFoundException(
        string message,
        string? rawResponseBody = null,
        string? requestId = null)
        : base(HttpStatusCode.NotFound, message, rawResponseBody, requestId)
    {
    }
}
