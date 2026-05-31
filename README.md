# LemonSqueezy.NET

[![NuGet](https://img.shields.io/nuget/v/LemonSqueezy.NET.svg)](https://www.nuget.org/packages/LemonSqueezy.NET)
[![NuGet Downloads](https://img.shields.io/nuget/dt/LemonSqueezy.NET.svg)](https://www.nuget.org/packages/LemonSqueezy.NET)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![Target Frameworks](https://img.shields.io/badge/targets-net10.0%20%7C%20netstandard2.0-512BD4)](LemonSqueezy.NET.csproj)

> Unofficial .NET SDK for the [LemonSqueezy API](https://docs.lemonsqueezy.com/api). Strongly-typed clients, `IHttpClientFactory`-based, DI-first, with built-in webhook signature verification.

> ⚠️ **Status — early scaffolding.** The infrastructure (DI, options, HTTP pipeline, exceptions, JSON:API envelopes, webhook verification) is in place. Resource endpoint methods (`GetAsync`, `ListAsync`, `CreateAsync`, …) are still being filled in resource-by-resource. See [Roadmap](#roadmap).

---

## Features

- ✅ One typed client per LemonSqueezy resource — `IStoresClient`, `ISubscriptionsClient`, `ICustomersClient`, `IOrdersClient`, …
- ✅ Single root entry point: `ILemonSqueezyClient` exposes every resource client as a property (`client.Subscriptions.GetAsync(id)`)
- ✅ `IHttpClientFactory` + `IOptions<LemonSqueezyOptions>` integration — no static state
- ✅ Strongly-typed exception hierarchy (401/404/422/429 each get their own type)
- ✅ Built-in webhook HMAC-SHA256 signature verification
- ✅ JSON:API envelope support out of the box (`JsonApiResponse<T>`, `JsonApiCollectionResponse<T>`, paging links)
- ✅ Multi-targets **net10.0** and **netstandard2.0**

---

## Install

```bash
dotnet add package LemonSqueezy.NET
```

---

## Quick start

### 1. Register the SDK

```csharp
using LemonSqueezy.NET.Extensions;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddLemonSqueezy(options =>
{
    options.ApiKey = Environment.GetEnvironmentVariable("LEMONSQUEEZY_API_KEY")!;
    options.WebhookSigningSecret = Environment.GetEnvironmentVariable("LEMONSQUEEZY_WEBHOOK_SECRET");
});
```

Or bind from `IConfiguration`:

```csharp
// appsettings.json
// {
//   "LemonSqueezy": {
//     "ApiKey": "lsq_...",
//     "WebhookSigningSecret": "whsec_...",
//     "Timeout": "00:00:30"
//   }
// }

services.AddLemonSqueezy(builder.Configuration);
```

### 2. Use it

```csharp
using LemonSqueezy.NET.Clients.Interfaces;

public class BillingService(ILemonSqueezyClient lemonSqueezy)
{
    public async Task<Subscription?> GetSubscriptionAsync(string id, CancellationToken ct)
    {
        var response = await lemonSqueezy.Subscriptions.GetAsync(id, ct);
        return response?.Data;
    }
}
```

---

## Configuration

All options live on `LemonSqueezyOptions` (binding section: `LemonSqueezy`):

| Property                | Type            | Default                                  | Description                                       |
| ----------------------- | --------------- | ---------------------------------------- | ------------------------------------------------- |
| `ApiKey`                | `string`        | _(required)_                             | Personal access token sent as `Bearer` header.    |
| `BaseAddress`           | `Uri`           | `https://api.lemonsqueezy.com/v1/`       | Override only for testing or proxies.             |
| `Timeout`               | `TimeSpan`      | `100s`                                   | Per-request timeout applied to every `HttpClient`.|
| `WebhookSigningSecret`  | `string?`       | `null`                                   | Required if you use `IWebhookVerifier`.           |
| `UserAgent`             | `string?`       | `LemonSqueezy.NET/{version}`             | Override the outbound `User-Agent`.               |

Validation runs on startup via `IValidateOptions<LemonSqueezyOptions>`, so misconfigurations fail fast.

---

## Verifying webhooks

Inject `IWebhookVerifier` and validate the `X-Signature` header against the raw request body:

```csharp
[ApiController]
[Route("webhooks/lemonsqueezy")]
public class WebhookController(IWebhookVerifier verifier) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Handle()
    {
        using var reader = new StreamReader(Request.Body);
        var body = await reader.ReadToEndAsync();

        var signature = Request.Headers["X-Signature"].ToString();
        if (!verifier.Verify(body, signature))
        {
            return Unauthorized();
        }

        // Parse the event and dispatch to your handler:
        var evt = JsonSerializer.Deserialize<WebhookEvent>(body, LemonSqueezyJsonOptions.Default);
        // switch (evt?.Meta?.EventName) { ... }

        return Ok();
    }
}
```

The verifier uses constant-time comparison (`CryptographicOperations.FixedTimeEquals` on net10.0; manual fixed-time loop on netstandard2.0).

---

## Error handling

Non-2xx responses are translated into a typed exception hierarchy:

| HTTP status | Exception                                | Extras                       |
| ----------- | ---------------------------------------- | ---------------------------- |
| 401         | `LemonSqueezyAuthenticationException`    |                              |
| 404         | `LemonSqueezyNotFoundException`          |                              |
| 422         | `LemonSqueezyValidationException`        | `Errors` (string list)       |
| 429         | `LemonSqueezyRateLimitException`         | `RetryAfter` (`TimeSpan?`)   |
| other       | `LemonSqueezyApiException`               | `StatusCode`                 |

All inherit from `LemonSqueezyException`. Every exception carries the raw response body and the server's `x-request-id` (when present) for diagnostics.

```csharp
try
{
    await client.Subscriptions.GetAsync("missing-id", ct);
}
catch (LemonSqueezyNotFoundException ex)
{
    _logger.LogWarning("Subscription not found. Request id: {RequestId}", ex.RequestId);
}
catch (LemonSqueezyRateLimitException ex)
{
    await Task.Delay(ex.RetryAfter ?? TimeSpan.FromSeconds(5), ct);
}
```

---

## Project structure

```
LemonSqueezy.NET/
├── LemonSqueezy.NET.csproj       multi-target net10.0 + netstandard2.0
├── Directory.Build.props         shared MSBuild props
├── .editorconfig                 code style
│
├── Clients/                      resource client implementations
│   ├── Base/                     ApiClientBase (HTTP plumbing)
│   ├── Interfaces/               one interface per resource
│   │   └── Base/                 IApiClient (marker)
│   ├── LemonSqueezyClient.cs     root façade implementation
│   └── <Resource>Client.cs       per-resource implementations
│
├── Models/                       DTOs grouped by resource
│   ├── Common/                   JSON:API envelopes (Resource, Response, Links, Meta, Error)
│   ├── Requests/                 shared request shapes (ListFilter, etc.)
│   └── <Resource>/               per-resource attribute models
│
├── Services/                     higher-level helpers
│   ├── WebhookVerifier.cs        HMAC-SHA256 verification
│   └── LemonSqueezyService.cs    optional façade combining client + verifier
│
├── Exceptions/                   typed exception hierarchy
├── Extensions/                   DI + HttpClient + HttpResponseMessage helpers
├── Http/                         DelegatingHandlers (auth, user-agent), endpoint catalogue
├── Configurations/               LemonSqueezyOptions + validator
├── Serialization/                JSON options, UnixDateTimeConverter
│
└── tests/LemonSqueezy.NET.Tests/ xUnit + FluentAssertions + NSubstitute
```

---

## Supported resources

The SDK scaffolds clients for every documented LemonSqueezy resource:

| Resource              | Client interface              | Status      |
| --------------------- | ----------------------------- | ----------- |
| Users                 | `IUsersClient`                | 🚧 scaffold |
| Stores                | `IStoresClient`               | 🚧 scaffold |
| Customers             | `ICustomersClient`            | 🚧 scaffold |
| Products              | `IProductsClient`             | 🚧 scaffold |
| Variants              | `IVariantsClient`             | 🚧 scaffold |
| Prices                | `IPricesClient`               | 🚧 scaffold |
| Files                 | `IFilesClient`                | 🚧 scaffold |
| Orders                | `IOrdersClient`               | 🚧 scaffold |
| Order items           | `IOrderItemsClient`           | 🚧 scaffold |
| Subscriptions         | `ISubscriptionsClient`        | 🚧 scaffold |
| Subscription invoices | `ISubscriptionInvoicesClient` | 🚧 scaffold |
| Subscription items    | `ISubscriptionItemsClient`    | 🚧 scaffold |
| Usage records         | `IUsageRecordsClient`         | 🚧 scaffold |
| Discounts             | `IDiscountsClient`            | 🚧 scaffold |
| Discount redemptions  | `IDiscountRedemptionsClient`  | 🚧 scaffold |
| License keys          | `ILicenseKeysClient`          | 🚧 scaffold |
| License key instances | `ILicenseKeyInstancesClient`  | 🚧 scaffold |
| Checkouts             | `ICheckoutsClient`            | 🚧 scaffold |
| Webhooks              | `IWebhooksClient`             | 🚧 scaffold |

---

## Roadmap

- [ ] Implement `ISubscriptionsClient` endpoints (highest-value first target)
- [ ] Implement `ICustomersClient`, `IOrdersClient`, `ICheckoutsClient`
- [ ] Implement the remaining resource clients
- [ ] Add Polly-based resilience extension (`AddStandardResilienceHandler` integration)
- [ ] Strongly-typed webhook event payloads (one record per event name)
- [ ] Sample apps: ASP.NET Core webhook handler, console subscription explorer
- [ ] CI workflow + automated NuGet publishing

Pull requests are very welcome — pick a resource from the table and open a PR.

---

## Development

Requirements: .NET 10 SDK.

```bash
# Restore + build
dotnet build

# Run the tests
dotnet test
```

The repository uses `Directory.Build.props`, `.editorconfig`, and `TreatWarningsAsErrors` — please run `dotnet build` and `dotnet test` before submitting a PR.

---

## License

MIT. See [LICENSE](LICENSE).

LemonSqueezy® is a trademark of Lemon Squeezy, LLC. This project is unofficial and not endorsed by Lemon Squeezy.
