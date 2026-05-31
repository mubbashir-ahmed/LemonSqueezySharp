# LemonSqueezy.NET

Unofficial .NET SDK for the [LemonSqueezy API](https://docs.lemonsqueezy.com/api).

> Status: project scaffolding only. Resource endpoints are stubbed and will be filled in next.

## Install

```bash
dotnet add package LemonSqueezy.NET
```

Targets `net10.0` and `netstandard2.0`.

## Quick start

```csharp
using LemonSqueezy.NET.Clients;
using LemonSqueezy.NET.Extensions;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddLemonSqueezy(options =>
{
    options.ApiKey = Environment.GetEnvironmentVariable("LEMONSQUEEZY_API_KEY")!;
    options.WebhookSigningSecret = Environment.GetEnvironmentVariable("LEMONSQUEEZY_WEBHOOK_SECRET");
});

using var provider = services.BuildServiceProvider();
var client = provider.GetRequiredService<ILemonSqueezyClient>();

// var subscription = await client.Subscriptions.GetAsync("123");
```

Binding from `IConfiguration`:

```csharp
services.AddLemonSqueezy(configuration); // expects a "LemonSqueezy" section
```

## Project structure

```
LemonSqueezy.NET/
├── LemonSqueezy.NET.csproj          multi-target net10.0 + netstandard2.0
├── Directory.Build.props            shared MSBuild props
├── .editorconfig                    code style
├── Clients/                         one resource client per LemonSqueezy endpoint
│   ├── Base/                        ApiClientBase, IApiClient
│   └── ILemonSqueezyClient.cs       root façade exposing every resource
├── Models/                          DTOs, grouped by resource
│   ├── Common/                      JSON:API envelopes (Resource, Response, Links, Meta)
│   ├── Requests/                    shared request shapes (ListFilter, etc.)
│   └── <Resource>/                  per-resource attribute models
├── Services/                        higher-level helpers (WebhookVerifier, façade)
├── Exceptions/                      typed exception hierarchy
├── Extensions/                      DI + HttpClient + HttpResponseMessage helpers
├── Http/                            DelegatingHandlers (auth, user-agent), endpoint paths
├── Configuration/                   LemonSqueezyOptions + validator
├── Serialization/                   JSON options, UnixDateTimeConverter
└── tests/LemonSqueezy.NET.Tests/    xUnit + FluentAssertions + NSubstitute
```

## Verifying webhooks

```csharp
public class WebhookController(IWebhookVerifier verifier) : ControllerBase
{
    [HttpPost("/webhooks/lemonsqueezy")]
    public async Task<IActionResult> Handle()
    {
        using var reader = new StreamReader(Request.Body);
        var body = await reader.ReadToEndAsync();

        var signature = Request.Headers["X-Signature"].ToString();
        if (!verifier.Verify(body, signature))
        {
            return Unauthorized();
        }

        // ...handle event
        return Ok();
    }
}
```

## Exceptions

| Status | Exception |
| --- | --- |
| 401 | `LemonSqueezyAuthenticationException` |
| 404 | `LemonSqueezyNotFoundException` |
| 422 | `LemonSqueezyValidationException` (exposes `Errors`) |
| 429 | `LemonSqueezyRateLimitException` (exposes `RetryAfter`) |
| other non-2xx | `LemonSqueezyApiException` |

All inherit from `LemonSqueezyException`.

## License

MIT.
