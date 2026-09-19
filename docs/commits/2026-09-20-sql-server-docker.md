# sql-server-docker

- Date: 2026-09-20
- Proposed message: `chore(infra): run SQL Server 2022 in Docker for local persistence`

## Why

Local development needed a portable SQL Server instead of a machine-installed instance. The existing compose file advertised `MSSQL_DB` (ignored by the official image) and a fragile healthcheck. Align the stack with Microsoft’s SQL Server 2022 Linux container so `docker compose up` plus the committed connection string is enough to reach `ForensicsAnalyzerDb`.

## What changed

- **DevOps:** `docker-compose.yml` — SQL Server 2022 image, healthcheck via `mssql-tools18` (`-C -b -l 2`), named network, host port **14330** (`SQL_PORT`, container still 1433) so a machine SQL instance on 1433 is not used, one-shot init container to `CREATE DATABASE` after the engine is healthy. Removed unused Compose `version` and invalid `MSSQL_DB`.
- **DevOps:** `.env.example` — `SA_PASSWORD`, `DB_NAME`, `SQL_PORT`, `MSSQL_PID`.
- **WebApi:** `appsettings.json` connection string uses `localhost,14330` and Docker defaults (`Encrypt` + `TrustServerCertificate` + MARS). A custom local SA password belongs in gitignored `appsettings.Development.json` or user secrets, not the committed file.
- **Docs:** feature page, system map DevOps section, this commit explanation.

No Domain, Application, Contracts, or Infrastructure provider changes. PostgreSQL is documented as a future architecture question only.

## Architecture

Approved for this step: local Docker SQL Server only. Persistence remains EF Core SQL Server (`UseSqlServer`, existing migrations).

Not in this commit (needs a later architecture question): dual SQL Server + PostgreSQL provider switch, Npgsql package, split migrations.

## Tests

None. No application behavior or adapter logic was added. Phase 10 test project was not created.

## Docs

- `docs/features/sql-server-docker.md`
- `docs/commits/2026-09-20-sql-server-docker.md`
- `PROJECT_DOCUMENTATION.md`
