// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using Microsoft.IdentityModel.Tokens;

// namespace WeConnectAPI.Utilities
// {
//     public class WebTokens
//     {
//         private readonly IConfiguration _configuration;

//         public WebTokens(IConfiguration configuration)
//         {
//             _configuration = configuration;
//         }

//         public static string GenerateJsonWebToken(List<Claim> claims)
//         {
//             var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
//             var tokenObject = new JwtSecurityToken(
//                 issuer: _configuration["JWT:ValidIssuer"],
//                 audience: _configuration["JWT:ValidAudience"],
//                 expires: DateTime.Now.AddDays(7),
//                 claims: claims,
//                 signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
//             );
//         }
//     }
// }