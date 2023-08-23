using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeConnectAPI.Command.EducationCommand;
using WeConnectAPI.DTOs;
using WeConnectAPI.Models;
using WeConnectAPI.Models.UserModels;
using WeConnectAPI.Query.EducationQueries;

namespace WeConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EducationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public EducationController(IMediator mediator, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _mediator = mediator;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        [Route("create-education-qualifications")]
        public async Task<GenericResponses> CreateEducation(EducationDto educationDto)
        {
            var user = await _userManager.FindByIdAsync(educationDto.ApplicationUserId);
            var education = _mapper.Map<Education>(educationDto);
            var createdEducationCommand = new CreateEducationCommand(education, user);
            try
            {
                var createdEducation = await _mediator.Send(createdEducationCommand);
                return new GenericResponses()
                {
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Education Created Successfully",
                    Data = educationDto,
                };
            }
            catch (Exception ex)
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = $"Education Creation Failed: {ex.Message}",
                    Data = null
                };
            }
        }

        [HttpGet]
        [Route("get-education-by-user-id")]
        public async Task<GenericResponses> GetEducationListById(string Id)
        {
            try
            {
                var education = await _mediator.Send(new GetEducationListByIdQuery() {Id = Id});
                if (education != null)
                {
                    return new GenericResponses()
                    {
                        Status = HttpStatusCode.OK.ToString(),
                        Message = "Education Retrieved Successfully",
                        Data = education,
                    };
                }
                else
                {
                    return new GenericResponses()
                    {
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Education Data Not Found",
                        Data = null,
                    };
                }
            }
            catch (Exception ex)
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = $"Failed To Retrieve Education: {ex.Message}",
                    Data = null
                };
            }
        }

        [HttpPut]
        [Route("edit-user-education-details")]
        public async Task<GenericResponses> UpdateEducation(EducationDto educationDto)
        {
            var user = await _userManager.FindByIdAsync(educationDto.ApplicationUserId);
            var education = _mapper.Map<Education>(educationDto);
            var updateEducationCommand = new UpdateEducationCommand(education, user);
            try
            {
                var updatedEducation = await _mediator.Send(updateEducationCommand);
                return new GenericResponses()
                {
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Education Updated Successfully",
                    Data = educationDto,
                };
            }
            catch (Exception ex)
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = $"Education Update Failed: {ex.Message}",
                    Data = null
                };
            }
        }
    }
}