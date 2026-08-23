using LemonSqueezySharp.Configurations;
using LemonSqueezySharp.Extensions;
using LemonSqueezySharp.Interfaces.Clients;
using LemonSqueezySharp.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LemonSqueezySharp.Tests.Extensions;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddLemonSqueezy_with_action_resolves_root_client()
    {
        var services = new ServiceCollection();
        services.AddLemonSqueezy(o => o.ApiKey = "test-key");

        using var provider = services.BuildServiceProvider();

        provider.GetRequiredService<ILemonSqueezyClient>().Should().NotBeNull();
        provider.GetRequiredService<ILemonSqueezyService>().Should().NotBeNull();
        provider.GetRequiredService<IWebhookVerifier>().Should().NotBeNull();
    }

    [Fact]
    public void AddLemonSqueezy_with_configuration_binds_options()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["LemonSqueezy:ApiKey"] = "from-config",
                ["LemonSqueezy:Timeout"] = "00:01:30",
            })
            .Build();

        var services = new ServiceCollection();
        services.AddLemonSqueezy(configuration);

        using var provider = services.BuildServiceProvider();

        var options = provider.GetRequiredService<IOptions<LemonSqueezyOptions>>().Value;
        options.ApiKey.Should().Be("from-config");
        options.Timeout.Should().Be(TimeSpan.FromSeconds(90));
    }
}
