# Commit explanations

Agents must add one file here **for every proposed commit** (protocol 4). The user applies the git commit locally.

## Naming

`YYYY-MM-DD-<slug>.md` — UTC date of the work, kebab-case slug matching the commit subject.

## Template

```markdown
# <slug>

- Date: YYYY-MM-DD
- Proposed message: `<type>(<scope>): <summary>`

## Why

## What changed

## Architecture

None.

## Tests

## Docs
```

Do not use this folder for feature specs (`docs/features/`) or the system map (`PROJECT_DOCUMENTATION.md`).
