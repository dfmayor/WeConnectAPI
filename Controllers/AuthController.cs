using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WeConnectApi.Utilities;
using WeConnectAPI.DTOs;
using WeConnectAPI.Models.UserModels;

namespace WeConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        // register users
        [HttpPost]
        [Route("signup")]
        public async Task<GenericResponses> Register([FromBody] RegisterDto registerDto)
        {
            var isUserExist = await _userManager.FindByNameAsync(registerDto.UserName);
            if (isUserExist != null)
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.OK.ToString(),
                    Message = $"{registerDto.UserName} Already Exists!! Try Another UserName",
                    Data = {}
                };
            }
            //var userRole = await _roleManager.FindByNameAsync("User");
            ApplicationUser newUser = new ApplicationUser()
            {
                RegisterNumber = GenerateUserCode.GetCode(),
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PhoneNumber = registerDto.PhoneNumber,
                SecurityStamp = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now              
            };
            var createdUserresult = await _userManager.CreateAsync(newUser, registerDto.Password);
            if (!createdUserresult.Succeeded)
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "User Creation Failed",
                    Data = {}
                };
            }
            await _userManager.AddToRoleAsync(newUser, "Users");
            return new GenericResponses()
            {
                Status = HttpStatusCode.OK.ToString(),
                Message = "User Created Successfully",
                Data = registerDto
            };
        }

        // login route
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user is null)
                return Unauthorized("Username not found!");
            
            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordCorrect)
                return Unauthorized("Incorrect Password");
            
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("JWTID", Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GenerateJsonWebToken(authClaims);
            Response.Headers.Add("Authorization", "Bearer " + token);
            return Ok(token);
        }

        private string GenerateJsonWebToken(List<Claim> claims)
        {
            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var tokenObject = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(7),
                claims: claims,
                signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
            );
            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);
            return token;
        }
    }
}