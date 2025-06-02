using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VocationalTrainingSystem.ViewModels
{
    public class UserProfileViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        // UserDetails
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

        // Present Address
        public string PresentAt { get; set; }
        public string PresentPost { get; set; }
        public string PresentDist { get; set; }
        public string PresentTownOrCity { get; set; }
        public string PresentState { get; set; }
        public string PresentCountry { get; set; }
        public int PresentPIN { get; set; }

        // Permanent Address
        public string PermanentAt { get; set; }
        public string PermanentPost { get; set; }
        public string PermanentDist { get; set; }
        public string PermanentTownOrCity { get; set; }
        public string PermanentState { get; set; }
        public string PermanentCountry { get; set; }
        public int PermanentPIN { get; set; }

        // Image
        public IFormFile Photo { get; set; }
    }
}
