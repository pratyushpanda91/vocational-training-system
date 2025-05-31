using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace VocationalTrainingSystem.Models
{
    public class UserDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string PhoneNumber { get; set; }
        public string Gmail { get; set; }
        public string AadharNumber { get; set; }
        public DateTime DOB { get; set; }
        public string IdentificationType { get; set; }
        public string Qualification { get; set; }
        public string College { get; set; }
        public string Course { get; set; }
        public string RegdNo { get; set; }
        public string MarksOrCgpa { get; set; }
        public int PassingYear { get; set; }
        public string Gender { get; set; }

        // Navigation Properties
        public PresentAddress PresentAddress { get; set; }
        public PermanentAddress PermanentAddress { get; set; }
        public UserImage Image { get; set; }
    }

}
