using LemonSqueezySharp.Configurations;

namespace LemonSqueezySharp.Tests.Configuration;

public class LemonSqueezyOptionsValidatorTests
{
    private readonly LemonSqueezyOptionsValidator _validator = new();

    [Fact]
    public void Valid_options_pass()
    {
        var options = new LemonSqueezyOptions { ApiKey = "test-key" };

        var result = _validator.Validate(name: null, options);

        result.Succeeded.Should().BeTrue();
    }

    [Fact]
    public void Missing_api_key_fails()
    {
        var options = new LemonSqueezyOptions { ApiKey = string.Empty };

        var result = _validator.Validate(name: null, options);

        result.Failed.Should().BeTrue();
        result.FailureMessage.Should().Contain(nameof(LemonSqueezyOptions.ApiKey));
    }

    [Fact]
    public void Non_positive_timeout_fails()
    {
        var options = new LemonSqueezyOptions
        {
            ApiKey = "test-key",
            Timeout = TimeSpan.Zero,
        };

        var result = _validator.Validate(name: null, options);

        result.Failed.Should().BeTrue();
        result.FailureMessage.Should().Contain(nameof(LemonSqueezyOptions.Timeout));
    }
}
