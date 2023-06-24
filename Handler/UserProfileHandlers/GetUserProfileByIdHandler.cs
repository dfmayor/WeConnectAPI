using MediatR;
using WeConnectAPI.Query;
using WeConnectAPI.Services.UserServices;
using WeConnectAPI.Models.UserModels;

namespace WeConnectAPI.Handler.UserProfileHandlers
{
    public class GetUserProfileByIdHanlder : IRequestHandler<GetUserProfileByIdQuery, UserProfile>
    {
        private readonly IUserProfileService _userProfileService;

        public GetUserProfileByIdHanlder(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        public async Task<UserProfile> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userProfileService.GetUserProfileById(request.Id);
        }
    }
}