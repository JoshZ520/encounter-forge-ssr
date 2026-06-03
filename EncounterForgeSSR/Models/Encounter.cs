using System.ComponentModel.DataAnnotations;

namespace EncounterForgeSSR.Models;

public class Encounter
{
    public int Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [Range(1, 20)]
    public int PartyLevel { get; set; }

    [Range(1, 10)]
    public int PartySize { get; set; }

    [Required]
    [StringLength(60)]
    public string Environment { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Difficulty { get; set; } = string.Empty;

    [StringLength(4000)]
    public string? Notes { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime UpdatedAtUtc { get; set; }

    public ICollection<MonsterEntry> MonsterEntries { get; set; } = new List<MonsterEntry>();
}