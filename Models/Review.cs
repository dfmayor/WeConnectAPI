using System.ComponentModel.DataAnnotations;

namespace WeConnectAPI.Models
{
    public class Review
    {
        [Key]
        public Guid ReviewId { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public List<GigReview> GigReviews { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}