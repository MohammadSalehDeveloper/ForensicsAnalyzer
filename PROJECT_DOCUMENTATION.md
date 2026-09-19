# ForensicsAnalyzer — Project Documentation

> Last updated: September 20, 2026  
> Status: Active development (Phases 1–4 complete; significant backend work beyond original roadmap)

---

## Overview

**ForensicsAnalyzer** is a digital forensics dashboard built with **.NET**, **Blazor WebAssembly**, and **SQL Server**. It follows **Clean Architecture** and uses **CQRS + MediatR** for application logic, **ASP.NET Core Identity + JWT** for authentication, and a **permission-based authorization** system for fine-grained access control.

The application models forensic investigation workflows: cases, evidence sources, artifacts, call logs, contacts, locations, social media data, and related metadata.

---

## Solution Structure

| Project | Role |
|---------|------|
| `ForensicsAnalyzer.Domain` | Entities, enums, base types (`BaseEntity`, `ISoftDelete`) |
| `ForensicsAnalyzer.Application` | CQRS commands/queries, validators, authorization policies, interfaces |
| `ForensicsAnalyzer.Infrastructure` | EF Core, Identity, JWT, repositories, AutoMapper profiles, seeding |
| `ForensicsAnalyzer.Contracts` | Shared DTOs consumed by API and Blazor client |
| `ForensicsAnalyzer.WebApi` | REST API, thin controllers, Swagger, CORS |
| `ForensicsAnalyzer.WebClient` | Blazor WebAssembly SPA (auth UI, cases, admin pages) |

---

## Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                    ForensicsAnalyzer.WebClient              │
│              (Blazor WASM — Login, Cases, Admin)            │
└──────────────────────────┬──────────────────────────────────┘
                           │ HTTP + JWT
┌──────────────────────────▼──────────────────────────────────┐
│                    ForensicsAnalyzer.WebApi                   │
│         Controllers → MediatR → Commands / Queries          │
└──────────────────────────┬──────────────────────────────────┘
                           │
┌──────────────────────────▼──────────────────────────────────┐
│                 ForensicsAnalyzer.Application                 │
│   Handlers · FluentValidation · Permissions · IRepositories   │
└──────────────────────────┬──────────────────────────────────┘
                           │
┌──────────────────────────▼──────────────────────────────────┐
│                ForensicsAnalyzer.Infrastructure               │
│  ApplicationDbContext · Repositories · Identity · JWT · Maps  │
└──────────────────────────┬──────────────────────────────────┘
                           │
