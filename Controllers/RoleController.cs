using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeConnectApi.Services.RoleServices;
using WeConnectAPI.DTOs;
using WeConnectAPI.Models.UserModels;

namespace WeConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(IRoleService roleService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleService = roleService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("add-role")]
        public async Task<GenericResponses> AddRole([FromBody] string roleName)
        {
            var result = await _roleService.AddRole(roleName);
            if (result)
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.OK.ToString(),
                    Message = $"{roleName} Role Added Successfully",
                    Data = new
                    {
                        RoleName = roleName
                    }
                };
            }
            return new GenericResponses()
                {
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Failed to add role",
                    Data = null
                };
        }

        [HttpGet]
        [Route("get-all-roles")]
        public async Task<GenericResponses> GetAllRoles()
        {
            var result = await _roleService.GetRoleList();
            if (result.Count > 0)
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "All Roles Retrieved Successfully",
                    Data = result
                };
            }
            else
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "No Role Has Been Added!!!",
                    Data = null
                };
            }
        }

        public class EditRoleRequest
        {
            public string? RoleName { get; set; }
            public string? EditedRole { get; set; }
        }

        [HttpPut]
        [Route("edit-role")]
        public async Task<GenericResponses> EditRole([FromBody] EditRoleRequest request)
        {
            var result = await _roleService.EditRole(request.RoleName, request.EditedRole);
            if (result)
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.OK.ToString(),
                    Message = $"{request.RoleName} Role Edited Successfully",
                    Data = new
                    {
                        OldRoleName = request.RoleName,
                        NewRoleName = request.EditedRole
                    }
                };
            }
            return new GenericResponses()
                {
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Failed to edit role",
                    Data = null
                };
        }

        [HttpDelete]
        [Route("delete-role")]
        public async Task<GenericResponses> DeleteRole([FromBody] string roleName)
        {
            var result = await _roleService.DeleteRole(roleName);
            if (result)
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.OK.ToString(),
                    Message = $"{roleName} Role Deleted Successfully",
                    Data = result
                };
            }
            return new GenericResponses()
            {
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Failed to delete role",
                Data = null
            };
        }

        // add more roles to users
        [HttpPost]
        [Route("add-more-role-to-user")]
        public async Task<GenericResponses> AddRoleToUser([FromBody] ChangeRoleDto changeRoleDto)
        {
            // get user with the id
            var user = await _userManager.FindByIdAsync(changeRoleDto.UserId);
            if (user != null)
            {
                // get roles of the user
                var roleNames = await _userManager.GetRolesAsync(user);
                bool roleExists = roleNames.Contains(changeRoleDto.RoleName);
                if (roleExists)
                {
                    return new GenericResponses()
                    {
                        Status = HttpStatusCode.OK.ToString(),
                        Message = $"{user.UserName} Already Has Role {changeRoleDto.RoleName}",
                        Data = null
                    };
                }
                else
                {
                    await _userManager.AddToRolesAsync(user, roleNames);
                    return new GenericResponses()
                    {
                        Status = HttpStatusCode.OK.ToString(),
                        Message = $"{changeRoleDto.RoleName} Role Added To {user.UserName}",
                        Data = true
                    };
                }
            }
            else
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "User not found",
                    Data = null
                };
            }
        }
    }
}