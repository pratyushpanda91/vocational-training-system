namespace VocationalTrainingSystem.Models;
using System.ComponentModel.DataAnnotations;

public class RegisterViewModel
{
    [Required]
    public string UserID { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Role { get; set; }  // "Admin" or "Approver"
}

