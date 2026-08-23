using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using LemonSqueezySharp.Configurations;
using LemonSqueezySharp.Exceptions;
using LemonSqueezySharp.Extensions;
using LemonSqueezySharp.Interfaces.Clients;
using LemonSqueezySharp.Interfaces.Services;
using LemonSqueezySharp.Models.Common;
using LemonSqueezySharp.Models.Webhooks.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

// ---------------------------------------------------------------------------------------------
// LemonSqueezySharp manual test harness.
//
// A small interactive console you can use to confirm each piece of the SDK works against a real
// account (set an API key), plus an offline webhook self-test that needs no network at all.
//
// Configure the API key in any of these (later wins): appsettings.json, an env var, or secrets:
//   * appsettings.json            -> "LemonSqueezy:ApiKey"
//   * environment variable        -> set LemonSqueezy__ApiKey=lsq_...
//   * user secrets (recommended)  -> dotnet user-secrets set "LemonSqueezy:ApiKey" "lsq_..."
//
// Run with:  dotnet run --project samples/LemonSqueezySharp.ConsoleTester
// ---------------------------------------------------------------------------------------------

// Pin the content root to the app's output folder so appsettings.json is found regardless of
// the working directory `dotnet run` is invoked from.
var builder = Host.CreateApplicationBuilder(new HostApplicationBuilderSettings
{
    Args = args,
    ContentRootPath = AppContext.BaseDirectory,
});
builder.Configuration.AddUserSecrets<Program>(optional: true);

// Register the SDK from configuration, then layer the optional Polly resilience pipeline on top
// so this harness also exercises AddLemonSqueezyResilience().
builder.Services
    .AddLemonSqueezy(builder.Configuration)
    .Services
    .AddLemonSqueezyResilience();

using var host = builder.Build();

var client = host.Services.GetRequiredService<ILemonSqueezyClient>();
var parser = host.Services.GetRequiredService<IWebhookEventParser>();
var options = host.Services.GetRequiredService<IOptions<LemonSqueezyOptions>>().Value;

PrintHeader();

while (true)
{
    Console.WriteLine();
    Console.WriteLine("Choose a test:");
    Console.WriteLine("  1) DI sanity check        (offline — resolves every resource client)");
    Console.WriteLine("  2) Webhook self-test      (offline — sign, verify, typed-parse, tamper)");
    Console.WriteLine("  3) Users.GetMe            (API)");
    Console.WriteLine("  4) Stores.List            (API)");
    Console.WriteLine("  5) Products.List          (API)");
    Console.WriteLine("  6) Customers.List         (API)");
    Console.WriteLine("  7) Subscriptions.List     (API)");
    Console.WriteLine("  8) Orders.List            (API)");
    Console.WriteLine("  9) Webhooks.List          (API)");
    Console.WriteLine(" 10) Typed-exception demo   (API — GET a non-existent store -> 404)");
    Console.WriteLine("  0) Exit");
    Console.Write("> ");

    var choice = Console.ReadLine()?.Trim();
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            DiSanityCheck(client);
            break;
        case "2":
            await WebhookSelfTestAsync(parser, options);
            break;
        case "3":
            await CallAsync("Users.GetMe", async () =>
            {
                var me = await client.Users.GetMeAsync();
                PrintOne(me.Data?.Id, me.Data?.Attributes is null ? null : Json(me.Data.Attributes));
            });
            break;
        case "4":
            await CallAsync("Stores.List", async () => PrintList(await client.Stores.ListAsync()));
            break;
        case "5":
            await CallAsync("Products.List", async () => PrintList(await client.Products.ListAsync()));
            break;
        case "6":
            await CallAsync("Customers.List", async () => PrintList(await client.Customers.ListAsync()));
            break;
        case "7":
            await CallAsync("Subscriptions.List", async () => PrintList(await client.Subscriptions.ListAsync()));
            break;
        case "8":
            await CallAsync("Orders.List", async () => PrintList(await client.Orders.ListAsync()));
            break;
        case "9":
            await CallAsync("Webhooks.List", async () => PrintList(await client.Webhooks.ListAsync()));
            break;
        case "10":
            await CallAsync("Stores.Get(\"999999999\")", async () =>
            {
                await client.Stores.GetAsync("999999999");
                Console.WriteLine("  (no exception thrown — that store id unexpectedly resolved)");
            });
            break;
        case "0":
        case null:
            Console.WriteLine("Bye.");
            return;
        default:
            Console.WriteLine($"Unknown option '{choice}'.");
            break;
    }
}

// ---------------------------------------------------------------------------------------------
// Helpers
// ---------------------------------------------------------------------------------------------

static void PrintHeader()
{
    Console.WriteLine("=================================================");
    Console.WriteLine(" LemonSqueezySharp — manual test harness");
    Console.WriteLine("=================================================");
}

// Resolves the root facade and every sub-client, proving the DI graph is wired correctly.
// This needs no API key — it never makes a network call.
static void DiSanityCheck(ILemonSqueezyClient client)
{
    Console.WriteLine("Resolving every resource client off the root facade...");

    var clients = new (string Name, object Instance)[]
    {
        (nameof(client.Users), client.Users),
        (nameof(client.Stores), client.Stores),
        (nameof(client.Customers), client.Customers),
        (nameof(client.Products), client.Products),
        (nameof(client.Variants), client.Variants),
        (nameof(client.Prices), client.Prices),
        (nameof(client.Files), client.Files),
        (nameof(client.Orders), client.Orders),
        (nameof(client.OrderItems), client.OrderItems),
        (nameof(client.Subscriptions), client.Subscriptions),
        (nameof(client.SubscriptionInvoices), client.SubscriptionInvoices),
        (nameof(client.SubscriptionItems), client.SubscriptionItems),
        (nameof(client.UsageRecords), client.UsageRecords),
        (nameof(client.Discounts), client.Discounts),
        (nameof(client.DiscountRedemptions), client.DiscountRedemptions),
        (nameof(client.LicenseKeys), client.LicenseKeys),
        (nameof(client.LicenseKeyInstances), client.LicenseKeyInstances),
        (nameof(client.Checkouts), client.Checkouts),
        (nameof(client.Webhooks), client.Webhooks),
        (nameof(client.Affiliates), client.Affiliates),
        (nameof(client.Licenses), client.Licenses),
    };

    foreach (var (name, instance) in clients)
    {
        Console.WriteLine($"  [{(instance is not null ? "OK" : "NULL")}] {name,-22} -> {instance?.GetType().Name}");
    }

    Console.WriteLine($"Resolved {clients.Length} clients successfully.");
}

