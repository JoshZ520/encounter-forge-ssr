using System.ComponentModel.DataAnnotations;

namespace EncounterForgeSSR.Models;

public class AppUser
{
    public int Id { get; set; }

    [Required]
    [StringLength(40)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string PasswordHash { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
