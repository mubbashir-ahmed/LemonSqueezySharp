namespace LemonSqueezy.NET.Clients.Interfaces;

/// <summary>
/// Root entry point of the LemonSqueezy SDK. Exposes every resource-specific client
/// as a property so callers only need to inject this single interface.
/// </summary>
public interface ILemonSqueezyClient
{
    IUsersClient Users { get; }
    IStoresClient Stores { get; }
    ICustomersClient Customers { get; }
    IProductsClient Products { get; }
    IVariantsClient Variants { get; }
    IPricesClient Prices { get; }
    IFilesClient Files { get; }
    IOrdersClient Orders { get; }
    IOrderItemsClient OrderItems { get; }
    ISubscriptionsClient Subscriptions { get; }
    ISubscriptionInvoicesClient SubscriptionInvoices { get; }
    ISubscriptionItemsClient SubscriptionItems { get; }
    IUsageRecordsClient UsageRecords { get; }
    IDiscountsClient Discounts { get; }
    IDiscountRedemptionsClient DiscountRedemptions { get; }
    ILicenseKeysClient LicenseKeys { get; }
    ILicenseKeyInstancesClient LicenseKeyInstances { get; }
    ICheckoutsClient Checkouts { get; }
    IWebhooksClient Webhooks { get; }
}
