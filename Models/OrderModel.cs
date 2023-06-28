using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeConnectAPI.Models.UserModels;

namespace WeConnectAPI.Models
{
    public class OrderModel
    {
        [Key]
        public Guid OrderId { get; set; }
        public Guid GigId { get; set; }
        [ForeignKey("GigId")]
        public GigModel Gig { get; set; }
        public bool status { get; set; } = false;
        public string? details { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}