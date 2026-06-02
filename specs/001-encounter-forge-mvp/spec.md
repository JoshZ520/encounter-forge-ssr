# Feature Specification: Encounter Forge MVP

**Feature Branch**: `001-encounter-forge-mvp`  
**Created**: 2026-06-02  
**Status**: Draft  
**Input**: User description: "I want to build Encounter Forge as a server-side rendered web application for Dungeon Masters. The app should let users create, view, edit, and delete D&D combat encounters. Each encounter should have a name, party level, party size, environment, difficulty, notes, and a list of monsters. Each monster should have a name, quantity, challenge rating, and basic notes. This first version should be server-side rendered, with the server generating HTML pages. Keep the MVP small and realistic for a college full stack development project."

## Clarifications

### Session 2026-06-02

- Q: How should encounter name uniqueness work? → A: Encounter names may repeat; each encounter is uniquely identified by system-generated ID.
- Q: Can encounters be saved with zero monsters? → A: For MVP combat encounters, no; at least one monster is required on create and edit. Non-combat encounter support is future scope.
- Q: How should missing encounter IDs be handled? → A: Redirect to encounter list and show an error banner ("Encounter not found or already deleted").
- Q: How should challenge rating input be validated? → A: Store challenge rating as text and validate against common D&D formats (fractions and integers).
- Q: What numeric bounds should party fields enforce? → A: Party level 1-20 and party size 1-10.

## User Scenarios & Testing _(mandatory)_

<!--
  IMPORTANT: User stories should be PRIORITIZED as user journeys ordered by importance.
  Each user story/journey must be INDEPENDENTLY TESTABLE - meaning if you implement just ONE of them,
  you should still have a viable MVP (Minimum Viable Product) that delivers value.

  Assign priorities (P1, P2, P3, etc.) to each story, where P1 is the most critical.
  Think of each story as a standalone slice of functionality that can be:
  - Developed independently
  - Tested independently
  - Deployed independently
  - Demonstrated to users independently
-->

### User Story 1 - Create and Review an Encounter (Priority: P1)

As a Dungeon Master, I can create a combat encounter with key party details and monster entries, then view the
saved encounter on a server-rendered page, so I can prepare a session quickly.

**Why this priority**: Creating and viewing encounters is the core value of the product and establishes a
usable MVP on its own.

**Independent Test**: Can be fully tested by submitting a new encounter form and then opening the encounter
details page to confirm all entered fields and monsters are displayed correctly.

**Acceptance Scenarios**:

1. **Given** a Dungeon Master is on the new encounter page, **When** they submit valid encounter and monster
   information, **Then** the system creates the encounter and shows a confirmation in a server-rendered page.
2. **Given** an encounter exists, **When** the Dungeon Master opens that encounter, **Then** the system shows
   all encounter fields and the full monster list in a server-rendered detail view.

---

### User Story 2 - Update Encounter Plans (Priority: P2)

As a Dungeon Master, I can edit an existing encounter and its monsters so my prep stays accurate as my party or
session plans change.

**Why this priority**: Editing avoids duplicate records and keeps the encounter list useful over time.

**Independent Test**: Can be fully tested by changing existing encounter fields and monster rows, saving,
then reopening the encounter to confirm updates are persisted.

**Acceptance Scenarios**:

1. **Given** an existing encounter, **When** the Dungeon Master updates one or more fields and submits the
   edit form, **Then** the system saves and displays the updated encounter values.
2. **Given** an existing encounter with monsters, **When** the Dungeon Master updates monster details in the
   edit flow, **Then** the updated monster list appears in the encounter detail view.

---

### User Story 3 - Manage Encounter List Lifecycle (Priority: P3)

As a Dungeon Master, I can browse my encounter list and delete encounters I no longer need, so prep stays
organized and manageable.

**Why this priority**: Listing and deleting complete the CRUD workflow and keep the MVP practical for regular
use.

**Independent Test**: Can be fully tested by viewing a list with multiple encounters, deleting one, and
confirming it no longer appears while the others remain.

**Acceptance Scenarios**:

1. **Given** multiple encounters exist, **When** the Dungeon Master opens the encounter list page, **Then**
   the system displays all saved encounters with key summary details.
2. **Given** an encounter exists, **When** the Dungeon Master confirms deletion, **Then** the encounter is
   removed and no longer appears in list or detail pages.

---

### Edge Cases