// End-to-end offline test of the webhook pipeline: build a payload, sign it with the configured
// secret, verify + parse into a typed event, then prove a tampered signature is rejected.
static async Task WebhookSelfTestAsync(IWebhookEventParser parser, LemonSqueezyOptions options)
{
    var secret = options.WebhookSigningSecret;
    if (string.IsNullOrWhiteSpace(secret))
    {
        Console.WriteLine("WebhookSigningSecret is not configured — set 'LemonSqueezy:WebhookSigningSecret' first.");
        return;
    }

    // A minimal but realistic order_created payload (meta.event_name drives the typed mapping).
    const string body = """
    {
      "meta": {
        "event_name": "order_created",
        "custom_data": { "user_id": "abc-123" }
      },
      "data": {
        "type": "orders",
        "id": "42",
        "attributes": { "identifier": "demo-order", "status": "paid", "total": 1999 }
      }
    }
    """;

    var signature = Sign(body, secret!);
    Console.WriteLine($"Computed X-Signature: {signature}");

    // 1) Good signature -> verifies and parses into the typed event.
    var evt = parser.VerifyAndParse(body, signature);
    Console.WriteLine($"VerifyAndParse -> {evt.GetType().Name} (event_name: {evt.EventName})");

    if (evt is OrderCreatedEvent order)
    {
        Console.WriteLine($"  typed Data.Id      : {order.Data?.Id}");
        Console.WriteLine($"  typed status       : {order.Data?.Attributes?.Status}");
        var custom = order.GetCustomData<Dictionary<string, string>>();
        Console.WriteLine($"  custom_data.user_id: {(custom is not null && custom.TryGetValue("user_id", out var u) ? u : "(none)")}");
    }

    // 2) Tampered signature -> the non-throwing variant returns false.
    var tampered = signature[..^1] + (signature[^1] == 'a' ? 'b' : 'a');
    var ok = parser.TryVerifyAndParse(body, tampered, out _);
    Console.WriteLine($"TryVerifyAndParse(tampered) -> {ok}  (expected: False)");

    // 3) Throwing variant on a bad signature surfaces the typed verification exception.
    try
    {
        parser.VerifyAndParse(body, tampered);
        Console.WriteLine("VerifyAndParse(tampered) -> did NOT throw (unexpected)");
    }
    catch (LemonSqueezyWebhookVerificationException)
    {
        Console.WriteLine("VerifyAndParse(tampered) -> threw LemonSqueezyWebhookVerificationException (expected)");
    }

    await Task.CompletedTask;
}

// Lowercase-hex HMAC-SHA256 — the exact format LemonSqueezy sends in X-Signature.
static string Sign(string body, string secret)
{
    var hash = HMACSHA256.HashData(Encoding.UTF8.GetBytes(secret), Encoding.UTF8.GetBytes(body));
    return Convert.ToHexStringLower(hash);
}

// Runs an API call, translating the SDK's typed exception hierarchy into readable output.
static async Task CallAsync(string label, Func<Task> action)
{
    Console.WriteLine($"Calling {label}...");
    try
    {
        await action();
    }
    catch (LemonSqueezyAuthenticationException ex)
    {
        Console.WriteLine($"  401 Authentication — is the API key set/valid? (request id: {ex.RequestId})");
    }
    catch (LemonSqueezyNotFoundException ex)
    {
        Console.WriteLine($"  404 Not Found (request id: {ex.RequestId})");
    }
    catch (LemonSqueezyValidationException ex)
    {
        Console.WriteLine($"  422 Validation: {string.Join("; ", ex.Errors)}");
    }
    catch (LemonSqueezyRateLimitException ex)
    {
        Console.WriteLine($"  429 Rate limited — retry after {ex.RetryAfter?.TotalSeconds ?? 0:0}s");
    }
    catch (LemonSqueezyApiException ex)
    {
        Console.WriteLine($"  API error {(int)ex.StatusCode} {ex.StatusCode}");
    }
    catch (LemonSqueezyException ex)
    {
        Console.WriteLine($"  SDK error: {ex.Message}");
    }
}

// Every resource derives from JsonApiResource<TAttributes> and so exposes string Id/Type;
// dynamic lets us print them without naming each concrete attribute type.
static void PrintList<T>(PagedBaseResult<T> page)
{
    Console.WriteLine($"  {page.Items.Count} item(s). HasNext={page.HasNext}");
    foreach (var item in page.Items.Take(10))
    {
        dynamic d = item!;
        Console.WriteLine($"    - {d.Type}/{d.Id}");
    }
}

static void PrintOne(string? id, string? json)
{
    Console.WriteLine($"  id: {id}");
    if (json is not null)
    {
        Console.WriteLine($"  attributes: {json}");
    }
}

static string Json(object value) =>
    JsonSerializer.Serialize(value, new JsonSerializerOptions { WriteIndented = false });
