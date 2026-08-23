namespace LemonSqueezySharp.Exceptions;

/// <summary>
/// Thrown when an inbound webhook payload fails HMAC-SHA256 signature verification.
/// </summary>
public sealed class LemonSqueezyWebhookVerificationException : LemonSqueezyException
{
    private const string DefaultMessage = "The webhook signature could not be verified.";

    /// <summary>Initializes a new instance of the <see cref="LemonSqueezyWebhookVerificationException"/> class.</summary>
    public LemonSqueezyWebhookVerificationException()
        : base(DefaultMessage)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="LemonSqueezyWebhookVerificationException"/> class.</summary>
    /// <param name="message">The message that describes the error.</param>
    public LemonSqueezyWebhookVerificationException(string message)
        : base(message)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="LemonSqueezyWebhookVerificationException"/> class.</summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public LemonSqueezyWebhookVerificationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
