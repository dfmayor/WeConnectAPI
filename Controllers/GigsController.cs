using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeConnectAPI.Command.GigModelCommand;
using WeConnectAPI.Data;
using WeConnectAPI.DTOs;
using WeConnectAPI.Models;
using WeConnectAPI.Models.UserModels;
using WeConnectAPI.Query.GigModelQueries;

namespace WeConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GigsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GigsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("create-gig")]
        // authorise user with Seller role
        public async Task<GenericResponses> CreateGig(GigModelDto gigModelDto)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == gigModelDto.CategoryId);
            var user =  await _userManager.FindByIdAsync(gigModelDto.UserId);
            var gig = _mapper.Map<GigModel>(gigModelDto);
            if (category == null)
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Category not found!!!",
                    Data = null,
                };
            }
            if (user == null)
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "User does not exist!!!",
                    Data = null,
                };
            }
            var gigModelCommand = new CreateGigModelCommand(gig, user, category);
            try
            {
                var createdGig = await _mediator.Send(gigModelCommand);
                return new GenericResponses()
                {
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Gig created successfully",
                    Data = gigModelDto,
                };
            }
            catch (Exception ex)
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = $"Failed to create gig: {ex.Message}",
                    Data = null
                };
            }
        }

        [HttpGet]
        [Route("get-gig-by-id")]
        public async Task<GenericResponses> GetGigModelById(Guid Id)
        {
            try
            {
                var gig = await _mediator.Send(new GetGigModelByIdQuery() {Id = Id});
                if (gig != null)
                {
                    return new GenericResponses()
                    {
                        Status = HttpStatusCode.OK.ToString(),
                        Message = "Gig Retrieved Successfully",
                        Data = gig,
                    };
                }
                else
                {
                    return new GenericResponses()
                    {
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Gig Not Found",
                        Data = null,
                    };
                }
            }
            catch (Exception ex)
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = $"Failed To Retrieve Gig: {ex.Message}",
                    Data = null
                };
            }
        }

        [HttpGet]
        [Route("get-all-gigs-list")]
        public async Task<List<GigModel>> GetGigModelList()
        {
            var gig = await _mediator.Send(new GetGigModelListQuery());
            if (gig != null)
            {
                return gig;
            }
            else
            {
                return null;
            }
        }

        [HttpPut]
        [Route("update-gigs")]
        public async Task<GenericResponses> UpdateGigModel([FromBody] GigModelDto gigModelDto)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == gigModelDto.CategoryId);
            var user =  await _userManager.FindByIdAsync(gigModelDto.UserId);
            var gig = _mapper.Map<GigModel>(gigModelDto);
            if (category == null)
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Category not found!!!",
                    Data = null,
                };
            }
            if (user == null)
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "User does not exist!!!",
                    Data = null,
                };
            }
            var gigModelCommand = new UpdateGigModelCommand(gig, user, category);
            try
            {
                var createdGig = await _mediator.Send(gigModelCommand);
                return new GenericResponses()
                {
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Gig updated successfully",
                    Data = gigModelDto,
                };
            }
            catch (Exception ex)
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = $"Failed to update gig: {ex.Message}",
                    Data = null
                };
            }
        }

        [HttpDelete]
        [Route("delete-gig")]
        public async Task<GenericResponses> DeleteGig(string id)
        {
            try
            {
                var gigModel = await _mediator.Send(new DeleteGigModelCommand() {Id = id});
                return new GenericResponses()
                {
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Gig Deleted Successfully",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = $"Failed to delete gig, {ex.Message}",
                    Data = null
                }; 
            }
        }
    }
}