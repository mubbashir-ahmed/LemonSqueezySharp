namespace LemonSqueezy.NET.Exceptions;

/// <summary>
/// Base exception for every error originating in the LemonSqueezy SDK.
/// </summary>
public class LemonSqueezyException : Exception
{
    public LemonSqueezyException()
    {
    }

    public LemonSqueezyException(string message)
        : base(message)
    {
    }

    public LemonSqueezyException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
