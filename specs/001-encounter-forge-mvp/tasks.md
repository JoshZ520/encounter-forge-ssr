# Tasks: Encounter Forge MVP

**Input**: Design documents from `/specs/001-encounter-forge-mvp/`
**Prerequisites**: plan.md (required), spec.md (required for user stories), research.md, data-model.md, contracts/

**Tests**: Automated tests are deferred for this small beginner-first task list; each story includes manual independent validation tasks.

**Organization**: Tasks are grouped by user story so each story can be built and verified one step at a time.

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Understand the generated MVC project and prepare feature folders

- [x] T001 Review generated MVC request flow in EncounterForgeSSR/Program.cs, EncounterForgeSSR/Controllers/HomeController.cs, and EncounterForgeSSR/Views/Shared/\_Layout.cshtml
- [x] T002 Create feature folders EncounterForgeSSR/Data, EncounterForgeSSR/ViewModels, and EncounterForgeSSR/Views/Encounters
- [x] T003 Add an `Encounters` navigation link placeholder in EncounterForgeSSR/Views/Shared/\_Layout.cshtml

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Build core domain and persistence foundation used by all user stories

**⚠️ CRITICAL**: Complete this phase before starting user story implementation

- [x] T004 Create encounter entity with required fields and validation attributes in EncounterForgeSSR/Models/Encounter.cs
- [x] T005 Create monster entry entity with FK and validation attributes in EncounterForgeSSR/Models/MonsterEntry.cs
- [x] T006 Configure one-to-many relationship navigation properties between models in EncounterForgeSSR/Models/Encounter.cs and EncounterForgeSSR/Models/MonsterEntry.cs
- [x] T007 Create EF Core DbContext with DbSet properties and relationship mapping in EncounterForgeSSR/Data/AppDbContext.cs
- [x] T008 Configure SQLite connection string and DbContext registration in EncounterForgeSSR/appsettings.json and EncounterForgeSSR/Program.cs

**Checkpoint**: Domain model and EF Core setup are ready for feature implementation

---

## Phase 3: User Story 1 - Create and Review an Encounter (Priority: P1) 🎯 MVP

**Goal**: Let a Dungeon Master create an encounter and view list/detail pages in SSR

**Independent Test**: Create an encounter with monsters, then open the list and details pages to verify data display

### Implementation for User Story 1

- [ ] T009 [US1] Create Encounters controller skeleton with Index, Create (GET/POST), and Details actions in EncounterForgeSSR/Controllers/EncountersController.cs
- [ ] T010 [P] [US1] Create encounter form view model for nested monster inputs in EncounterForgeSSR/ViewModels/EncounterFormViewModel.cs
- [ ] T011 [US1] Build create form Razor view with validation summary and one monster input row in EncounterForgeSSR/Views/Encounters/Create.cshtml
- [ ] T012 [US1] Build encounter list and details Razor views in EncounterForgeSSR/Views/Encounters/Index.cshtml and EncounterForgeSSR/Views/Encounters/Details.cshtml
- [ ] T013 [US1] Create and apply initial EF migration in EncounterForgeSSR/Data/Migrations/ and verify SQLite file creation
- [ ] T014 [US1] Manually validate create + details flow using criteria in specs/001-encounter-forge-mvp/quickstart.md

**Checkpoint**: User Story 1 is functional and independently testable

---

## Phase 4: User Story 2 - Update Encounter Plans (Priority: P2)

**Goal**: Allow editing existing encounters and monster entries

**Independent Test**: Edit encounter and monster values, save, then reopen details to verify updates

### Implementation for User Story 2

- [ ] T015 [US2] Add Edit (GET/POST) actions with model binding and validation handling in EncounterForgeSSR/Controllers/EncountersController.cs
- [ ] T016 [US2] Create edit Razor view for encounter fields and monster rows in EncounterForgeSSR/Views/Encounters/Edit.cshtml
- [ ] T017 [US2] Implement missing-ID redirect with error banner for edit/details actions in EncounterForgeSSR/Controllers/EncountersController.cs
- [ ] T018 [US2] Manually validate update workflow and missing-ID behavior using specs/001-encounter-forge-mvp/spec.md

**Checkpoint**: User Stories 1 and 2 both work independently

---

## Phase 5: User Story 3 - Manage Encounter List Lifecycle (Priority: P3)

**Goal**: Allow deleting encounters and keeping list management clean

**Independent Test**: Delete one encounter from details/list and confirm it no longer appears while others remain

### Implementation for User Story 3

- [ ] T019 [US3] Add delete POST action with success/error TempData banner behavior in EncounterForgeSSR/Controllers/EncountersController.cs
- [ ] T020 [US3] Add delete form/button in EncounterForgeSSR/Views/Encounters/Details.cshtml and action links in EncounterForgeSSR/Views/Encounters/Index.cshtml
- [ ] T021 [US3] Manually validate delete flow and not-found redirect behavior using specs/001-encounter-forge-mvp/spec.md

**Checkpoint**: All three user stories are functional and independently testable

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Final consistency pass and learning wrap-up

- [ ] T022 Review labels, validation messages, and banner wording for consistent UX across EncounterForgeSSR/Views/Encounters/\*.cshtml
- [ ] T023 Run full milestone walkthrough from specs/001-encounter-forge-mvp/quickstart.md and record completion notes in specs/001-encounter-forge-mvp/checklists/requirements.md

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: Start immediately
- **Foundational (Phase 2)**: Depends on Setup completion; blocks all user stories
- **User Story phases (Phase 3-5)**: Depend on Foundational completion
- **Polish (Phase 6)**: Depends on all user stories being complete

### User Story Dependencies

- **US1 (P1)**: Starts first after Foundational; delivers MVP
- **US2 (P2)**: Depends on US1 controller/views existing
- **US3 (P3)**: Depends on US1 list/details views and controller routes

### Within Each User Story

- Controller action scaffolding before view wiring
- View model updates before complex form rendering
- Manual validation task completes the story

### Parallel Opportunities

- T010 can run in parallel with T009 (different files)
- After Foundational phase, different team members can split US2 and US3 prep work

---

## Parallel Example: User Story 1

```bash
Task: "Create Encounters controller skeleton in EncounterForgeSSR/Controllers/EncountersController.cs"
Task: "Create encounter form view model in EncounterForgeSSR/ViewModels/EncounterFormViewModel.cs"
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1 and Phase 2
2. Complete Phase 3 (US1)
3. Validate with T014 before moving on

### Incremental Delivery

1. Deliver US1 create/list/details
2. Add US2 edit flow
3. Add US3 delete flow
4. Finish with consistency and walkthrough tasks

### Beginner-Friendly Pace

1. Complete one task at a time in ID order
2. Run the app after each controller/view change
3. Use manual validation tasks as stop points before continuing

---

## Notes

- Each task is intentionally small so you can implement it yourself with guidance
- Keep commits small (one task or one logical pair)
- If a task feels too large, split it locally before coding
