using MediatR;
using WeConnectAPI.Models.UserModels;

namespace WeConnectAPI.Command.UserProfileCommand
{
    public class CreateUserProfileCommand : IRequest<UserProfile>
    {
        public CreateUserProfileCommand(UserProfile userProfile, ApplicationUser applicationUser)
        {
            UserProfile = userProfile;
            ApplicationUser = applicationUser;
        }

        public UserProfile UserProfile { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}