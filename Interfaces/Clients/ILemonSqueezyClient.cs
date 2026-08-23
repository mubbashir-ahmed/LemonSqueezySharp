namespace LemonSqueezySharp.Interfaces.Clients;

/// <summary>
/// Root entry point of the LemonSqueezy SDK. Exposes every resource-specific client
/// as a property so callers only need to inject this single interface.
/// </summary>
public interface ILemonSqueezyClient
{
    /// <summary>Client for the LemonSqueezy users endpoints.</summary>
    IUsersClient Users { get; }

    /// <summary>Client for the LemonSqueezy stores endpoints.</summary>
    IStoresClient Stores { get; }

    /// <summary>Client for the LemonSqueezy customers endpoints.</summary>
    ICustomersClient Customers { get; }

    /// <summary>Client for the LemonSqueezy products endpoints.</summary>
    IProductsClient Products { get; }

    /// <summary>Client for the LemonSqueezy variants endpoints.</summary>
    IVariantsClient Variants { get; }

    /// <summary>Client for the LemonSqueezy prices endpoints.</summary>
    IPricesClient Prices { get; }

    /// <summary>Client for the LemonSqueezy files endpoints.</summary>
    IFilesClient Files { get; }

    /// <summary>Client for the LemonSqueezy orders endpoints.</summary>
    IOrdersClient Orders { get; }

    /// <summary>Client for the LemonSqueezy order items endpoints.</summary>
    IOrderItemsClient OrderItems { get; }

    /// <summary>Client for the LemonSqueezy subscriptions endpoints.</summary>
    ISubscriptionsClient Subscriptions { get; }

    /// <summary>Client for the LemonSqueezy subscription invoices endpoints.</summary>
    ISubscriptionInvoicesClient SubscriptionInvoices { get; }

    /// <summary>Client for the LemonSqueezy subscription items endpoints.</summary>
    ISubscriptionItemsClient SubscriptionItems { get; }

    /// <summary>Client for the LemonSqueezy usage records endpoints.</summary>
    IUsageRecordsClient UsageRecords { get; }

    /// <summary>Client for the LemonSqueezy discounts endpoints.</summary>
    IDiscountsClient Discounts { get; }

    /// <summary>Client for the LemonSqueezy discount redemptions endpoints.</summary>
    IDiscountRedemptionsClient DiscountRedemptions { get; }

    /// <summary>Client for the LemonSqueezy license keys endpoints.</summary>
    ILicenseKeysClient LicenseKeys { get; }

    /// <summary>Client for the LemonSqueezy license key instances endpoints.</summary>
    ILicenseKeyInstancesClient LicenseKeyInstances { get; }

    /// <summary>Client for the LemonSqueezy checkouts endpoints.</summary>
    ICheckoutsClient Checkouts { get; }

    /// <summary>Client for the LemonSqueezy webhooks endpoints.</summary>
    IWebhooksClient Webhooks { get; }

    /// <summary>Client for the LemonSqueezy affiliates endpoints.</summary>
    IAffiliatesClient Affiliates { get; }

    /// <summary>Client for the LemonSqueezy License API endpoints.</summary>
    ILicensesClient Licenses { get; }
}
