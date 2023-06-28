using System.Net;
using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeConnectAPI.Command.UserProfileCommand;
using WeConnectAPI.DTOs;
using WeConnectAPI.Models.UserModels;
using WeConnectAPI.Query;

namespace WeConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserProfileController(IMediator mediator, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _mediator = mediator;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        [Route("create-user-profile")]
        public async Task<GenericResponses> CreateProfile(UserProfileDto userProfileDto)
        {
            var applicationUser = await _userManager.FindByIdAsync(userProfileDto.ApplicationUserId);
            var userProfile = _mapper.Map<UserProfile>(userProfileDto);
            var createUserProfileCommand = new CreateUserProfileCommand(userProfile, applicationUser);
            try
            {
                var createdUserProfile = await _mediator.Send(createUserProfileCommand);
                return new GenericResponses()
                {
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "User Profile Created Successfully",
                    Data = userProfileDto,
                };
            }
            catch (Exception ex)
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = $"User Profile Creation Failed: {ex.Message}",
                    Data = null
                };
            }
        }

        [HttpGet]
        //[Authorize]
        [Route("get-user-profile-by-id")]
        public async Task<GenericResponses> UserProfileById(string Id)
        {
            try
            {
                var userProfile = await _mediator.Send(new GetUserProfileByIdQuery() {Id = Id});
                if (userProfile != null)
                {
                    return new GenericResponses()
                    {
                        Status = HttpStatusCode.OK.ToString(),
                        Message = "User Profile Retrieved Successfully",
                        Data = userProfile,
                    };
                }
                else
                {
                    return new GenericResponses()
                    {
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "User Profile Not Found",
                        Data = null,
                    };
                }
                
            }
            catch (Exception ex)
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = $"Failed To Retrieve User Profile: {ex.Message}",
                    Data = null
                };
            }
        }

        [HttpGet]
        //[Authorize]
        [Route("get-all-user-profiles")]
        public async Task<List<UserProfile>> UserProfileList()
        {
            var users = await _mediator.Send(new GetUserProfileListQuery());
            if (users != null)
            {
                return users;
            }
            else
            {
                return null;
            }
        }

        [HttpPut]
        [Authorize]
        [Route("edit-user-profile")]
        public async Task<GenericResponses> UpdateUserProfile(UserProfileDto userProfileDto)
        {
            var applicationUser = await _userManager.FindByIdAsync(userProfileDto.ApplicationUserId);
            var userProfile = _mapper.Map<UserProfile>(userProfileDto);
            var updateUserProfileCommand = new UpdateUserProfileCommand(userProfile, applicationUser);
            try
            {
                var updatedUserProfile = await _mediator.Send(updateUserProfileCommand);
                return new GenericResponses()
                {
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "User Profile Updated Successfully",
                    Data = userProfileDto,
                };
            }
            catch (Exception ex)
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = $"User Profile Update Failed: {ex.Message}",
                    Data = {}
                };
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("delete-user-profile")]
        public async Task<GenericResponses> DeleteUserProfile(string Id)
        {
            try
            {
                var userProfile = await _mediator.Send(new DeleteUserProfileCommand() {Id = Id});
                return new GenericResponses()
                {
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "User Profile Deleted Successfully",
                    Data = null
                };
            }
            catch (Exception ex)
            {
               return new GenericResponses()
                {
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = $"User Profile Cannot Be Deleted!!!, {ex.Message}",
                    Data = null
                }; 
            }
        }
    }
}