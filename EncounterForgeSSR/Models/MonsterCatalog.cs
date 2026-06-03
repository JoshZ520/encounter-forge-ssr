using System.ComponentModel.DataAnnotations;

namespace EncounterForgeSSR.Models;

public class MonsterCatalog
{
    public int Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [RegularExpression("^(1/8|1/4|1/2|[1-9][0-9]*)$")]
    public string ChallengeRating { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string MonsterType { get; set; } = string.Empty;

    [StringLength(500)]
    public string? SuggestedNotes { get; set; }
}
