using MediatR;
using WeConnectAPI.Models.UserModels;

namespace WeConnectAPI.Query
{
    public class GetUserProfileByIdQuery : IRequest<UserProfile>
    {
        public string Id { get; set; }
    }
}