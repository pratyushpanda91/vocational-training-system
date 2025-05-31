using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VocationalTrainingSystem.Models
{
    public class PresentAddress
    {
        [Key]
        public int Id { get; set; }

        public string At { get; set; }
        public string Post { get; set; }
        public string Dist { get; set; }
        public string TownOrCity { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int PIN { get; set; }

        // Foreign key
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public UserDetail User { get; set; }
    }

}
