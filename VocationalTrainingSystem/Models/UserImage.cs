using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VocationalTrainingSystem.Models
{
    public class UserImage
    {
        [Key]
        public int Id { get; set; }

        public byte[] Photo { get; set; }

        // Foreign key
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public UserDetail User { get; set; }
    }

}

