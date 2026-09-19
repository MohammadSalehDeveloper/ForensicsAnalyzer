---
name: document-change
description: Write project docs for commits, features, layers, models, infrastructure, and technologies. Use when finishing work, adding a feature, or changing architecture after approval.
---

# Document the change

always explain for each commit in docs

create docs for new features, layer, model, infra, technology,etc.

## Required artifacts

1. **Commit explanation** — always: `docs/commits/YYYY-MM-DD-<slug>.md`
2. **Feature/story** — if user-visible or a new use case: `docs/features/<kebab-name>.md`
3. **System map** — if layers, entities, endpoints, infra, or tech changed: update `PROJECT_DOCUMENTATION.md` (bump “Last updated”)

Keep `README.md` as the short blurb; do not dump architecture there.

## Commit doc template

```markdown
# <slug>

- Date: YYYY-MM-DD
- Proposed message: `<type>(<scope>): ...`

## Why
## What changed
## Architecture
None | Approved change: <summary of the question and answer>
## Tests
## Docs
```

## Feature doc template

```markdown
# <Feature name>

## Purpose
## Behavior
## Layers involved
## Endpoints / UI
## Permissions
## Tests
```

Do not finish implementation without these files. Chat is not a substitute.
