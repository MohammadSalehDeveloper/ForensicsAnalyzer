# Agent guard protocols

## Purpose

Give every coding agent the same non-negotiable workflow for ForensicsAnalyzer: no auto-commit, no silent architecture changes, Clean Architecture / ASP.NET conventions, docs for every commit and new capability, and unit tests for new behavior.

## Behavior

Agents must:

1. Propose a commit message only; never run `git commit` unless the user overrides the protocol in that message.
2. Ask and explain before changing architecture, layers, infrastructure, domain, persistence, or packages.
3. Follow Clean Architecture, Clean Code, established design patterns, and ASP.NET technologies already in the solution.
4. Write `docs/commits/YYYY-MM-DD-<slug>.md` for each proposed commit.
5. Document new features, layers, models, infra, and technologies under `docs/` and update `PROJECT_DOCUMENTATION.md` when the system map changes.
6. Add unit tests for new functionalities, features, stories, and infra (ask first to add the initial test project).

## Layers involved

None of the `src/` projects. Files live at repo root (`AGENTS.md`), `.cursor/`, and `docs/`.

## Endpoints / UI

None.

## Permissions

None.

## Tests

Not applicable (process files only). Future product work must add tests per protocol 6.

## Related

- Commit doc: `docs/commits/2026-09-20-agent-protocols.md`
- System map: `PROJECT_DOCUMENTATION.md`
- Entry file: `AGENTS.md`