┌──────────────────────────▼──────────────────────────────────┐
│                   ForensicsAnalyzer.Domain                    │
│              Entities · Enums · Domain rules                  │
└─────────────────────────────────────────────────────────────┘
```

### Key patterns

- **CQRS + MediatR** — All business logic lives in command/query handlers, not controllers.
- **FluentValidation** — Validators registered via assembly scan; enforced through a MediatR `ValidationBehavior` pipeline.
- **Repository pattern** — One repository per aggregate; registered in Infrastructure DI.
- **AutoMapper** — Entity ↔ DTO mapping in Infrastructure profiles.
- **Soft delete** — Global query filters on entities implementing `ISoftDelete` (e.g. `Case`).
- **Thin controllers** — Controllers only dispatch MediatR requests and apply `[HasPermission]` attributes.

---

## Domain Model

### Core entities (15)

| Entity | Description |
|--------|-------------|
| `Case` | Investigation case linked to a source and owner; supports assignments |
| `CaseAssignment` | Links analysts to cases |
| `Source` | Forensic data source (path, device type, date range, method) |
| `Artifact` | Evidence container belonging to a case |
| `ArtifactItem` | Individual item within an artifact |
| `CallLog` | Phone call records |
| `Contact` | Contact list entries |
| `FileCustom` | Custom file metadata |
| `Location` | GPS / location records |
| `LocationImage` | Images associated with locations |
| `Thumbnail` | Thumbnails for file customs |
| `SocialMessenger` | Social app instance (e.g. WhatsApp, Telegram) |
| `SocialChat` | Chat thread within a messenger |
| `SocialMessage` | Individual messages |
| `SocialMember` | Participants in social chats |

### Enums

`DeviceType`, `SourceType`, `MethodType`, `ArtifactType` — used to classify sources and evidence.

---

## Authentication & Authorization

### Authentication

- **ASP.NET Core Identity** with `ApplicationUser` (extends `IdentityUser` with `FullName`).
- **JWT Bearer** tokens issued on login; roles and permission claims embedded in the token.
- **Google OAuth** optional — configured via `GoogleAuth:ClientId` / `ClientSecret` in `appsettings.json`.
- Endpoints: `POST /api/auth/register`, `POST /api/auth/login`, `GET /api/auth/google-login`.

### Authorization (permission-based)

Permissions are defined in `Permissions.cs` and grouped by resource:

| Group | Permissions |
|-------|-------------|
| Cases | Create, Read, Update, Delete, Assign |
| Users | Read, Update, Delete, ManageRoles |
| Sources | Create, Read, Update, Delete |
| Artifacts | Create, Read, Update, Delete |
| Social | Create, Read, Update, Delete |
| Admin | ImportDatabase, ManageRoles |

Controllers use `[HasPermission(Permissions.X.Y)]` which maps to ASP.NET Core authorization policies. A `PermissionAuthorizationHandler` checks the `permission` claim on the JWT.

### Seeded roles

| Role | Access |
|------|--------|
| **Administrator** | All permissions |
| **Analyst** | Cases (CRUD + assign), Sources (read), Artifacts (CRU), Social (CRU) |
| **Viewer** | Read-only on Cases, Sources, Artifacts, Social, Users |

On startup (Development), `IdentitySeeder` creates roles, attaches permission claims, and seeds an admin user from config:

```json
"SeedAdmin": {
  "Email": "admin@forensics.local",
  "Password": "Admin123!",
  "FullName": "System Administrator"
}
```

---

## API Endpoints

All controllers except `AuthController` require authentication. Individual actions require specific permissions.

| Controller | Base route | Operations |
|------------|------------|------------|
| `AuthController` | `/api/auth` | Register, Login, Google OAuth |
| `CasesController` | `/api/cases` | CRUD, assign/unassign, list assignments, "my cases" |
| `UsersController` | `/api/users` | User management |
| `SourcesController` | `/api/sources` | CRUD |
| `ArtifactsController` | `/api/artifacts` | CRUD |
| `ArtifactItemsController` | `/api/artifactitems` | Create, list by artifact |
| `CallLogsController` | `/api/calllogs` | Create, list, delete |
| `ContactsController` | `/api/contacts` | Create, list, delete |
| `FileCustomController` | `/api/filecustoms` | CRUD |
| `LocationsController` | `/api/locations` | Create, list, delete |
| `LocationImagesController` | `/api/locationimages` | CRUD |
| `ThumbnailsController` | `/api/thumbnails` | Create, list, delete |
| `SocialMessengersController` | `/api/socialmessengers` | CRUD |
| `SocialChatsController` | `/api/socialchats` | Create, list |
| `DatabaseImportController` | `/api/databaseimport` | Bulk import users and cases (Admin) |

Swagger UI is available in Development at `/swagger`.

---

## Application Layer (CQRS)

Handlers are organized by feature folder under `ForensicsAnalyzer.Application`:

- **Cases** — Create, Update, Delete, Assign, Unassign; queries for all cases, by ID, user cases, assignments
- **Sources** — Full CRUD
- **Artifacts / ArtifactItems** — Create and query
- **CallLogs, Contacts, Locations** — Create, query, delete
- **FileCustoms, Thumbnails, LocationImages** — Full or partial CRUD
- **Social** — Messengers, chats, messages, members
- **Users** — Update, delete
- **Database** — Bulk import command

Each write operation has a corresponding FluentValidation validator where input rules apply.

---

## Infrastructure

### Database

- **EF Core** with SQL Server
- **IdentityDbContext** extended as `ApplicationDbContext`
- Entity configurations applied via `ApplyConfigurationsFromAssembly`
- Initial migration: `20260712195217_InitialCreate`
- Auto-migrate on startup in Development

### Repositories

Registered repositories: `Case`, `CaseAssignment`, `Source`, `Artifact`, `ArtifactItem`, `CallLog`, `Contact`, `FileCustom`, `Location`, `Thumbnail`, `LocationImage`, `SocialMessenger`, `SocialChat`, `SocialMessage`, `SocialMember`, plus `UnitOfWork`.

### Services

- `IIdentityService` / `IdentityService` — User lookup and creation (used by import handler)
- `ICurrentUserService` / `CurrentUserService` — Resolves current user from HTTP context
- AutoMapper profiles for Cases, Sources, Contacts, and other entities

---

## Blazor WebClient

### Pages

| Route | Page | Status |
|-------|------|--------|
| `/` | Index | Landing |
| `/login` | Login | Username/password + Google link |
| `/register` | Register | New user registration |
| `/cases` | Cases | Case list and create (requires auth) |
| `/users` | Users | User management |
| `/admin` | Admin | Admin tools |
| `/redirect-from-auth` | RedirectFromAuth | OAuth callback helper |

### Auth components

- `AuthService` — Calls API login/register, stores JWT
- `LocalStorageAuthenticationStateProvider` — Persists token, exposes auth state
- `AuthLayout` — Layout for unauthenticated pages

### Current gap

`Program.cs` in WebClient is still the default Blazor WASM template — **auth services are not yet registered in DI**. Login/Cases pages exist but full end-to-end client auth wiring is incomplete (Phase 5 pending).

The WebApi `Program.cs` serves Blazor static files in Development as a convenience so SPA routes work alongside the API.

---

## DevOps & Configuration

### Docker Compose (SQL Server)

Local persistence is **SQL Server 2022 Linux** via `docker-compose.yml` (`mcr.microsoft.com/mssql/server:2022-latest`). PostgreSQL is not deployed yet.

| Piece | Role |
|-------|------|
| `sqlserver` | Engine on host port `SQL_PORT` (default **14330** → container 1433); volume `sqlserverdata` |
| `sqlserver-init` | After healthy, creates `DB_NAME` (`ForensicsAnalyzerDb`). Official image ignores `MSSQL_DB` |
| `.env` | From `.env.example` — `SA_PASSWORD`, `DB_NAME`, `SQL_PORT`, `MSSQL_PID` |

Healthcheck uses `/opt/mssql-tools18/bin/sqlcmd` with `-C -b -l 2`. Docker Desktop should allow ~2 GB RAM for the image.

See `docs/features/sql-server-docker.md`. Dual SQL Server + PostgreSQL providers would be an Infrastructure change (new NuGet, migration sets) and is not enabled.

### Configuration (`appsettings.json`)

| Section | Purpose |
|---------|---------|
| `ConnectionStrings:DefaultConnection` | SQL Server (local Docker defaults; override with env / user secrets) |
| `JwtSettings` | Key, Issuer, Audience, ExpirationMinutes |
| `GoogleAuth` | OAuth client credentials |
| `SeedAdmin` | Default admin user for Development |

### CORS

Configured for Blazor dev URLs: `https://localhost:7123`, `http://localhost:5123`.

