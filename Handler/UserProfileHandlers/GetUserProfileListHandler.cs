using MediatR;
using WeConnectAPI.Models.UserModels;
using WeConnectAPI.Query;
using WeConnectAPI.Services.UserServices;

namespace WeConnectAPI.Handler.UserProfileHandlers
{
    public class GetUserProfileListHandler : IRequestHandler<GetUserProfileListQuery, List<UserProfile>>
    {
        private readonly IUserProfileService _userProfileService;

        public GetUserProfileListHandler(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        public async Task<List<UserProfile>> Handle(GetUserProfileListQuery request, CancellationToken cancellationToken)
        {
            return await _userProfileService.GetUserProfilesList();
        }
    }
}