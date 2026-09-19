---
name: add-unit-tests
description: Add unit tests for new handlers, validators, domain rules, stories, and infrastructure adapters. Use when implementing features or when no tests exist for new behavior.
---

# Add unit tests

create unit test for new functionalities, features, stories, infra

## Gate: no test project in the solution yet

Ask before adding `tests/*.csproj` or NuGet packages (architecture/package change). Propose:

- `tests/ForensicsAnalyzer.Application.Tests` targeting `net9.0`
- Packages: `xunit`, `xunit.runner.visualstudio`, `Microsoft.NET.Test.Sdk`, `FluentAssertions`, `NSubstitute`
- Solution folder `tests` mirroring `src`

After approval, create the project, add it to `ForensicsAnalyzer.sln`, then write tests. Do not put test packages in `src/` projects.

## What to test

| New thing | Test it via |
|-----------|-------------|
| Command/query handler | NSubstitute on ports; assert persistence calls and results |
| FluentValidation validator | `TestValidate` / direct `Validate` |
| Domain rule | entity unit tests |
| Infra adapter (mapping, token helper) | isolated unit test; no SQL unless user asked for integration tests |
| API story | handler tests first; WebApi integration only if asked |

## Conventions

- File: `tests/<Project>.Tests/<Feature>/<Sut>Tests.cs`
- Names: `Method_Scenario_Expected`
- One behavior per fact; no shared mutable fixture that hides arrange

Skip only if the user explicitly says not to test. Blazor `.razor` markup alone may skip; test extracted services.
