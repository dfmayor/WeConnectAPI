using System.ComponentModel.DataAnnotations;

namespace WeConnectAPI.Models
{
    public class GigReview
    {
        [Key]
        public Guid GigReviewId { get; set; }
        public Guid GigId { get; set; }
        public GigModel Gig { get; set; }
        public Guid ReviewId { get; set; }
        public Review Review { get; set; }
    }
}