namespace WeConnectAPI.DTOs
{
    public class UserProfileDto
    {
        public string ApplicationUserId { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Occupation { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string? ProfilePicture { get; set; }
    }
}