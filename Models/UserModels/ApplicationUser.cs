using System;
using Microsoft.AspNetCore.Identity;

namespace WeConnectAPI.Models.UserModels
{

    public class ApplicationUser : IdentityUser
    {
        public string RegisterNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<GigModel> Gigs { get; set; }
        public List<Review> Reviews { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}