using System.ComponentModel.DataAnnotations;

namespace EncounterForgeSSR.ViewModels;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Username")]
    [StringLength(40, MinimumLength = 3)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
