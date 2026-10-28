# LemonSqueezySharp — GitHub issue drafts

Copy each **Title** into the issue title field, apply the **Labels**, and paste the **Body** into the description.

Suggested label names (create them on GitHub if they do not exist yet): `bug`, `enhancement`, `docs`, `ci`, `good first issue`.

**Already added on GitHub:** 1, 3, 4, 5, 6, 8, 10, 11, 13, 15, 16, 18

---

## Issue 1 — ✅ Added on GitHub

**Title:** Hydrate JSON:API `included` and read-side `relationships`

**Labels:** `enhancement`

**Body:**

```
## Summary
List endpoints accept `include=`, but related resources in the JSON:API `included` array (and read-side `relationships`) are not modeled or returned. `JsonApiResourceConverter` is currently an empty placeholder.

## Why
Callers must re-fetch related resources even after requesting them via `include`. This is a core JSON:API feature consumers expect from an SDK.

## Proposed work
- [ ] Add `Relationships` on read models / `JsonApiResource<T>`
- [ ] Capture `included` on `JsonApiResponse<T>` / `JsonApiCollectionResponse<T>`
- [ ] Implement or remove the stub in `Serialization/JsonApiResourceConverter.cs`
- [ ] Wire converters in `LemonSqueezyJsonOptions`
- [ ] Unit tests for responses with `include` + `included`

## Relevant files
- `Models/Common/JsonApiResource.cs`
- `Models/Common/JsonApiResponse.cs` / collection response
- `Serialization/JsonApiResourceConverter.cs`
- `Serialization/LemonSqueezyJsonOptions.cs`

## Acceptance criteria
- Deserializing a response with `included` preserves related resources
- Consumers can access relationships without a second round-trip when `include` was used
```

---

## Issue 2 — ✅ Added on GitHub

**Title:** Add `sort` support to `ListFilter`

**Labels:** `enhancement`, `good first issue`

**Body:**

```
## Summary
Lemon Squeezy list endpoints support a `sort` query parameter. `ListFilter` currently only exposes paging (`page[number]`, `page[size]`) and `include`.

## Why
Callers cannot sort list results without manually building query strings outside the SDK.

## Proposed work
- [ ] Add `Sort` (string?) to `ListFilter`
- [ ] Map it to the `sort` query param in `ApiClientBase` (or equivalent)
- [ ] Document common sort fields in XML docs / README
- [ ] Unit test that the query string includes `sort`

## Relevant files
- `Models/Requests/ListFilter.cs`
- `Clients/Base/ApiClientBase.cs`
```

---

## Issue 3 — ✅ Added on GitHub

**Title:** Support `include` on single-resource `GetAsync` methods

**Labels:** `enhancement`

**Body:**

```
## Summary
List filters accept `Include`, but `GetAsync` methods on resource clients do not. Consumers cannot do e.g. `GET /subscriptions/{id}?include=customer,product` through the typed API.

## Why
Side-loading related data on a single get is a common JSON:API pattern and avoids extra requests.

## Proposed work
- [ ] Add an optional `include` parameter (or options object) to `GetAsync` on all relevant clients
- [ ] Update interfaces under `Interfaces/Clients/`
- [ ] Update implementations under `Clients/`
- [ ] Tests per client (or shared base tests)

## Example API shape
```csharp
Task<JsonApiResponse<Subscription>?> GetAsync(
    string id,
    string? include = null,
    CancellationToken cancellationToken = default);
```

## Acceptance criteria
- Get requests can append `?include=...`
- Existing call sites remain source-compatible (optional param / overload)
```

---

## Issue 4 — ✅ Added on GitHub

**Title:** Sync models with newer Lemon Squeezy API fields (affiliates, payment processor, etc.)

**Labels:** `bug`, `enhancement`

**Body:**

```
## Summary
Some fields present in the current Lemon Squeezy API / changelog appear missing from attribute models (e.g. affiliate attribution on orders/invoices, `payment_processor` on subscriptions). Missing properties cause silent data loss on deserialize.

## Why
Apps relying on affiliate attribution or payment-processor metadata cannot use the SDK correctly without gaps.

## Proposed work
- [ ] Diff attribute models against current Lemon Squeezy docs / sample payloads
- [ ] Add missing fields (candidates: `affiliate_id`, `referral_amount` on Order + SubscriptionInvoice; `payment_processor` on Subscription)
- [ ] Update XML docs
- [ ] Add/adjust unit tests with sample JSON fixtures

## Relevant files
- `Models/Orders/OrderAttributes.cs`
- `Models/SubscriptionInvoices/SubscriptionInvoiceAttributes.cs`
- `Models/Subscriptions/SubscriptionAttributes.cs`
- Related customer / affiliate models if needed

## Notes
Verify exact field names against the latest official docs before merging.
```

