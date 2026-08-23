using System.Net;

namespace LemonSqueezySharp.Exceptions;

/// <summary>
/// Thrown when the LemonSqueezy API returns a non-success HTTP status code.
/// </summary>
public class LemonSqueezyApiException : LemonSqueezyException
{
    /// <summary>Initializes a new instance of the <see cref="LemonSqueezyApiException"/> class.</summary>
    /// <param name="statusCode">The HTTP status code returned by the API.</param>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="rawResponseBody">The raw response body, preserved for diagnostics.</param>
    /// <param name="requestId">The optional request correlation id echoed by the server.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public LemonSqueezyApiException(
        HttpStatusCode statusCode,
        string message,
        string? rawResponseBody = null,
        string? requestId = null,
        Exception? innerException = null)
        : base(message, innerException!)
    {
        StatusCode = statusCode;
        RawResponseBody = rawResponseBody;
        RequestId = requestId;
    }

    /// <summary>
    /// HTTP status code returned by the API.
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Raw response body, preserved for diagnostics.
    /// </summary>
    public string? RawResponseBody { get; }

    /// <summary>
    /// Optional request correlation id, when echoed by the server.
    /// </summary>
    public string? RequestId { get; }
}
