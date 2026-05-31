using LemonSqueezy.NET.Clients.Interfaces;

namespace LemonSqueezy.NET.Clients;

/// <summary>
/// Default <see cref="ILemonSqueezyClient"/> implementation. Wires each resource client
/// constructor-injected from DI into a single façade.
/// </summary>
public sealed class LemonSqueezyClient : ILemonSqueezyClient
{
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
        IWebhooksClient webhooks)
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
    }

    public IUsersClient Users { get; }
    public IStoresClient Stores { get; }
    public ICustomersClient Customers { get; }
    public IProductsClient Products { get; }
    public IVariantsClient Variants { get; }
    public IPricesClient Prices { get; }
    public IFilesClient Files { get; }
    public IOrdersClient Orders { get; }
    public IOrderItemsClient OrderItems { get; }
    public ISubscriptionsClient Subscriptions { get; }
    public ISubscriptionInvoicesClient SubscriptionInvoices { get; }
    public ISubscriptionItemsClient SubscriptionItems { get; }
    public IUsageRecordsClient UsageRecords { get; }
    public IDiscountsClient Discounts { get; }
    public IDiscountRedemptionsClient DiscountRedemptions { get; }
    public ILicenseKeysClient LicenseKeys { get; }
    public ILicenseKeyInstancesClient LicenseKeyInstances { get; }
    public ICheckoutsClient Checkouts { get; }
    public IWebhooksClient Webhooks { get; }
}
