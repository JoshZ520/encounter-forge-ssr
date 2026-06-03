using System.ComponentModel.DataAnnotations;

namespace EncounterForgeSSR.Models;

public class MonsterEntry
{
    public int Id { get; set; }

    public int EncounterId { get; set; }

    public int? MonsterCatalogId { get; set; }

    public Encounter? Encounter { get; set; }

    [StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [RegularExpression("^(1/8|1/4|1/2|[1-9][0-9]*)$")]
    public string ChallengeRating { get; set; } = string.Empty;

    [StringLength(2000)]
    public string? Notes { get; set; }

    public int SortOrder { get; set; }
}