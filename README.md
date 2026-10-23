# LemonSqueezySharp

[![NuGet](https://img.shields.io/nuget/v/LemonSqueezySharp.svg)](https://www.nuget.org/packages/LemonSqueezySharp)
[![NuGet Downloads](https://img.shields.io/nuget/dt/LemonSqueezySharp.svg)](https://www.nuget.org/packages/LemonSqueezySharp)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![Build](https://github.com/mubbashir-ahmed/LemonSqueezySharp/actions/workflows/publish.yml/badge.svg)](https://github.com/mubbashir-ahmed/LemonSqueezySharp/actions/workflows/publish.yml)
[![Target Frameworks](https://img.shields.io/badge/targets-net10.0%20%7C%20netstandard2.0-512BD4)](LemonSqueezySharp.csproj)

> Unofficial .NET SDK for the [LemonSqueezy API](https://docs.lemonsqueezy.com/api). Strongly-typed clients, `IHttpClientFactory`-based, DI-first, with built-in webhook signature verification.

> **Status — pre-1.0 (`0.1.0`).** Every documented LemonSqueezy resource is implemented and covered by tests. The public API may still change before 1.0.

---

## Features

- ✅ One typed client per LemonSqueezy resource — `IStoresClient`, `ISubscriptionsClient`, `ICustomersClient`, `IOrdersClient`, …
- ✅ Single root entry point: `ILemonSqueezyClient` exposes every resource client as a property (`client.Subscriptions.GetAsync(id)`)
- ✅ `IHttpClientFactory` + `IOptions<LemonSqueezyOptions>` integration — no static state
- ✅ Strongly-typed exception hierarchy (401/404/422/429 each get their own type)
- ✅ Built-in webhook HMAC-SHA256 signature verification **plus strongly-typed, pattern-matchable webhook events**
- ✅ JSON:API envelope support out of the box (`JsonApiResponse<T>`, `JsonApiCollectionResponse<T>`, paging links)
- ✅ Optional one-call Polly resilience (`AddLemonSqueezyResilience()`) — retry, circuit breaker, timeouts
- ✅ Multi-targets **net10.0** and **netstandard2.0** — runs on modern .NET, .NET Core 2.0+, and .NET Framework 4.7.2+ (see [Compatibility](#compatibility))

---

## Install

```bash
dotnet add package LemonSqueezySharp
```

---

## Compatibility

The library multi-targets **net10.0** and **netstandard2.0**. The `netstandard2.0` target is what gives it broad reach:

| Platform                 | Supported versions                       |
| ------------------------ | ---------------------------------------- |
| .NET / .NET Core         | .NET Core 2.0+, .NET 5, 6, 7, 8, 9, 10   |
| .NET Framework           | 4.7.2+                                    |
| Mono / Xamarin / Unity   | any target supporting .NET Standard 2.0  |

The test suite runs against **net10.0**, **net48**, and **net472**, so the netstandard2.0 / .NET Framework path is verified rather than merely declared. **4.7.2 is the documented minimum floor** — older 4.x versions are technically in-spec for .NET Standard 2.0 but are not tested and may need binding redirects for the modern `System.Text.Json` / `Microsoft.Extensions.*` dependencies.

---

## Quick start

### 1. Register the SDK

```csharp
using LemonSqueezySharp.Extensions;
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
using LemonSqueezySharp.Clients.Interfaces;

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
| `UserAgent`             | `string?`       | `LemonSqueezySharp/{version}`             | Override the outbound `User-Agent`.               |

Validation runs on startup via `IValidateOptions<LemonSqueezyOptions>`, so misconfigurations fail fast.

---

## Resilience (optional)

Call `AddLemonSqueezyResilience()` after `AddLemonSqueezy(...)` to wrap **only** the SDK's HTTP clients in the standard Polly pipeline — retry with exponential backoff + jitter, a circuit breaker, total-request and per-attempt timeouts, and a concurrency limiter. It builds on [`Microsoft.Extensions.Http.Resilience`](https://learn.microsoft.com/dotnet/core/resilience/http-resilience):

```csharp
services
    .AddLemonSqueezy(options => options.ApiKey = "lsq_...")
    .AddLemonSqueezyResilience();

// …or tune the standard pipeline:
services.AddLemonSqueezyResilience(options =>
{
    options.Retry.MaxRetryAttempts = 5;
    options.AttemptTimeout.Timeout = TimeSpan.FromSeconds(15);
});
```

It scopes resilience to the LemonSqueezy clients (it does not touch other `HttpClient`s in your app). LemonSqueezy rate-limits with `429` + `Retry-After`, which the retry strategy honors automatically — so transient `429`/`5xx`/network failures are retried before surfacing as exceptions.

> The standard pipeline only retries idempotent requests by default, so it won't blindly re-issue writes like `CreateAsync`.

---

## Verifying webhooks

### Typed events (recommended)

Inject `IWebhookEventParser` and call `VerifyAndParse` — it checks the `X-Signature` header and returns a strongly-typed event you can pattern-match. Each known event carries its resource in `Data` (e.g. an `OrderCreatedEvent` exposes a typed `Order`):

```csharp
[ApiController]
[Route("webhooks/lemonsqueezy")]
public class WebhookController(IWebhookEventParser webhooks) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Handle()
    {
        using var reader = new StreamReader(Request.Body);
        var body = await reader.ReadToEndAsync();
        var signature = Request.Headers["X-Signature"].ToString();

        if (!webhooks.TryVerifyAndParse(body, signature, out var evt))
        {
            return Unauthorized();
        }

        switch (evt)
        {
            case OrderCreatedEvent e:
                await FulfillAsync(e.Data!);                 // e.Data is an Order
                break;
            case SubscriptionUpdatedEvent e:
                await SyncAsync(e.Data!);                    // e.Data is a Subscription
                break;
            case SubscriptionPaymentFailedEvent e:
                await DunAsync(e.Data!);                     // e.Data is a SubscriptionInvoice
                break;
            case UnknownWebhookEvent e:
                _logger.LogInformation("Unhandled event {Name}", e.EventName);
                break;
        }

        return Ok();
    }
}
```

`VerifyAndParse` is the throwing variant (it raises `LemonSqueezyWebhookVerificationException` on a bad signature); `TryVerifyAndParse` returns `false` instead. Custom data attached at checkout is available on any event via `evt.GetCustomData<T>()`. Event-name constants live in `WebhookEventNames`.

### Low-level verification

If you only need to authenticate the payload and parse it yourself, inject `IWebhookVerifier` and call `Verify(body, signature)`. It uses constant-time comparison (`CryptographicOperations.FixedTimeEquals` on net10.0; a manual fixed-time loop on netstandard2.0).

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
LemonSqueezySharp/
├── LemonSqueezySharp.csproj       multi-target net10.0 + netstandard2.0
├── Directory.Build.props         shared MSBuild props
├── .editorconfig                 code style
│
├── Clients/                      resource client implementations
│   ├── Base/                     ApiClientBase (HTTP plumbing)
│   ├── LemonSqueezyClient.cs     root façade implementation
│   └── <Resource>Client.cs       per-resource implementations
│
├── Interfaces/                   public abstractions
│   ├── Clients/                  one interface per resource (+ Base/ marker)
│   └── Services/                 IWebhookVerifier and friends
│
├── Models/                       DTOs grouped by resource
│   ├── Common/                   JSON:API envelopes (Resource, Response, Links, Meta, Error)
│   ├── Requests/                 shared request shapes (ListFilter, etc.)
│   ├── Webhooks/Events/          typed webhook event hierarchy + event-name catalogue
│   └── <Resource>/               per-resource attribute models
│
├── Services/                     higher-level helpers
│   ├── WebhookVerifier.cs        HMAC-SHA256 verification
│   ├── WebhookEventParser.cs     verify + parse into typed events
│   └── LemonSqueezyService.cs    optional façade combining client + verifier
│
├── Compatibility/                netstandard2.0 polyfills (IsExternalInit)
├── Exceptions/                   typed exception hierarchy
├── Extensions/                   DI + HttpClient + HttpResponseMessage helpers
├── Http/                         DelegatingHandlers (auth, user-agent), endpoint catalogue
├── Configurations/               LemonSqueezyOptions + validator
├── Serialization/                JSON options, UnixDateTimeConverter
│
└── tests/LemonSqueezySharp.Tests/ xUnit + FluentAssertions + NSubstitute (net10.0 / net48 / net472)
```

---

## Supported resources

Every documented LemonSqueezy resource has a fully implemented, tested client. The root `ILemonSqueezyClient` exposes each one as a property (e.g. `client.Subscriptions`, `client.Checkouts`):

| Resource              | Client interface              | Methods                                                     |
| --------------------- | ----------------------------- | ---------------------------------------------------------- |
| Users                 | `IUsersClient`                | `GetMeAsync`                                                |
| Stores                | `IStoresClient`               | `GetAsync`, `ListAsync`                                     |
| Customers             | `ICustomersClient`            | `GetAsync`, `ListAsync`, `CreateAsync`, `UpdateAsync`       |
| Products              | `IProductsClient`             | `GetAsync`, `ListAsync`                                     |
| Variants              | `IVariantsClient`             | `GetAsync`, `ListAsync`                                     |
| Prices                | `IPricesClient`               | `GetAsync`, `ListAsync`                                     |
| Files                 | `IFilesClient`                | `GetAsync`, `ListAsync`                                     |
| Orders                | `IOrdersClient`               | `GetAsync`, `ListAsync`, `GenerateInvoiceAsync`, `RefundAsync` |
| Order items           | `IOrderItemsClient`           | `GetAsync`, `ListAsync`                                     |
| Subscriptions         | `ISubscriptionsClient`        | `GetAsync`, `ListAsync`, `UpdateAsync`, `CancelAsync`       |
| Subscription invoices | `ISubscriptionInvoicesClient` | `GetAsync`, `ListAsync`, `GenerateInvoiceAsync`, `RefundAsync` |
| Subscription items    | `ISubscriptionItemsClient`    | `GetAsync`, `ListAsync`, `UpdateAsync`, `GetCurrentUsageAsync` |
| Usage records         | `IUsageRecordsClient`         | `GetAsync`, `ListAsync`, `CreateAsync`                      |
| Discounts             | `IDiscountsClient`            | `GetAsync`, `ListAsync`, `CreateAsync`, `DeleteAsync`       |
| Discount redemptions  | `IDiscountRedemptionsClient`  | `GetAsync`, `ListAsync`                                     |
| License keys          | `ILicenseKeysClient`          | `GetAsync`, `ListAsync`, `UpdateAsync`                      |
| License key instances | `ILicenseKeyInstancesClient`  | `GetAsync`, `ListAsync`                                     |
| Checkouts             | `ICheckoutsClient`            | `GetAsync`, `ListAsync`, `CreateAsync`                      |
| Webhooks              | `IWebhooksClient`             | `GetAsync`, `ListAsync`, `CreateAsync`, `UpdateAsync`, `DeleteAsync` |
| Affiliates            | `IAffiliatesClient`           | `GetAsync`, `ListAsync`                                     |
| Licenses (license API) | `ILicensesClient`            | `ActivateAsync`, `DeactivateAsync`, `ValidateAsync`        |

---

## Development

Requirements: .NET 10 SDK. The test project multi-targets **net10.0**, **net48**, and **net472**; the two .NET Framework targets only execute on Windows (with the .NET Framework 4.7.2/4.8 developer packs installed). On non-Windows hosts only the net10.0 target runs.

```bash
# Restore + build
dotnet build

# Run the tests (all target frameworks)
dotnet test

# Run only the modern .NET target
dotnet test -f net10.0
```

The repository uses `Directory.Build.props`, `.editorconfig`, and `TreatWarningsAsErrors` — please run `dotnet build` and `dotnet test` before submitting a PR.

---

## Contributing

Contributions are welcome: bug reports, fixes, new endpoints, docs, and tests.

1. Check [existing issues](https://github.com/mubbashir-ahmed/LemonSqueezySharp/issues) or open one to discuss larger changes first.
2. Fork the repo and create a branch from `main`.
3. Make your change, with tests where it makes sense.
4. Run `dotnet build -c Release` and `dotnet test -c Release`.
5. Open a pull request against `main`.

See [CONTRIBUTING.md](CONTRIBUTING.md) for setup details and guidelines. By contributing, you agree that your contributions are licensed under the project's MIT license.

---

## License

MIT. See [LICENSE](LICENSE).

LemonSqueezy® is a trademark of Lemon Squeezy, LLC. This project is unofficial and not endorsed by Lemon Squeezy.
