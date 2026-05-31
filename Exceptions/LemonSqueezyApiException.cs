using System.Net;

namespace LemonSqueezy.NET.Exceptions;

/// <summary>
/// Thrown when the LemonSqueezy API returns a non-success HTTP status code.
/// </summary>
public class LemonSqueezyApiException : LemonSqueezyException
{
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
