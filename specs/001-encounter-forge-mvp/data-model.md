# Data Model: Encounter Forge MVP

## Entity: Encounter

- Purpose: Represents one combat encounter prepared by a Dungeon Master.
- Primary key:
  - `Id` (int, auto-increment)
- Fields:
  - `Name` (string, required, max length 120)
  - `PartyLevel` (int, required, range 1-20)
  - `PartySize` (int, required, range 1-10)
  - `Environment` (string, required, max length 60)
  - `Difficulty` (string, required, allowed set for MVP such as Easy/Medium/Hard/Deadly)
  - `Notes` (string, optional, max length 4000)
  - `CreatedAtUtc` (DateTime, required)
  - `UpdatedAtUtc` (DateTime, required)
- Relationships:
  - One-to-many with `MonsterEntry`.
  - Minimum one `MonsterEntry` required for MVP combat encounters.

## Entity: MonsterEntry

- Purpose: Represents one monster line item in an encounter.
- Primary key:
  - `Id` (int, auto-increment)
- Foreign key:
  - `EncounterId` (int, required, references `Encounter.Id`)
- Fields:
  - `Name` (string, required, max length 120)
  - `Quantity` (int, required, minimum 1)
  - `ChallengeRating` (string, required, validated as fraction `1/8|1/4|1/2` or positive integer)
  - `Notes` (string, optional, max length 2000)
  - `SortOrder` (int, required, used to preserve display order)
- Relationships:
  - Belongs to exactly one `Encounter`.

## Validation Rules (from spec clarifications)

- Duplicate encounter names are allowed; identity is by `Encounter.Id`.
- Encounter create/edit must include at least one monster entry.
- Missing encounter ID for view/edit/delete triggers redirect to list with error banner.
- Party field bounds enforced server-side:
  - `PartyLevel`: 1-20
  - `PartySize`: 1-10
- Challenge rating accepted formats:
  - `1/8`, `1/4`, `1/2`
  - Positive integers like `1`, `2`, `10`

## State Transitions

- Encounter lifecycle:
  - `Created` -> `Updated` (zero or many times) -> `Deleted`
- Monster entry lifecycle (within an encounter):
  - `Added` -> `Edited` -> `Removed`
- Delete behavior:
  - Deleting an encounter cascades deletion to its monster entries.
