using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeConnectAPI.Models.UserModels;

namespace WeConnectAPI.Models
{
    public class Education
    {
        [Key]
        public Guid EducationId { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public string School { get; set; }
        public string Qualification { get; set; }
        public string Course { get; set; }
        public DateTime GraduationYear { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}