- For MVP combat encounters, submissions with zero monsters are rejected on create and edit with a clear validation message.
- What happens when monster quantity is zero, negative, or non-numeric?
- Challenge rating accepts only common D&D formats (fractions like 1/8, 1/4, 1/2 and positive integers); invalid formats are rejected with field-level feedback.
- Party level and party size values outside allowed MVP ranges (level 1-20, size 1-10) are rejected with field-level validation errors.
- How does the system handle very long notes for encounters or monsters?
- If an encounter ID is missing during view, edit, or delete, the user is redirected to the encounter list with a clear error banner.
- Encounter names may repeat; duplicate names are allowed and records are distinguished by unique encounter ID.

## Requirements _(mandatory)_

### Functional Requirements

- **FR-001**: System MUST provide server-rendered HTML pages for listing, creating, viewing, editing, and
  deleting encounters.
- **FR-002**: System MUST allow users to create an encounter with name, party level, party size, environment,
  difficulty, optional notes, and at least one monster entry.
- **FR-003**: System MUST allow users to add one or more monsters to an encounter, each with name, quantity,
  challenge rating, and optional notes.
- **FR-004**: System MUST persist encounter and monster data so created records remain available across
  sessions.
- **FR-005**: System MUST allow users to view an encounter detail page that includes all encounter fields and
  associated monsters.
- **FR-006**: System MUST allow users to update encounter fields and monster fields for an existing encounter.
- **FR-007**: System MUST allow users to delete an existing encounter from the encounter detail or list flow.
- **FR-008**: System MUST validate required fields and numeric fields before saving, and MUST show clear
  server-rendered error messages when validation fails.
- **FR-009**: System MUST show an encounter list view with enough summary information for users to identify
  each encounter quickly.
- **FR-010**: System MUST preserve user-entered text formatting in notes fields as plain text when displayed.
- **FR-011**: System MUST provide a predictable navigation path between encounter list, create form, detail
  view, and edit view.
- **FR-012**: System MUST keep MVP scope to a single-user workflow without requiring collaboration, publishing,
  import/export, or advanced encounter balancing automation.
- **FR-013**: System MUST allow duplicate encounter names and MUST treat each encounter as distinct using a
  system-generated unique identifier.
- **FR-014**: System MUST require at least one monster entry when creating or editing a combat encounter in
  the MVP.
- **FR-015**: If a requested encounter ID does not exist (including already-deleted encounters), the system
  MUST redirect users to the encounter list and display a clear error banner.
- **FR-016**: System MUST store challenge rating as text and MUST validate it against allowed MVP formats:
  D&D-style fractions (1/8, 1/4, 1/2) and positive integers.
- **FR-017**: System MUST validate party level as an integer from 1 through 20 and party size as an integer
  from 1 through 10.

### Key Entities _(include if feature involves data)_

- **Encounter**: A combat prep record with name, party level, party size, environment, difficulty, notes,
  creation timestamp, and update timestamp; uniquely identified by system-generated encounter ID.
- **Monster Entry**: A monster line item belonging to one encounter, with monster name, quantity, challenge
  rating, notes, and ordering position.

## Assumptions

- The MVP serves one Dungeon Master context at a time; account management and multi-user permissions are out of
  scope.
- Difficulty is captured as a predefined label set (for example: Easy, Medium, Hard, Deadly) to keep the form
  simple.
- Challenge rating is stored as text but validated to allow only common D&D formats (fractions and positive integers).
- Party level and party size are validated as bounded integers for MVP input quality (level 1-20, size 1-10).
- Notes fields are optional and stored as plain text without rich-text editing.
- The app prioritizes desktop-first usability for coursework while remaining functional on typical mobile
  screens.
- Non-combat encounters that may allow zero monsters are out of MVP scope and may be considered in a later
  release.

## Success Criteria _(mandatory)_

### Measurable Outcomes

- **SC-001**: A Dungeon Master can create a complete encounter with at least two monsters in under 3 minutes
  on first use.
- **SC-002**: At least 90% of valid create, edit, and delete submissions complete successfully without requiring
  a page refresh or repeated submission.
- **SC-003**: 95% of encounter list and detail pages are fully rendered and visible to the user in under 2
  seconds under normal classroom demo conditions.
- **SC-004**: In user testing with at least 5 representative users, at least 4 users can complete the full
  CRUD flow without facilitator assistance.
- **SC-005**: At least 90% of validation errors are correctly identified and shown with field-specific,
  understandable messages.
