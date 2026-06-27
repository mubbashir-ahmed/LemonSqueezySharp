# LemonSqueezy.NET — Console Test Harness

A small interactive console for manually exercising every part of the SDK against the **local
build** of `LemonSqueezy.NET` (it uses a `ProjectReference`, not the NuGet package).

## Run

```bash
dotnet run --project samples/LemonSqueezy.NET.ConsoleTester
```

You'll get a menu:

| # | Test | Needs network? |
| - | ---- | -------------- |
| 1 | **DI sanity check** — resolves the root facade and all 21 resource clients | No |
| 2 | **Webhook self-test** — signs a payload, verifies it, parses into a typed `OrderCreatedEvent`, then proves a tampered signature is rejected (both `Try…` and throwing variants) | No |
| 3–9 | Live read calls: `Users.GetMe`, `Stores/Products/Customers/Subscriptions/Orders/Webhooks .List` | Yes |
| 10 | **Typed-exception demo** — GETs a non-existent store and shows the `404` path | Yes |

Tests **1** and **2** run fully offline, so you can validate DI wiring and the whole webhook
pipeline without any credentials.

## Configuration

The API key and webhook secret are read from (later sources win):

1. `appsettings.json` (committed with a placeholder key + `test-secret` webhook secret)
2. Environment variables — `LemonSqueezy__ApiKey`, `LemonSqueezy__WebhookSigningSecret`
3. User secrets (recommended for the real key):

```bash
dotnet user-secrets --project samples/LemonSqueezy.NET.ConsoleTester \
  set "LemonSqueezy:ApiKey" "lsq_your_real_key"
```

Until a real key is set, the live calls (3–10) return `401`, which the harness reports through
the SDK's typed exception hierarchy — itself a useful test of the error-handling path.
