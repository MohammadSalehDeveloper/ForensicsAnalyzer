# SQL Server (Docker) — local persistence

## Purpose

Run **SQL Server 2022** in Docker as the first, portable local database for ForensicsAnalyzer. Developers do not need a Windows SQL Server install. EF Core, Identity, and existing SQL Server migrations stay as they are.

PostgreSQL (or dual-provider persistence) is **not** implemented in this step.

## Behavior

1. Copy `.env.example` → `.env` and keep the SA password in sync with `ConnectionStrings:DefaultConnection`.
2. `docker compose up -d` starts `forensics_sqlserver` on host port `SQL_PORT` (default **14330**). Inside the container the engine still listens on 1433.
3. A healthcheck uses `/opt/mssql-tools18/bin/sqlcmd` with `-C -b -l 2` (tools18 + encrypt-by-default).
4. After the engine is healthy, `sqlserver-init` creates `DB_NAME` (`ForensicsAnalyzerDb`) if it does not exist. The official image does **not** honor `MSSQL_DB` and has no Postgres-style init folder.
5. Data is stored in the `sqlserverdata` volume.
6. In Development, WebApi still runs `Database.MigrateAsync()` and Identity seeding on startup.

### Connection

Default (matches `.env.example`):

```
Server=localhost,14330;Database=ForensicsAnalyzerDb;User Id=sa;Password=YourStrongPassword123!;Encrypt=True;TrustServerCertificate=True;MultipleActiveResultSets=True
```

Override in production or personal machines via environment (`ConnectionStrings__DefaultConnection`) or user secrets — do not rely on the committed local password outside Docker Desktop.

Host **14330** is used so a local SQL Server on 1433 is left alone. Change `SQL_PORT` in `.env` if 14330 is taken, and keep the connection string host port in sync.

Docker Desktop should have about **2 GB** of RAM available for this image.

If `.env` already existed, Compose uses **that** `SA_PASSWORD`. Changing the password in `.env` / `.env.example` / `appsettings.json` does **not** rotate `sa` on an existing `sqlserverdata` volume; SQL Server keeps the password stored in the data files. `MSSQL_SA_PASSWORD` is applied only on first initialization of the volume. To change it later, `ALTER LOGIN sa WITH PASSWORD = '...'` against the current password, or start from a blank engine with `docker compose down -v` (deletes local database data).

`appsettings.json` matches `.env.example` for a fresh clone. On a machine with a custom `.env` password, use gitignored `appsettings.Development.json` (or `ConnectionStrings__DefaultConnection` / user secrets) so the API password matches the volume.

## Layers involved

| Layer | Change |
|-------|--------|
| Domain | None |
| Application | None |
| Infrastructure | None (still `UseSqlServer`) |
| WebApi | Local connection string aligned with Docker defaults |
| WebClient | None |
| DevOps | `docker-compose.yml`, `.env.example` |

## Endpoints / UI

None. This is local infrastructure only.

## Permissions

None.

## Tests

No new unit tests. Compose and the SQL image are operational infrastructure, not application behavior. Handler/repository tests are unchanged. A test project still does not exist (Phase 10) — not added here.

## Future: SQL Server + PostgreSQL (not in this change)

Goal: persistence that can target **either** SQL Server or PostgreSQL from configuration, without changing Domain or Application.

**What would change (needs explicit approval):**

- **Layer:** Infrastructure DI and EF migrations only.
- **Packages:** `Npgsql.EntityFrameworkCore.PostgreSQL` (new NuGet — architecture gate).
- **Shape:** `Database:Provider` (`SqlServer` \| `PostgreSQL`) selecting `UseSqlServer` vs `UseNpgsql` on the existing `ApplicationDbContext`. Two migration sets (or two assemblies), same entities.
- **What stays the same:** Domain entities, repository interfaces, MediatR handlers, Contracts, permission policies, Blazor client.
- **What we will not do in that step:** a second DbContext for every aggregate, or leaking EF provider types into Application.

Until that is approved, only the SQL Server Docker service is supported.

## Related

- Commit doc: `docs/commits/2026-09-20-sql-server-docker.md`
- System map: `PROJECT_DOCUMENTATION.md`
- Compose: `docker-compose.yml`
- Env template: `.env.example`