---

## Issue 5 — ✅ Added on GitHub

**Title:** Complete `CheckoutOptions` (color overrides + `skip_trial`)

**Labels:** `enhancement`

**Body:**

```
## Summary
`CheckoutOptions` currently exposes limited styling (`button_color` and deprecated `dark`). Lemon Squeezy has added additional checkout color overrides and `skip_trial`.

## Why
Custom checkout branding and trial-skip behavior cannot be configured fully via the SDK.

## Proposed work
- [ ] Align `CheckoutOptions` with current Checkout API docs
- [ ] Add missing color properties and `skip_trial`
- [ ] Mark `dark` as obsolete if still present for compatibility
- [ ] Update create-checkout samples / README if needed
- [ ] Serialization tests

## Relevant files
- `Models/Checkouts/CheckoutAttributes.cs` (`CheckoutOptions`)
- `Models/Checkouts/CreateCheckoutRequest.cs`
```

---

## Issue 6 — ✅ Added on GitHub

**Title:** Auto-pagination helpers (`IAsyncEnumerable`) for list endpoints

**Labels:** `enhancement`

**Body:**

```
## Summary
Paged list responses expose links, but consumers must manually loop with `page[number]` / `links.next`. There is no first-class helper to enumerate all pages.

## Why
“List all X” is a common need; manual pagination is boilerplate and easy to get wrong with rate limits.

## Proposed work
- [ ] Add helpers that follow `links.next` (or page until empty)
- [ ] Prefer `IAsyncEnumerable<T>` for streaming pages/items
- [ ] Document rate-limit behavior / interaction with resilience
- [ ] Tests with multi-page stub responses

## Relevant files
- `Models/Common/PagedBaseResult.cs` (or equivalent)
- `Clients/Base/ApiClientBase.cs`
- Possible extension methods on list clients

## Acceptance criteria
- Consumer can iterate all items without manually managing page numbers
- Cancellation is honored between pages
```

---

## Issue 7

**Title:** Audit webhook event catalogue against official Lemon Squeezy docs

**Labels:** `docs`, `enhancement`

**Body:**

