
using System.ComponentModel.DataAnnotations;


namespace VocationalTrainingSystem.Models
{
    public class LoginViewModel
    {
        [Required]
        public string UserID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
