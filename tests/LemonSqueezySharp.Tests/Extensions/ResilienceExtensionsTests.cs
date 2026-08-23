using LemonSqueezySharp.Extensions;
using LemonSqueezySharp.Interfaces.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace LemonSqueezySharp.Tests.Extensions;

public class ResilienceExtensionsTests
{
    [Fact]
    public void AddLemonSqueezyResilience_keeps_all_clients_resolvable()
    {
        var services = new ServiceCollection();
        services.AddLemonSqueezy(o => o.ApiKey = "test-key");

        var result = services.AddLemonSqueezyResilience();

        result.Should().BeSameAs(services);

        using var provider = services.BuildServiceProvider();

        // Resolving a typed client builds its HttpClient + handler pipeline (incl. the
        // resilience handler and its options validation), so this also smoke-tests wiring.
        provider.GetRequiredService<ISubscriptionsClient>().Should().NotBeNull();
        provider.GetRequiredService<IWebhooksClient>().Should().NotBeNull();
        provider.GetRequiredService<ILemonSqueezyClient>().Should().NotBeNull();
    }

    [Fact]
    public void AddLemonSqueezyResilience_fluent_overload_chains_from_AddLemonSqueezy()
    {
        var services = new ServiceCollection();

        var builder = services
            .AddLemonSqueezy(o => o.ApiKey = "test-key")
            .AddLemonSqueezyResilience(o => o.Retry.MaxRetryAttempts = 5);

        builder.Should().NotBeNull();

        using var provider = services.BuildServiceProvider();
        provider.GetRequiredService<IOrdersClient>().Should().NotBeNull();
    }

    [Fact]
    public void AddLemonSqueezyResilience_throws_for_null_services()
    {
        var act = () => ((IServiceCollection)null!).AddLemonSqueezyResilience();

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ResourceClientNames_covers_every_resource_client_interface()
    {
        var resourceInterfaces = typeof(ILemonSqueezyClient).Assembly
            .GetExportedTypes()
            .Where(t => t.IsInterface
                && t.Namespace == "LemonSqueezySharp.Interfaces.Clients"
                && t != typeof(ILemonSqueezyClient))
            .Select(t => t.Name)
            .ToArray();

        ServiceCollectionExtensions.ResourceClientNames
            .Should().BeEquivalentTo(resourceInterfaces);
    }
}