```
## Summary
The SDK includes strongly-typed webhook events that may not match the current official event-types list (e.g. dispute-related events, `subscription_plan_changed`). These should be verified, documented, or marked obsolete.

## Why
Misleading event types create wrong expectations (e.g. chargeback handling) and dead public API surface.

## Proposed work
- [ ] Compare `WebhookEventNames` / typed events to current LS docs
- [ ] Remove, obsolete, or clearly document undocumented events
- [ ] Update README webhook section
- [ ] Adjust parser tests accordingly

## Relevant files
- `Models/Webhooks/Events/WebhookEventNames.cs`
- `Models/Webhooks/Events/WebhookEvents.cs`
- `Services/WebhookEventParser.cs`
- `Models/Disputes/*` (if present)
- Related tests
```

---

## Issue 8 — ✅ Added on GitHub

**Title:** Add ASP.NET Core helpers for webhook verification endpoints

**Labels:** `enhancement`, `docs`

**Body:**

```
## Summary
README shows manual `StreamReader` + header plumbing for webhooks. Most .NET consumers are on ASP.NET Core and need reliable access to the raw body for HMAC verification.

## Why
Signature verification fails easily if the body is buffered/rewound incorrectly. Helpers reduce integration bugs.

## Proposed work
- [ ] Add extension helpers to read raw body + `X-Signature` (and optional `X-Event-Name`)
- [ ] Integrate cleanly with `IWebhookEventParser` / `IWebhookVerifier`
- [ ] Document usage in README (minimal controller sample)
- [ ] Consider optional package vs core package dependency on ASP.NET abstractions

## Relevant files
- New helpers under `Extensions/` (or companion package)
- `Services/WebhookEventParser.cs`
- `README.md`
```

---

## Issue 9 — ✅ Fixed in README

**Title:** Fix incorrect namespace in README quick-start sample

**Labels:** `bug`, `docs`, `good first issue`

**Body:**

```
## Summary
The “Use it” quick-start sample imports `LemonSqueezySharp.Clients.Interfaces`, but the real namespace is `LemonSqueezySharp.Interfaces.Clients`.

## Why
Copy-paste from the README fails to compile for new users.

## Proposed work
- [ ] Fix the `using` in README
- [ ] Grep README for any other stale namespaces/type names
- [ ] Optionally add a smoke sample that matches the docs

## Relevant files
- `README.md`
```

---

## Issue 10 — ✅ Added on GitHub

**Title:** Add `examples/` sample projects (console + ASP.NET webhook)

**Labels:** `docs`, `enhancement`

**Body:**

```
## Summary
There is no examples project demonstrating DI registration, common API calls, resilience, and webhook handling.

## Why
Samples are the fastest onboarding path and catch wiring mistakes the unit tests miss.

## Proposed work
- [ ] Add `examples/ConsoleQuickStart` (list stores / get me)
- [ ] Add `examples/AspNetWebhook` (verify + pattern-match events)
- [ ] Link from README
- [ ] Keep examples out of the NuGet package / pack excludes

## Notes
Do not commit real API keys; use env vars / user secrets.
```

---

## Issue 11 — ✅ Added on GitHub

**Title:** Add CHANGELOG.md for pre-1.0 releases

**Labels:** `docs`, `good first issue`

**Body:**

```
## Summary
The package is pre-1.0 (`0.1.0`) and the public API may still change, but there is no changelog for consumers.

## Why
Users need a clear place to see breaking changes, new resources, and migration notes between versions.

## Proposed work
- [ ] Add `CHANGELOG.md` (Keep a Changelog style)
- [ ] Document 0.1.0 baseline
- [ ] Mention changelog updates in CONTRIBUTING / release checklist
- [ ] Optionally automate release notes from GitHub Releases later
```

---

## Issue 12

**Title:** Add dedicated PR CI with Linux runners and coverage

**Labels:** `ci`, `enhancement`

**Body:**

```
## Summary
CI currently lives in `publish.yml` on `windows-latest`. There is no lightweight PR-focused workflow with coverage, and Linux is not exercised despite multi-targeting.

## Why
Faster PR signal, earlier platform bugs, and Coverlet is already available but unused as a gate. CONTRIBUTING says “CI must pass.”

## Proposed work
- [ ] Split or add a `ci.yml` for PRs (build + test)
- [ ] Run on `ubuntu-latest` (and keep Windows if needed for net48/net472)
- [ ] Collect coverage; optionally fail under a threshold later
- [ ] Update README badge if the workflow name/path changes

## Relevant files
- `.github/workflows/*`
- `tests/LemonSqueezySharp.Tests/LemonSqueezySharp.Tests.csproj`
```

---

## Issue 13 — ✅ Added on GitHub

**Title:** Publish symbol packages (`.snupkg`) to NuGet.org

**Labels:** `bug`, `ci`

**Body:**

```
## Summary
The project packs with `IncludeSymbols` / `SymbolPackageFormat=snupkg`, and artifacts may include `.snupkg`, but the publish step appears to push only `*.nupkg`.

## Why
Without symbols on NuGet.org, consumers lose a good debugger / SourceLink experience.

## Proposed work
- [ ] Confirm whether `.snupkg` is uploaded today
- [ ] Update publish workflow to push symbol packages
- [ ] Verify on a dry-run / test feed if possible

## Relevant files
- `.github/workflows/publish.yml`
- `LemonSqueezySharp.csproj`
```

---

## Issue 14

**Title:** Add SourceLink and package validation toward 1.0

**Labels:** `enhancement`, `ci`

**Body:**

```
## Summary
`PublishRepositoryUrl` / `EmbedUntrackedSources` are set, but `Microsoft.SourceLink.GitHub` is not referenced. There is also no package/public API validation baseline for a stable 1.0.

## Why
Reliable “step into package source” for consumers, and intentional API evolution before locking 1.0.

## Proposed work
- [ ] Add `Microsoft.SourceLink.GitHub`
- [ ] Enable package validation / PublicApi analyzers (or equivalent) when ready
- [ ] Document versioning policy in README or CONTRIBUTING

## Relevant files
- `LemonSqueezySharp.csproj`
- `Directory.Build.props`
```

---

## Issue 15 — ✅ Added on GitHub

**Title:** Enforce XML documentation on public API (stop suppressing CS1591)

**Labels:** `docs`, `enhancement`

**Body:**

```
## Summary
`GenerateDocumentationFile` is enabled, but `CS1591` is globally suppressed. Missing docs on public surface area do not fail the build.

## Why
NuGet consumers rely on IntelliSense/XML docs. CONTRIBUTING asks for docs, but the build does not enforce them.

## Proposed work
- [ ] Remove `CS1591` from `NoWarn` (or scope suppression to tests only)
- [ ] Fill missing docs on public interfaces/models
- [ ] Keep warnings-as-errors green

## Relevant files
- `Directory.Build.props`
- `LemonSqueezySharp.csproj`
- Public types under `Interfaces/`, `Models/`, `Extensions/`, etc.
```

---

## Issue 16 — ✅ Added on GitHub

**Title:** Add opt-in live integration test suite

**Labels:** `enhancement`, `ci`

**Body:**

```
## Summary
All client tests use stubbed HTTP handlers. There are no gated live tests against the real Lemon Squeezy API (test mode).

## Why
Stub tests cannot catch schema drift (missing attributes, renamed fields, new required relationships).

## Proposed work
- [ ] Add an Integration test project or category
- [ ] Gate on `LEMONSQUEEZY_API_KEY` (skip when unset)
- [ ] Document how to run locally in CONTRIBUTING
- [ ] Optional CI job with secrets (manual/nightly)

## Notes
Use a dedicated test-mode store; never commit secrets.
```

---

## Issue 17

**Title:** Expand systematic error-handling tests for `ApiClientBase`

**Labels:** `enhancement`, `good first issue`

**Body:**

```
## Summary
Error mapping (401/404/422/429) is covered unevenly across client tests. Shared `ApiClientBase` plumbing deserves focused tests for malformed error bodies, missing `Retry-After`, and generic API exceptions.

## Why
Regressions in the base client affect every resource.

## Proposed work
- [ ] Add `ApiClientBaseTests` (or equivalent) with stubbed responses
- [ ] Cover 401, 404, 422, 429, 5xx, invalid JSON:API errors
- [ ] Assert exception types and important properties

## Relevant files
- `Clients/Base/ApiClientBase.cs`
- `Exceptions/*`
- New tests under `tests/LemonSqueezySharp.Tests/`
```

---

## Issue 18 — ✅ Added on GitHub

**Title:** Surface Lemon Squeezy rate-limit response headers

**Labels:** `enhancement`

**Body:**

```
## Summary
The API returns rate-limit headers (e.g. `X-Ratelimit-Limit`, `X-Ratelimit-Remaining`). The SDK maps `429` to `LemonSqueezyRateLimitException` but does not expose header values on successful responses.

## Why
Apps can throttle proactively instead of only reacting after a 429.

## Proposed work
- [ ] Capture rate-limit headers on responses or via a small context type
- [ ] Document how to read them
- [ ] Tests with stubbed headers
- [ ] Consider exposing `Retry-After` clearly on the 429 exception (if not already)

## Relevant files
- `Clients/Base/ApiClientBase.cs`
- Exception / result types
- Tests
```

---

## Issue 19

**Title:** Add nested relationship URL helpers (e.g. `/products/{id}/variants`)

**Labels:** `enhancement`

**Body:**

```
## Summary
Lemon Squeezy documents nested relationship URLs such as `GET /v1/products/{id}/variants`. The SDK has no first-class helpers for these paths.

## Why
Improves discoverability versus composing paths or relying only on top-level list filters.

## Proposed work
- [ ] Identify documented nested routes worth supporting
- [ ] Add helpers on the parent clients (or relationship client methods)
- [ ] Update `ApiEndpoints` as needed
- [ ] Unit tests

## Relevant files
- `Http/ApiEndpoints.cs`
- `Clients/ProductsClient.cs`, `Clients/StoresClient.cs`, etc.
```

---

## Issue 20

**Title:** Add subscription convenience methods (`PauseAsync` / `UnpauseAsync` / `ResumeAsync`)

**Labels:** `enhancement`, `good first issue`

**Body:**

```
## Summary
Subscription pause/resume/cancel flows today go through generic `UpdateAsync` / `CancelAsync`. Convenience methods would encode the common attribute payloads.

## Why
Reduces boilerplate and incorrect payload shapes for frequent subscription lifecycle operations.

## Proposed work
- [ ] Add `PauseAsync`, `UnpauseAsync` / `ResumeAsync` (names per LS docs) wrapping `UpdateAsync`
- [ ] XML docs with links to official docs
- [ ] Unit tests asserting request body

## Relevant files
- `Interfaces/Clients/ISubscriptionsClient.cs`
- `Clients/SubscriptionsClient.cs`
- Subscription update request models
```

---

## Issue 21

**Title:** Modernize GitHub issue templates to issue forms

**Labels:** `docs`, `good first issue`

**Body:**

```
## Summary
The repo has legacy issue/PR markdown templates. GitHub issue forms (YAML) give structured fields for bugs vs features and improve triage.

## Why
Clearer reports for a growing SDK (repro steps, LS API version, target framework, package version).

## Proposed work
- [ ] Add `.github/ISSUE_TEMPLATE/bug_report.yml` and `feature_request.yml`
- [ ] Add `config.yml` (disable blank issues or link Discussions)
- [ ] Keep PR template; retire or replace legacy `issue_template.md`
```

