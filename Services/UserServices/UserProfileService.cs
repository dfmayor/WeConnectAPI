using WeConnectAPI.Data;
using WeConnectAPI.Models.UserModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace WeConnectAPI.Services.UserServices
{
    public class UserProfileService : IUserProfileService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserProfileService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<UserProfile> CreateUserProfile(UserProfile userProfile)
        {
            var result = _dbContext.UserProfiles.Add(userProfile);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<UserProfile> DeleteUserProfile(UserProfile userProfile)
        {
            var result = _dbContext.UserProfiles.Remove(userProfile);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<UserProfile> GetUserProfileById(string Id) // id of the ApplicationUser
        {
            var userRole = await GetUserRoles(Id);
            var userData = await _dbContext.UserProfiles.Where(u => u.ApplicationUserId == Id)
                .Select(u => new UserProfile
                {
                    ApplicationUserId = u.ApplicationUserId,
                    ApplicationUser = u.ApplicationUser,
                    Roles = userRole,
                    Gender = u.Gender,
                    MaritalStatus = u.MaritalStatus,
                    Occupation = u.Occupation,
                    DateOfBirth = u.DateOfBirth,
                    Nationality = u.Nationality,
                    ProfilePicture = u.ProfilePicture,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt
                })
                .FirstOrDefaultAsync();
            if (userData != null)
            {
                return userData;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<UserProfile>> GetUserProfilesList()
        {
            var userData = await _dbContext.UserProfiles.ToListAsync();
            foreach (var userProfile in userData)
            {
                var applicationUser = await _userManager.FindByIdAsync(userProfile.ApplicationUserId);
                var userRole = await GetUserRoles(userProfile.ApplicationUserId);
                userProfile.ApplicationUser = applicationUser;
                userProfile.Roles = userRole;
            }
            return userData;
        }

        public async Task<UserProfile> UpdateUserProfile(UserProfile userProfile)
        {
            var result = _dbContext.UserProfiles.Update(userProfile);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        private async Task<List<IdentityRole>> GetUserRoles(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
                return null;
            var roleNames = await _userManager.GetRolesAsync(user);
            var roles = new List<IdentityRole>();
            foreach (var roleName in roleNames)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                    roles.Add(role);
            }
            return roles;
        }
    }
}