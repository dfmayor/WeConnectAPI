using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeConnectAPI.Models.UserModels;

namespace WeConnectAPI.Models
{
    public class GigModel
    {
        [Key]
        public Guid GigId { get; set; }
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public List<GigReview> GigReviews { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
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