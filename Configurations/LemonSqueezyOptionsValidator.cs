using Microsoft.Extensions.Options;

namespace LemonSqueezy.NET.Configurations;

/// <summary>
/// Validates <see cref="LemonSqueezyOptions"/> at startup.
/// </summary>
internal sealed class LemonSqueezyOptionsValidator : IValidateOptions<LemonSqueezyOptions>
{
    public ValidateOptionsResult Validate(string? name, LemonSqueezyOptions options)
    {
        var failures = new List<string>();

        if (string.IsNullOrWhiteSpace(options.ApiKey))
        {
            failures.Add($"{nameof(LemonSqueezyOptions.ApiKey)} is required.");
        }

        if (options.BaseAddress is null || !options.BaseAddress.IsAbsoluteUri)
        {
            failures.Add($"{nameof(LemonSqueezyOptions.BaseAddress)} must be an absolute URI.");
        }

        if (options.Timeout <= TimeSpan.Zero)
        {
            failures.Add($"{nameof(LemonSqueezyOptions.Timeout)} must be greater than zero.");
        }

        return failures.Count == 0
            ? ValidateOptionsResult.Success
            : ValidateOptionsResult.Fail(failures);
    }
}
