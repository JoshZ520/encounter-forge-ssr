# Quickstart: Encounter Forge MVP (Learning-Focused)

## Goal

Build a small ASP.NET Core MVC + EF Core application that supports SSR CRUD for D&D combat encounters while learning MVC architecture and EF fundamentals.

## Prerequisites

- .NET 8 SDK installed
- IDE/editor with C# support
- Basic C# syntax familiarity

## Milestone 1: Create MVC Skeleton and Understand Request Flow

1. Create solution and MVC web project under `src/EncounterForge.Web`.
2. Add Bootstrap via static files/layout.
3. Create `EncountersController` with placeholder `Index`, `Create`, `Details`, `Edit` actions returning views.
4. Add nav links in shared layout.

Learning objectives:

- Understand MVC routing to controller actions.
- Understand how Razor views render server-side HTML.

Checkpoint:

- Navigating to encounter routes returns HTML views without runtime errors.

## Milestone 2: Add EF Core and SQLite Persistence

1. Add EF Core and SQLite packages.
2. Create entities: `Encounter`, `MonsterEntry`.
3. Create `AppDbContext` and configure one-to-many relationship.
4. Add initial migration and apply database update.

Learning objectives:

- Understand DbContext, entity relationships, and migrations.
- Understand how model classes map to SQLite tables.

Checkpoint:

- SQLite DB created with encounter and monster tables and FK relationship.

## Milestone 3: Implement Create + List with Validation

1. Build `Create` form with encounter fields and repeatable monster rows.
2. Implement POST create action using model binding.
3. Add validation rules:
   - Party level 1-20
   - Party size 1-10
   - Quantity >= 1
   - CR format (1/8, 1/4, 1/2, positive integers)
   - At least one monster
4. Redirect to list/details after success (PRG).

Learning objectives:

- Understand model binding, ModelState, and server-side validation.
- Learn PRG pattern and feedback messaging.

Checkpoint:

- Valid submissions persist and display; invalid submissions show field-level errors.

## Milestone 4: Implement Details + Edit + Missing-ID Behavior

1. Implement details page loading encounter plus monsters.
2. Implement edit GET/POST for encounter and nested monster entries.
3. Handle missing IDs by redirecting to list with error banner.

Learning objectives:

- Understand querying relational data with EF Core includes.
- Understand update workflows and consistency in SSR UX.

Checkpoint:

- Edits persist correctly and missing IDs follow agreed redirect behavior.

## Milestone 5: Implement Delete + Add Tests + Polish

1. Add delete confirmation and POST delete action.
2. Ensure cascade delete for related monsters.
3. Add tests:
   - Unit tests for validation helpers/rules.
   - Integration tests for create/edit/delete and missing-ID redirect flow.
4. Apply UI consistency pass (labels, validation summary, banners).

Learning objectives:

- Understand safe delete flow and regression testing strategy.
- Learn how tests protect key business behavior.

Checkpoint:

- Full CRUD works; tests pass; UI flow is consistent across pages.

## Suggested Study Topics per Milestone

- Milestone 1: MVC pattern, middleware pipeline, Razor syntax
- Milestone 2: EF Core entities, migrations, SQLite provider
- Milestone 3: Model binding and DataAnnotations
- Milestone 4: Relationship loading, update patterns, PRG
- Milestone 5: xUnit fundamentals and integration testing with `WebApplicationFactory`
