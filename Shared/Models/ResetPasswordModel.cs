using System.ComponentModel.DataAnnotations;

namespace Shared;

public class ResetPasswordModel
{
    [Required]
    public string? Token { get; set; }
    
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(20,MinimumLength = 6)]
    public string? Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(20,MinimumLength = 6)]
    [Compare("Password")]
    public string? RePassword { get; set; }
}
