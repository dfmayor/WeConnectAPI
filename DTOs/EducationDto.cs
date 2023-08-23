namespace WeConnectAPI.DTOs
{
    public class EducationDto
    {
        public string ApplicationUserId { get; set; }
        public string School { get; set; }
        public string Qualification { get; set; }
        public string Course { get; set; }
        public DateTime GraduationYear { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}