using LemonSqueezy.NET.Clients;
using LemonSqueezy.NET.Configurations;
using LemonSqueezy.NET.Http;
using LemonSqueezy.NET.Interfaces.Clients;
using LemonSqueezy.NET.Interfaces.Services;
using LemonSqueezy.NET.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace LemonSqueezy.NET.Extensions;

/// <summary>
/// Entry-point DI extensions for wiring the LemonSqueezy SDK into
/// <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// The <see cref="HttpClient"/> names under which the SDK registers its typed resource
    /// clients (a typed client's name is its interface name). Kept in sync with the
    /// registrations below and consumed by <see cref="ResilienceExtensions"/>.
    /// </summary>
    internal static readonly string[] ResourceClientNames =
    {
        nameof(ICheckoutsClient),
        nameof(ICustomersClient),
        nameof(IDiscountRedemptionsClient),
        nameof(IDiscountsClient),
        nameof(IFilesClient),
        nameof(ILicenseKeyInstancesClient),
        nameof(ILicenseKeysClient),
        nameof(IOrderItemsClient),
        nameof(IOrdersClient),
        nameof(IPricesClient),
        nameof(IProductsClient),
        nameof(IStoresClient),
        nameof(ISubscriptionInvoicesClient),
        nameof(ISubscriptionItemsClient),
        nameof(ISubscriptionsClient),
        nameof(IUsageRecordsClient),
        nameof(IUsersClient),
        nameof(IVariantsClient),
        nameof(IWebhooksClient),
        nameof(IAffiliatesClient),
        nameof(ILicensesClient),
    };

    /// <summary>
    /// Registers the LemonSqueezy SDK with options bound from <see cref="IConfiguration"/>.
    /// </summary>
    public static IHttpClientBuilder AddLemonSqueezy(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionName = LemonSqueezyOptions.SectionName)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        services.AddOptions<LemonSqueezyOptions>()
            .Bind(configuration.GetSection(sectionName))
            .ValidateOnStart();

        return services.AddLemonSqueezyCore();
    }

    /// <summary>
    /// Registers the LemonSqueezy SDK with imperatively configured options.
    /// </summary>
    public static IHttpClientBuilder AddLemonSqueezy(
        this IServiceCollection services,
        Action<LemonSqueezyOptions> configureOptions)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (configureOptions is null)
        {
            throw new ArgumentNullException(nameof(configureOptions));
        }

        services.AddOptions<LemonSqueezyOptions>()
            .Configure(configureOptions)
            .ValidateOnStart();

        return services.AddLemonSqueezyCore();
    }

    private static IHttpClientBuilder AddLemonSqueezyCore(this IServiceCollection services)
    {
        services.TryAddEnumerable(ServiceDescriptor
            .Singleton<IValidateOptions<LemonSqueezyOptions>, LemonSqueezyOptionsValidator>());

        services.AddTransient<LemonSqueezyAuthenticationHandler>();
        services.AddTransient<LemonSqueezyUserAgentHandler>();

        services.AddSingleton<IWebhookVerifier, WebhookVerifier>();
        services.AddSingleton<IWebhookEventParser, WebhookEventParser>();
        services.AddSingleton<ILemonSqueezyService, LemonSqueezyService>();
        services.AddSingleton<ILemonSqueezyClient, LemonSqueezyClient>();

        IHttpClientBuilder? last = null;
        last = RegisterResourceClient<ICheckoutsClient, CheckoutsClient>(services);
        last = RegisterResourceClient<ICustomersClient, CustomersClient>(services);
        last = RegisterResourceClient<IDiscountRedemptionsClient, DiscountRedemptionsClient>(services);
        last = RegisterResourceClient<IDiscountsClient, DiscountsClient>(services);
        last = RegisterResourceClient<IFilesClient, FilesClient>(services);
        last = RegisterResourceClient<ILicenseKeyInstancesClient, LicenseKeyInstancesClient>(services);
        last = RegisterResourceClient<ILicenseKeysClient, LicenseKeysClient>(services);
        last = RegisterResourceClient<IOrderItemsClient, OrderItemsClient>(services);
        last = RegisterResourceClient<IOrdersClient, OrdersClient>(services);
        last = RegisterResourceClient<IPricesClient, PricesClient>(services);
        last = RegisterResourceClient<IProductsClient, ProductsClient>(services);
        last = RegisterResourceClient<IStoresClient, StoresClient>(services);
        last = RegisterResourceClient<ISubscriptionInvoicesClient, SubscriptionInvoicesClient>(services);
        last = RegisterResourceClient<ISubscriptionItemsClient, SubscriptionItemsClient>(services);
        last = RegisterResourceClient<ISubscriptionsClient, SubscriptionsClient>(services);
        last = RegisterResourceClient<IUsageRecordsClient, UsageRecordsClient>(services);
        last = RegisterResourceClient<IUsersClient, UsersClient>(services);
        last = RegisterResourceClient<IVariantsClient, VariantsClient>(services);
        last = RegisterResourceClient<IWebhooksClient, WebhooksClient>(services);
        last = RegisterResourceClient<IAffiliatesClient, AffiliatesClient>(services);
        last = RegisterResourceClient<ILicensesClient, LicensesClient>(services);

        return last!;
    }

    private static IHttpClientBuilder RegisterResourceClient<TInterface, TImplementation>(IServiceCollection services)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        return services.AddHttpClient<TInterface, TImplementation>((sp, http) =>
        {
            var options = sp.GetRequiredService<IOptions<LemonSqueezyOptions>>().Value;
            http.BaseAddress = options.BaseAddress;
            http.Timeout = options.Timeout;
        })
        .AddHttpMessageHandler<LemonSqueezyAuthenticationHandler>()
        .AddHttpMessageHandler<LemonSqueezyUserAgentHandler>();
    }
}
