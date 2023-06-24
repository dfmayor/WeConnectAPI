// redundant, except if needed to extend the IdentityRole

using WeConnectAPI.Models.BaseModels;

namespace WeConnectAPI.Models.RoleModels
{
    public class RoleModel : BaseModel
    {
        public int Id { get; set; }
        public string? UserRole { get; set; }
    }
}