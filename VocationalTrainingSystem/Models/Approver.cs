using System.ComponentModel.DataAnnotations;

namespace VocationalTrainingSystem.Models
{
    public class Approver
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