---

## Roadmap Progress

| Phase | Description | Status |
|-------|-------------|--------|
| 1 | Solution skeleton, Clean Architecture projects | ✅ Done |
| 2 | DB + Identity + JWT + EF Core + SQL Server | ✅ Done |
| 3 | CQRS + MediatR + FluentValidation for Cases | ✅ Done (extended to all domains) |
| 4 | Google OAuth + JWT for Blazor client | ✅ Done (API side) |
| 5 | Blazor auth UI + secure routing + AuthStateProvider | ⬜ Partial (pages exist; DI not wired) |
| 6 | Middleware: exception handling, logging, correlation ID, audit | ⬜ Pending |
| 7 | Additional forensic features (notes, evidence, alerts) | ⬜ Partial (core entities implemented) |
| 8 | Role + policy + resource-based authorization | ✅ Mostly done (permission claims + roles) |
| 9 | Blazor dashboards, search, analysis tools | ⬜ Pending |
| 10 | Unit + integration tests | ⬜ Pending |
| 11 | Docker deployment, CI/CD | ⬜ Partial (local SQL Server 2022 compose + init DB) |

---

## Getting Started

### Prerequisites

- .NET 9 SDK
- Docker Desktop (~2 GB RAM for the SQL Server Linux image)
- Optional: Google OAuth credentials

