# Phase 0 Research: Encounter Forge MVP

## Decision 1: Use ASP.NET Core MVC with Razor Views for SSR

- Decision: Build a single ASP.NET Core MVC app where controllers return Razor views and handle form posts.
- Rationale: This directly matches the requirement for server-rendered pages and reinforces core MVC architecture (routing, controller actions, model binding, views).
- Alternatives considered:
  - Razor Pages: Simpler page model, but less explicit for teaching classic MVC controller/view separation.
  - SPA frontend + API backend: More complex than MVP scope and conflicts with SSR-first goal.

## Decision 2: Use EF Core Code-First with SQLite and migrations

- Decision: Define `Encounter` and `MonsterEntry` entities in C#, configure `AppDbContext`, and manage schema with EF migrations.
- Rationale: Code-first is pedagogically strong for learning entity mapping, relationships, and schema evolution while keeping persistence setup small.
- Alternatives considered:
  - Raw SQL with ADO.NET: More boilerplate and less aligned with EF learning objectives.
  - Database-first scaffolding: Less instructional for modeling and migration concepts.

## Decision 3: Validation strategy combines DataAnnotations + custom checks

- Decision: Use DataAnnotations for required/range checks and custom validation for CR format and minimum monster rows.
- Rationale: This teaches built-in MVC validation pipeline while covering domain-specific rules from spec clarifications.
- Alternatives considered:
  - Client-only validation: Insufficient for server trust and SSR behavior.
  - External validation library: Overkill for MVP complexity.

## Decision 4: Use Post-Redirect-Get (PRG) and TempData banners

- Decision: After successful POST operations, redirect to list/detail pages and show status messages via TempData.
- Rationale: Prevents duplicate form submissions, supports consistent SSR UX, and cleanly implements missing-ID redirection behavior.
- Alternatives considered:
  - Return view directly after POST: Simpler initially but risks duplicate submissions and inconsistent URL state.

## Decision 5: Testing strategy with unit + integration layers

- Decision: Use xUnit for focused validation/mapping tests and integration tests (`WebApplicationFactory`) for route + persistence workflows.
- Rationale: Covers both conceptual learning and behavior safety without introducing heavy E2E tooling.
- Alternatives considered:
  - Integration-only tests: Slower feedback loop.
  - Unit-only tests: Misses critical MVC + DB interaction paths.

## Decision 6: Keep project architecture intentionally simple

- Decision: Single web project, no repository/service abstraction requirement for MVP.
- Rationale: Aligns with constitution simplicity principle and keeps focus on learning MVC + EF fundamentals.
- Alternatives considered:
  - Layered clean architecture from start: Useful long-term, but adds abstraction overhead for this teaching-focused MVP.
