using MediatR;
using WeConnectAPI.Models.UserModels;

namespace WeConnectAPI.Command.UserProfileCommand
{
    public class DeleteUserProfileCommand : IRequest<UserProfile>
    {
        public string Id { get; set; }
    }
}