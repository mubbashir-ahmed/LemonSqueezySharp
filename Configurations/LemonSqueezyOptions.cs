namespace LemonSqueezySharp.Configurations;

/// <summary>
/// Configuration options for the LemonSqueezy SDK.
/// </summary>
public sealed class LemonSqueezyOptions
{
    /// <summary>
    /// Configuration section name used when binding from <c>IConfiguration</c>.
    /// </summary>
    public const string SectionName = "LemonSqueezy";

    /// <summary>
    /// Default base address for the LemonSqueezy REST API.
    /// </summary>
    public static readonly Uri DefaultBaseAddress = new("https://api.lemonsqueezy.com/v1/");

    /// <summary>
    /// API key (personal access token) used to authenticate requests.
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// Base address of the LemonSqueezy API. Override only for testing or proxies.
    /// </summary>
    public Uri BaseAddress { get; set; } = DefaultBaseAddress;

    /// <summary>
    /// Default per-request timeout. Defaults to 100 seconds.
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(100);

    /// <summary>
    /// Optional secret used to verify incoming webhook signatures.
    /// </summary>
    public string? WebhookSigningSecret { get; set; }

    /// <summary>
    /// Optional override for the User-Agent header sent with every request.
    /// </summary>
    public string? UserAgent { get; set; }
}
