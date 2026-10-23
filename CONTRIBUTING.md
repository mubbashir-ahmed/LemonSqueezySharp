# Contributing to LemonSqueezySharp

Thanks for helping improve this unofficial .NET SDK for the LemonSqueezy API.

---

## Development setup

- Install the [.NET 10 SDK](https://dotnet.microsoft.com/download).
- Clone your fork and run:

  ```bash
  dotnet restore
  dotnet build -c Release
  dotnet test -c Release
  ```

- The test project also targets .NET Framework (net472), which only runs on Windows.
  On Linux/macOS, run only the modern target:

  ```bash
  dotnet test -c Release -f net10.0
  ```

---

## Guidelines

- Keep changes focused: one fix or feature per pull request.
- Warnings are treated as errors, so the build must be warning-free.
- Add XML documentation to public types and members.
- Add or update tests for behavior changes. Never put real API keys in tests; use mocked HTTP responses.
- Follow the existing code style (`.editorconfig` is enforced during build).
- Avoid breaking public API changes. If one is needed, explain why in the PR.
- Update the README or docs if you change behavior.

---

## Reporting bugs

Open an issue with:
- SDK version and target framework (.NET 10, .NET Framework, etc.)
- A minimal code sample that reproduces the problem
- Expected vs actual behavior and any exception details

**Never include API keys or webhook signing secrets in issues.** Redact them.

---

## Security issues

Do not open a public issue for security problems. Use GitHub's
[private vulnerability reporting](https://github.com/mubbashir-ahmed/LemonSqueezySharp/security/advisories/new)
instead.

---

## Pull request process

1. CI must pass on your PR.
2. A maintainer will review and may request changes.
3. PRs are merged by the maintainer. Releases are published by the maintainer only.
