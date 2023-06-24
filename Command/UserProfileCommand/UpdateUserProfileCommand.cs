using MediatR;
using WeConnectAPI.Models.UserModels;

namespace WeConnectAPI.Command.UserProfileCommand
{
    public class UpdateUserProfileCommand : IRequest<UserProfile>
    {
        public UpdateUserProfileCommand(UserProfile userProfile, ApplicationUser applicationUser)
        {
            UserProfile = userProfile;
            ApplicationUser = applicationUser;
        }

        public UserProfile UserProfile { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}