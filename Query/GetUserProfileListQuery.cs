using MediatR;
using WeConnectAPI.Models.UserModels;

namespace WeConnectAPI.Query
{
    public class GetUserProfileListQuery : IRequest<List<UserProfile>>
    {
        
    }
}