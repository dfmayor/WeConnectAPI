using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WeConnectAPI.Command.UserProfileCommand;
using WeConnectAPI.Data;
using WeConnectAPI.Models.UserModels;
using WeConnectAPI.Services.UserServices;

namespace WeConnectAPI.Handler.UserProfileHandlers
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, UserProfile>
    {
        private readonly IUserProfileService _userProfileService;
        private readonly ApplicationDbContext _dbContext;

        public UpdateUserProfileCommandHandler(IUserProfileService userProfileService, ApplicationDbContext dbContext)
        {
            _userProfileService = userProfileService;
            _dbContext = dbContext;
        }

        public async Task<UserProfile> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            // find user profile with the request.ApplicationUserId 
            var profile = _dbContext.UserProfiles.Where(u => u.ApplicationUserId == request.ApplicationUser.Id).FirstOrDefault();
            if (profile != null)
            {
                profile.Gender = request.UserProfile.Gender;
                profile.MaritalStatus = request.UserProfile.MaritalStatus;
                profile.Occupation = request.UserProfile.Occupation;
                profile.Address = request.UserProfile.Address;
                profile.DateOfBirth = request.UserProfile.DateOfBirth;
                profile.Nationality = request.UserProfile.Nationality;
                profile.ProfilePicture = request.UserProfile.ProfilePicture;
                profile.UpdatedAt = DateTime.Now;
                UserProfile createdUserProfile = await _userProfileService.UpdateUserProfile(profile);
                return createdUserProfile;
            }
            else
                return null;
        }
    }
}
