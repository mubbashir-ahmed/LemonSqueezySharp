using System.Text.Json;
using LemonSqueezySharp.Exceptions;
using LemonSqueezySharp.Interfaces.Services;
using LemonSqueezySharp.Models.Affiliates;
using LemonSqueezySharp.Models.Customers;
using LemonSqueezySharp.Models.Disputes;
using LemonSqueezySharp.Models.LicenseKeys;
using LemonSqueezySharp.Models.Orders;
using LemonSqueezySharp.Models.SubscriptionInvoices;
using LemonSqueezySharp.Models.Subscriptions;
using LemonSqueezySharp.Models.Webhooks;
using LemonSqueezySharp.Models.Webhooks.Events;
using LemonSqueezySharp.Serialization;

namespace LemonSqueezySharp.Services;

/// <inheritdoc cref="IWebhookEventParser"/>
public sealed class WebhookEventParser : IWebhookEventParser
{
    private readonly IWebhookVerifier _verifier;

    /// <summary>Initializes a new instance of the <see cref="WebhookEventParser"/> class.</summary>
    public WebhookEventParser(IWebhookVerifier verifier)
    {
        _verifier = verifier ?? throw new ArgumentNullException(nameof(verifier));
    }

    /// <inheritdoc />
    public LemonSqueezyWebhookEvent Parse(string rawBody)
    {
        if (rawBody is null)
        {
            throw new ArgumentNullException(nameof(rawBody));
        }

        WebhookEvent? raw;
        try
        {
            raw = JsonSerializer.Deserialize<WebhookEvent>(rawBody, LemonSqueezyJsonOptions.Default);
        }
        catch (JsonException ex)
        {
            throw new LemonSqueezyException("The webhook payload was not valid JSON.", ex);
        }

        if (raw?.Meta is null)
        {
            throw new LemonSqueezyException("The webhook payload was missing the required 'meta' object.");
        }

        var meta = raw.Meta;
        var data = raw.Data;

        return meta.EventName switch
        {
            WebhookEventNames.OrderCreated => Create<OrderCreatedEvent, Order>(meta, data),
            WebhookEventNames.OrderRefunded => Create<OrderRefundedEvent, Order>(meta, data),

            WebhookEventNames.CustomerUpdated => Create<CustomerUpdatedEvent, Customer>(meta, data),

            WebhookEventNames.DisputeCreated => Create<DisputeCreatedEvent, Dispute>(meta, data),
            WebhookEventNames.DisputeResolved => Create<DisputeResolvedEvent, Dispute>(meta, data),

            WebhookEventNames.SubscriptionCreated => Create<SubscriptionCreatedEvent, Subscription>(meta, data),
            WebhookEventNames.SubscriptionUpdated => Create<SubscriptionUpdatedEvent, Subscription>(meta, data),
            WebhookEventNames.SubscriptionCancelled => Create<SubscriptionCancelledEvent, Subscription>(meta, data),
            WebhookEventNames.SubscriptionResumed => Create<SubscriptionResumedEvent, Subscription>(meta, data),
            WebhookEventNames.SubscriptionExpired => Create<SubscriptionExpiredEvent, Subscription>(meta, data),
            WebhookEventNames.SubscriptionPaused => Create<SubscriptionPausedEvent, Subscription>(meta, data),
            WebhookEventNames.SubscriptionUnpaused => Create<SubscriptionUnpausedEvent, Subscription>(meta, data),
            WebhookEventNames.SubscriptionPlanChanged => Create<SubscriptionPlanChangedEvent, Subscription>(meta, data),

            WebhookEventNames.SubscriptionPaymentSuccess => Create<SubscriptionPaymentSuccessEvent, SubscriptionInvoice>(meta, data),
            WebhookEventNames.SubscriptionPaymentFailed => Create<SubscriptionPaymentFailedEvent, SubscriptionInvoice>(meta, data),
            WebhookEventNames.SubscriptionPaymentRecovered => Create<SubscriptionPaymentRecoveredEvent, SubscriptionInvoice>(meta, data),
            WebhookEventNames.SubscriptionPaymentRefunded => Create<SubscriptionPaymentRefundedEvent, SubscriptionInvoice>(meta, data),

            WebhookEventNames.LicenseKeyCreated => Create<LicenseKeyCreatedEvent, LicenseKey>(meta, data),
            WebhookEventNames.LicenseKeyUpdated => Create<LicenseKeyUpdatedEvent, LicenseKey>(meta, data),

            WebhookEventNames.AffiliateActivated => Create<AffiliateActivatedEvent, Affiliate>(meta, data),

            _ => new UnknownWebhookEvent { Meta = meta, RawData = data },
        };
    }

    /// <inheritdoc />
    public LemonSqueezyWebhookEvent VerifyAndParse(string rawBody, string signatureHeader)
    {
        if (rawBody is null)
        {
            throw new ArgumentNullException(nameof(rawBody));
        }

        if (!_verifier.Verify(rawBody, signatureHeader))
        {
            throw new LemonSqueezyWebhookVerificationException();
        }

        return Parse(rawBody);
    }

    /// <inheritdoc />
    public bool TryVerifyAndParse(string rawBody, string signatureHeader, out LemonSqueezyWebhookEvent? webhookEvent)
    {
        webhookEvent = null;

        if (rawBody is null || !_verifier.Verify(rawBody, signatureHeader))
        {
            return false;
        }

        webhookEvent = Parse(rawBody);
        return true;
    }

    private static TEvent Create<TEvent, TResource>(WebhookMeta meta, JsonElement data)
        where TEvent : LemonSqueezyWebhookEvent<TResource>, new()
        => new() { Meta = meta, Data = DeserializeData<TResource>(data) };

    private static TResource? DeserializeData<TResource>(JsonElement data)
        => data.ValueKind is JsonValueKind.Undefined or JsonValueKind.Null
            ? default
            : data.Deserialize<TResource>(LemonSqueezyJsonOptions.Default);
}
