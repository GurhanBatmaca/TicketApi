﻿using System.ComponentModel.DataAnnotations;

namespace Shared;

public class RegisterModel
{

    [Required]
    [StringLength(20,MinimumLength = 3)]
    public string? UserName { get; set; }

    [Required]
    [StringLength(20,MinimumLength = 3)]
    public string? FirstName { get; set; }
    
    [Required]
    [StringLength(20,MinimumLength = 2)]
    public string? LastName { get; set; }

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
