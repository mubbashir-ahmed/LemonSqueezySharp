using LemonSqueezySharp.Interfaces.Clients;

namespace LemonSqueezySharp.Clients;

/// <summary>
/// Default <see cref="ILemonSqueezyClient"/> implementation. Wires each resource client
/// constructor-injected from DI into a single façade.
/// </summary>
public sealed class LemonSqueezyClient : ILemonSqueezyClient
{
    /// <summary>Initializes a new instance of the <see cref="LemonSqueezyClient"/> class.</summary>
    public LemonSqueezyClient(
        IUsersClient users,
        IStoresClient stores,
        ICustomersClient customers,
        IProductsClient products,
        IVariantsClient variants,
        IPricesClient prices,
        IFilesClient files,
        IOrdersClient orders,
        IOrderItemsClient orderItems,
        ISubscriptionsClient subscriptions,
        ISubscriptionInvoicesClient subscriptionInvoices,
        ISubscriptionItemsClient subscriptionItems,
        IUsageRecordsClient usageRecords,
        IDiscountsClient discounts,
        IDiscountRedemptionsClient discountRedemptions,
        ILicenseKeysClient licenseKeys,
        ILicenseKeyInstancesClient licenseKeyInstances,
        ICheckoutsClient checkouts,
        IWebhooksClient webhooks,
        IAffiliatesClient affiliates,
        ILicensesClient licenses)
    {
        Users = users;
        Stores = stores;
        Customers = customers;
        Products = products;
        Variants = variants;
        Prices = prices;
        Files = files;
        Orders = orders;
        OrderItems = orderItems;
        Subscriptions = subscriptions;
        SubscriptionInvoices = subscriptionInvoices;
        SubscriptionItems = subscriptionItems;
        UsageRecords = usageRecords;
        Discounts = discounts;
        DiscountRedemptions = discountRedemptions;
        LicenseKeys = licenseKeys;
        LicenseKeyInstances = licenseKeyInstances;
        Checkouts = checkouts;
        Webhooks = webhooks;
        Affiliates = affiliates;
        Licenses = licenses;
    }

    /// <inheritdoc />
    public IUsersClient Users { get; }
    /// <inheritdoc />
    public IStoresClient Stores { get; }
    /// <inheritdoc />
    public ICustomersClient Customers { get; }
    /// <inheritdoc />
    public IProductsClient Products { get; }
    /// <inheritdoc />
    public IVariantsClient Variants { get; }
    /// <inheritdoc />
    public IPricesClient Prices { get; }
    /// <inheritdoc />
    public IFilesClient Files { get; }
    /// <inheritdoc />
    public IOrdersClient Orders { get; }
    /// <inheritdoc />
    public IOrderItemsClient OrderItems { get; }
    /// <inheritdoc />
    public ISubscriptionsClient Subscriptions { get; }
    /// <inheritdoc />
    public ISubscriptionInvoicesClient SubscriptionInvoices { get; }
    /// <inheritdoc />
    public ISubscriptionItemsClient SubscriptionItems { get; }
    /// <inheritdoc />
    public IUsageRecordsClient UsageRecords { get; }
    /// <inheritdoc />
    public IDiscountsClient Discounts { get; }
    /// <inheritdoc />
    public IDiscountRedemptionsClient DiscountRedemptions { get; }
    /// <inheritdoc />
    public ILicenseKeysClient LicenseKeys { get; }
    /// <inheritdoc />
    public ILicenseKeyInstancesClient LicenseKeyInstances { get; }
    /// <inheritdoc />
    public ICheckoutsClient Checkouts { get; }
    /// <inheritdoc />
    public IWebhooksClient Webhooks { get; }
    /// <inheritdoc />
    public IAffiliatesClient Affiliates { get; }
    /// <inheritdoc />
    public ILicensesClient Licenses { get; }
}
