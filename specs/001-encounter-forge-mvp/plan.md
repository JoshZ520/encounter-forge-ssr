# Implementation Plan: Encounter Forge MVP

**Branch**: `001-encounter-forge-mvp` | **Date**: 2026-06-02 | **Spec**: `specs/001-encounter-forge-mvp/spec.md`
**Input**: Feature specification from `specs/001-encounter-forge-mvp/spec.md`

## Summary

Build a small, server-side rendered Encounter Forge MVP for Dungeon Masters using ASP.NET Core MVC, Razor
Views, Bootstrap, SQLite, and Entity Framework Core. The implementation approach is milestone-driven and
teaching-oriented: each milestone introduces one core MVC or EF concept while delivering usable CRUD behavior
for encounters and monster entries.

## Technical Context

**Language/Version**: C# 12 on .NET 8 (LTS)  
**Primary Dependencies**: ASP.NET Core MVC, Razor Views, Bootstrap 5, Entity Framework Core, EF Core SQLite  
**Storage**: SQLite database file managed by EF Core migrations  
**Testing**: xUnit + ASP.NET Core integration tests via `WebApplicationFactory`  
**Target Platform**: Server-hosted web app (desktop and mobile browsers)  
**Project Type**: Web application (single MVC server project)  
**Performance Goals**: 95% of list/detail renders under 2s in local classroom conditions; form post/redirect
completes without repeat submission for valid data  
**Constraints**: Keep MVP scope small; server-rendered HTML only; single-user workflow; no auth/collaboration;
validation for CR formats and numeric ranges from spec  
**Scale/Scope**: Course project scope (roughly 1 developer, up to a few hundred encounters, low concurrency)

## Constitution Check

_GATE: Must pass before Phase 0 research. Re-check after Phase 1 design._

**Pre-Phase 0 Gate Review**

- Code Quality First: PASS. Plan keeps a single MVC project and avoids unnecessary architectural layers.
- Test-Driven Verification: PASS. Milestones include unit/integration test checkpoints for validation, routing,
  and persistence.
- User Experience Consistency: PASS. PRG flow, consistent navigation, and uniform validation/error banners are
  built into contract and quickstart guidance.
- Performance Budgets: PASS. Explicit page-render target (<2s p95 classroom conditions) is carried from spec.
- Scope Discipline and Simplicity: PASS. No advanced balancing engine, auth, or non-combat variant behavior in
  MVP.

## Project Structure

### Documentation (this feature)

```text
specs/001-encounter-forge-mvp/
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── contracts/
│   └── encounter-forge.yaml
└── tasks.md
```

### Source Code (repository root)

```text
src/
└── EncounterForge.Web/
    ├── Controllers/
    │   └── EncountersController.cs
    ├── Data/
    │   ├── AppDbContext.cs
    │   └── Migrations/
    ├── Models/
    │   ├── Encounter.cs
    │   └── MonsterEntry.cs
    ├── ViewModels/
    │   ├── EncounterFormViewModel.cs
    │   └── EncounterDetailsViewModel.cs
    ├── Views/
    │   ├── Encounters/
    │   │   ├── Index.cshtml
    │   │   ├── Create.cshtml
    │   │   ├── Details.cshtml
    │   │   └── Edit.cshtml
    │   └── Shared/
    ├── wwwroot/
    └── Program.cs

tests/
└── EncounterForge.Web.Tests/
    ├── Unit/
    └── Integration/
```

**Structure Decision**: Use one ASP.NET Core MVC project so architecture remains easy to learn. Keep domain
models, view models, controller actions, and views in standard MVC folders to reinforce framework conventions.

## Implementation Milestones and Learning Objectives

### Milestone 1: MVC Request Flow and Routing Foundation

- Learning objectives:
  Understand how incoming requests map to controller actions and Razor views.
  Learn how `Program.cs` wires MVC services and middleware.
- Deliverables:
  Basic app skeleton, shared layout, Bootstrap integration, and `EncountersController` with placeholder
  `Index`, `Create`, `Details`, `Edit` routes returning views.
- Checkpoint:
  Routes render expected pages with consistent navigation.

### Milestone 2: Domain Modeling and EF Core Basics

- Learning objectives:
  Understand EF Core entities, one-to-many relationships, DbContext, and migrations.
  Learn persistence lifecycle: add, query with includes, update, delete.
- Deliverables:
  `Encounter` and `MonsterEntry` entities, `AppDbContext`, initial migration, SQLite database creation.
- Checkpoint:
  Database contains normalized tables and FK relationship; simple seed data can be listed.

### Milestone 3: Create and List Encounters (P1)

- Learning objectives:
  Learn model binding, server-side validation, and Post-Redirect-Get in MVC.
- Deliverables:
  Create form with monster rows, list page, validation rules (party ranges, CR format, required fields,
  at least one monster), and success/error banners.
- Checkpoint:
  A valid encounter persists and appears on list/details pages.

### Milestone 4: Details, Edit, and Missing-ID UX (P2)

- Learning objectives:
  Learn loading relational data for display/edit and handling null/not-found paths safely.
- Deliverables:
  Details page, edit workflow for encounter + monster entries, redirect-to-list behavior with error banner when
  ID is missing.
- Checkpoint:
  Edit persists changes; missing IDs always return user to list with clear feedback.

### Milestone 5: Delete Flow, Test Coverage, and Polish (P3)

- Learning objectives:
  Learn safe delete interactions and testing strategy layering (unit + integration).
- Deliverables:
  Delete confirmation flow, integration tests for create/edit/delete and missing-ID handling, UI consistency pass.
- Checkpoint:
  Core CRUD works end-to-end and test suite validates key behavior.

## Post-Design Constitution Check

- Code Quality First: PASS. Data model and project structure remain minimal and convention-based.
- Test-Driven Verification: PASS. Quickstart and milestones require test checkpoints before feature completion.
- User Experience Consistency: PASS. Shared navigation, consistent validation messages, and redirect banner
  behavior defined in contracts and quickstart.
- Performance Budgets: PASS. No heavyweight client runtime; SSR paths and acceptance checks maintain <2s target.
- Scope Discipline and Simplicity: PASS. MVP excludes non-essential features and retains a single-server design.

## Complexity Tracking

No constitution violations or exceptions are required for this plan.
