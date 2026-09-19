# Agent protocols (ForensicsAnalyzer)

Cursor and other coding agents must follow these protocols. Machine-readable copies live in `AGENTS.md` and `.cursor/rules/`.

## Standing orders

1. **don't commit changes directly and only gimme commit message to me**  
   Agents never run `git commit` / `git push`. They only propose a commit message.

2. **don't change architecture, layer, infrastructure, domain, persistence, packages, without question and explanation**  
   Stop, explain, and wait for approval before structural change.

3. **keep in mind we're using clean architecture and Clean Code, Design Patterns, ASP.NET Technologies for development**  
   .NET 9, ASP.NET Core, Blazor WASM, EF Core, Identity + JWT, CQRS/MediatR, FluentValidation, AutoMapper.

4. **always explain for each commit in docs**  
   Every proposed commit has `docs/commits/YYYY-MM-DD-<slug>.md`.

5. **create docs for new features, layer, model, infra, technology,etc.**  
   Feature pages under `docs/features/`; system map in `PROJECT_DOCUMENTATION.md`.

6. **create unit test for new functionalities, features, stories, infra**  
   Unit tests are part of the work. The first test project requires an architecture question (none exists yet).

## Layout for agents

| Path | Role |
|------|------|
| `AGENTS.md` | Entry protocol for all agents |
| `.cursor/rules/*.mdc` | Always-on and layer-scoped Cursor rules |
| `.cursor/skills/draft-commit-message/` | Message-only git workflow |
| `.cursor/skills/document-change/` | Docs workflow |
| `.cursor/skills/add-unit-tests/` | Test workflow |
| `docs/commits/` | Per-commit explanations |
| `docs/features/` | Feature / story docs |

## Architecture reminder

Do not add layers. Existing projects: Domain, Application, Infrastructure, Contracts, WebApi, WebClient. Details: `PROJECT_DOCUMENTATION.md`.
