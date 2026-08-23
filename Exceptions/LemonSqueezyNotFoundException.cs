using System.Net;

namespace LemonSqueezySharp.Exceptions;

/// <summary>
/// Thrown for 404 Not Found responses.
/// </summary>
public sealed class LemonSqueezyNotFoundException : LemonSqueezyApiException
{
    /// <summary>Initializes a new instance of the <see cref="LemonSqueezyNotFoundException"/> class.</summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="rawResponseBody">The raw response body, preserved for diagnostics.</param>
    /// <param name="requestId">The optional request correlation id echoed by the server.</param>
    public LemonSqueezyNotFoundException(
        string message,
        string? rawResponseBody = null,
        string? requestId = null)
        : base(HttpStatusCode.NotFound, message, rawResponseBody, requestId)
    {
    }
}
