namespace WeConnectAPI.DTOs
{
    public class GigModelDto
    {
        public Guid CategoryId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? GigPicture { get; set; }
        public double Price { get; set; }
        public DateTime DeliveryTime { get; set; }
        public bool Status { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}