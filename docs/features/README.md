# Features

Product and technical feature docs. Create a page here when adding a **new feature, user story, layer, model, infrastructure piece, or technology** (protocol 5).

## Naming

`<kebab-case-name>.md`

## Template

```markdown
# Feature name

## Purpose
## Behavior
## Layers involved
## Endpoints / UI
## Permissions
## Tests
## Related
- Commit doc: `docs/commits/...`
- System map: `PROJECT_DOCUMENTATION.md`
```

Keep `PROJECT_DOCUMENTATION.md` as the index of the running system; pages here explain a single capability in depth.

## Pages

| Page | Topic |
|------|--------|
| [agent-guard-protocols.md](agent-guard-protocols.md) | Agent standing orders |
| [sql-server-docker.md](sql-server-docker.md) | Local SQL Server 2022 via Docker Compose |
