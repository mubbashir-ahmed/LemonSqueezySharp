using System.Text.Json;
using LemonSqueezy.NET.Exceptions;
using LemonSqueezy.NET.Interfaces.Services;
using LemonSqueezy.NET.Models.Affiliates;
using LemonSqueezy.NET.Models.Customers;
using LemonSqueezy.NET.Models.Disputes;
using LemonSqueezy.NET.Models.LicenseKeys;
using LemonSqueezy.NET.Models.Orders;
using LemonSqueezy.NET.Models.SubscriptionInvoices;
using LemonSqueezy.NET.Models.Subscriptions;
using LemonSqueezy.NET.Models.Webhooks;
using LemonSqueezy.NET.Models.Webhooks.Events;
using LemonSqueezy.NET.Serialization;

namespace LemonSqueezy.NET.Services;

/// <inheritdoc cref="IWebhookEventParser"/>
public sealed class WebhookEventParser : IWebhookEventParser
{
    private readonly IWebhookVerifier _verifier;

    public WebhookEventParser(IWebhookVerifier verifier)
    {
        _verifier = verifier ?? throw new ArgumentNullException(nameof(verifier));
    }

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
