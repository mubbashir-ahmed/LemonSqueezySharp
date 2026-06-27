namespace LemonSqueezy.NET.Exceptions;

/// <summary>
/// Base exception for every error originating in the LemonSqueezy SDK.
/// </summary>
public class LemonSqueezyException : Exception
{
    /// <summary>Initializes a new instance of the <see cref="LemonSqueezyException"/> class.</summary>
    public LemonSqueezyException()
    {
    }

    /// <summary>Initializes a new instance of the <see cref="LemonSqueezyException"/> class.</summary>
    /// <param name="message">The message that describes the error.</param>
    public LemonSqueezyException(string message)
        : base(message)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="LemonSqueezyException"/> class.</summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public LemonSqueezyException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
