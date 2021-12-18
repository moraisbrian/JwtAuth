using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using JwtAuth.Models;

namespace JwtAuth.Controllers;

[ApiController]
[Route("api")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Authenticate([FromBody] UserLoginModel login)
    {
        try
        {
            var user = FindUser(login);

            if (user != null)
            {
                var token = GenerateToken(user);
                return Ok(token);
            }

            return NotFound("Usuário ou senha inválidos");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    private string GenerateToken(UserModel user)
    {
        var secret = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Auth:Secret"]));
        var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserName!),
            new Claim(ClaimTypes.Email, user.EmailAddress!),
            new Claim(ClaimTypes.GivenName, user.GivenName!),
            new Claim(ClaimTypes.Surname, user.Surname!),
            new Claim(ClaimTypes.Role, user.Role!)
        };

        var token = new JwtSecurityToken(
            _configuration["Auth:Issuer"],
            _configuration["Auth:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private UserModel? FindUser(UserLoginModel user)
    {
        return DatabaseMock.GetUsers().FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
    }
}
