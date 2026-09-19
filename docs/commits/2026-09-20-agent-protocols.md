# agent-protocols

- Date: 2026-09-20
- Proposed message: `docs(agents): add guard protocols for commits, architecture, docs, and tests`

## Why

Agents had no project-level constraints. Work could be committed automatically, layers and packages could change without asking, and docs/tests were optional. These files encode the six standing orders so every agent session starts from the same rules.

## What changed

- Added `AGENTS.md` as the repo entry protocol.
- Added Cursor rules under `.cursor/rules/` (always-on protocols plus per-layer globs).
- Added project skills: draft-commit-message, document-change, add-unit-tests.
- Added `docs/agent-protocols.md`, `docs/commits/`, `docs/features/`.
- Pointed `PROJECT_DOCUMENTATION.md` at the new agent-protocol docs.

No application/runtime code changed.

## Architecture

None. No projects, packages, layers, domain, or persistence were modified. This is process/documentation only.

## Tests

None. No production behavior was added. Protocol 6 will apply once features land; the first test project still requires an explicit architecture question.

## Docs

- `docs/agent-protocols.md`
- `docs/features/agent-guard-protocols.md`
- `PROJECT_DOCUMENTATION.md` (Related Files + last updated)