### Run locally

1. **Start database** (PowerShell from repo root)
   ```powershell
   copy .env.example .env
   docker compose up -d
   docker compose ps
   ```
   Keep `.env` `SA_PASSWORD` in sync with `ConnectionStrings:DefaultConnection`. If you change the password or `SQL_PORT`, update the connection string (or set `ConnectionStrings__DefaultConnection`). A custom password should go in gitignored `src/ForensicsAnalyzer.WebApi/appsettings.Development.json` so it is not committed. Changing `.env` does not rotate `sa` on an existing Docker volume.

2. **Run API** (applies migrations and seeds roles/admin in Development)
   ```bash
   dotnet run --project src/ForensicsAnalyzer.WebApi
   ```

3. **Run Blazor client** (separate terminal)
   ```bash
   dotnet run --project src/ForensicsAnalyzer.WebClient
   ```

4. **Login** as `admin@forensics.local` / `Admin123!` and verify JWT includes role and permission claims.

5. **Explore API** at `https://localhost:<port>/swagger`.

---

## Git History (committed)

| Commit | Summary |
|--------|---------|
| `9633c10` | Initial DB context, models, auth controller, JWT, DTOs, DI, SQL migration |
| `88898a1` | Added `.gitignore` |
| `ff57801` | Initial commit |

> **Note:** A large portion of the current codebase (CQRS handlers, repositories, permission system, additional controllers, Blazor pages, Docker compose) exists as **local uncommitted changes** at the time of this document.

---

## Next Steps (recommended)

1. Wire Blazor `Program.cs` — register `AuthService`, `LocalStorageAuthenticationStateProvider`, authorized `HttpClient` with JWT bearer handler.
2. Add global exception middleware and structured logging (Phase 6).
3. Write integration tests for auth and case CRUD flows (Phase 10).
4. Commit the current feature work in logical chunks (auth, domain API, Blazor UI).
5. Expand Blazor pages for Sources, Artifacts, Social data, and search/dashboard views (Phase 9).
6. After an architecture review: optional PostgreSQL provider beside SQL Server (Infrastructure only; see `docs/features/sql-server-docker.md`).

---

## Agent protocols

Coding agents must follow `AGENTS.md` (also `.cursor/rules/` and `docs/agent-protocols.md`):

1. Propose commit messages only — do not run `git commit`.
2. Do not change architecture, layers, infrastructure, domain, persistence, or packages without a question and explanation.
3. Use Clean Architecture, Clean Code, design patterns, and ASP.NET technologies already in this solution.
4. Explain every proposed commit in `docs/commits/`.
5. Document new features, layers, models, infra, and technologies under `docs/features/` and this file when the system map changes.
6. Add unit tests for new functionalities, features, stories, and infra (the first test project is still an architecture question — Phase 10).

## Related Files

- `AGENTS.md` — Standing orders for coding agents
- `docs/agent-protocols.md` — Human-readable agent protocol
- `docs/commits/` — Per-commit explanations
- `docs/features/` — Feature and story docs
- `Roadmap.txt` — Original phase plan
- `README.md` — Short project description
- `.env.example` — Docker SQL Server environment template
- `docker-compose.yml` — Local SQL Server 2022 + catalog init
- `docs/features/sql-server-docker.md` — Local SQL Server Docker behavior and dual-provider notes
