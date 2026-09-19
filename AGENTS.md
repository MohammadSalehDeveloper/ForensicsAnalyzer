# Agent protocols — ForensicsAnalyzer

Read this file before writing code, docs, or tests. Project protocols override generic “commit for me” habits.

## Standing orders (verbatim)

1. don't commit changes directly and only gimme commit message to me
2. don't change architecture, layer, infrastructure, domain, persistence, packages, without question and explanation
3. keep in mind we're using clean architecture and Clean Code, Design Patterns, ASP.NET Technologies for development
4. always explain for each commit in docs
5. create docs for new features, layer, model, infra, technology,etc.
6. create unit test for new functionalities, features, stories, infra

## How to apply them

| # | Do | Do not |
|---|----|--------|
| 1 | After work, output a proposed commit message. Leave staging/commit to the user. | Run `git commit`, `git push`, `--amend`, or create a PR unless the user overrides this protocol in the same message. |
| 2 | Stop. Ask. Explain *why*, *what layer*, and *what stays the same*. Wait for approval. | Add/remove projects, NuGet packages, DI composition, EF mappings, domain entities, or persistence shape as a silent side effect. |
| 3 | Follow Clean Architecture, Clean Code, GoF/enterprise patterns already in the repo, and ASP.NET Core / Blazor conventions. | Put business logic in controllers, leak EF into Domain, or invent a parallel architecture. |
| 4 | Add `docs/commits/YYYY-MM-DD-<slug>.md` for the proposed commit and mention it with the message. | Finish a task with only a chat summary and no commit doc. |
| 5 | Document new features, layers, models, infra, and technologies under `docs/` and update `PROJECT_DOCUMENTATION.md` when the system map changes. | Ship undocumented public behavior, entities, or infrastructure. |
| 6 | Add unit tests for new behavior (handlers, validators, domain rules, infra adapters, stories). | Treat “tests pending” as optional. First test project is an architecture change — ask once, then always test. |

## Solution map (do not invent new layers)

```
WebClient (Blazor WASM) → WebApi (thin controllers + MediatR)
    → Application (CQRS, FluentValidation, permissions, repository ports)
        → Domain (entities, enums, base types)
    ← Infrastructure (EF Core, Identity, JWT, repositories, AutoMapper)
Contracts = shared DTOs (API + client)
```

Allowed references: Domain has none. Application → Domain + Contracts. Infrastructure → Application + Domain. WebApi → Application + Infrastructure + Contracts. WebClient → Contracts (HTTP), never Domain/Infrastructure.

## After every task

1. Docs for the change (`docs/features/` and/or `PROJECT_DOCUMENTATION.md`).
2. Unit tests for new behavior (or a clear question if a test project must be added).
3. Commit explanation in `docs/commits/`.
4. Proposed commit message only — no `git commit`.

Detailed rules: `.cursor/rules/`. Workflows: `.cursor/skills/`. Human copy: `docs/agent-protocols.md`.
