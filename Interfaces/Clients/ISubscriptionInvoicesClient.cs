using LemonSqueezy.NET.Interfaces.Clients.Base;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.Orders;
using LemonSqueezy.NET.Models.SubscriptionInvoices;

namespace LemonSqueezy.NET.Interfaces.Clients;

/// <summary>
/// Client for the LemonSqueezy <c>/v1/subscription-invoices</c> endpoints.
/// See https://docs.lemonsqueezy.com/api/subscription-invoices.
/// </summary>
public interface ISubscriptionInvoicesClient : IApiClient
{
    /// <summary>
    /// Retrieves a subscription invoice by id
    /// — <c>GET /v1/subscription-invoices/{id}</c>.
    /// </summary>
    Task<BaseResult<SubscriptionInvoice>> GetAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists subscription invoices, optionally filtered by store / subscription / status
    /// — <c>GET /v1/subscription-invoices</c>.
    /// </summary>
    Task<PagedBaseResult<SubscriptionInvoice>> ListAsync(
        SubscriptionInvoiceListFilter? filter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Generates a PDF for a subscription invoice and returns its short-lived URL
    /// — <c>POST /v1/subscription-invoices/{id}/generate-invoice</c>.
    /// </summary>
    Task<BaseResult<OrderInvoice>> GenerateInvoiceAsync(
        string id,
        GenerateSubscriptionInvoiceRequest? request = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Issues a refund (full or partial) for a subscription invoice
    /// — <c>POST /v1/subscription-invoices/{id}/refund</c>. Returns the updated invoice.
    /// </summary>
    Task<BaseResult<SubscriptionInvoice>> RefundAsync(
        string id,
        RefundSubscriptionInvoiceRequest? request = null,
        CancellationToken cancellationToken = default);
}
