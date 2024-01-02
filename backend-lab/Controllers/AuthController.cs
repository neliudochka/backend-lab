using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend_lab.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models;

namespace backend_lab.Controllers;


[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration; // Inject IConfiguration
    private readonly UserService _userService;

    public AuthController(IConfiguration configuration, UserService userService)
    {
        _configuration = configuration;
        _userService = userService;
    }
    
    //JWT token
    private string GenerateJwtToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("KEY"));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    //login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]User user)
    {
        try
        {
            var thisUser = (await _userService.GetUsers())
                .FirstOrDefault(i => i.Name == user.Name);
            if (thisUser is null)
                return BadRequest();
            if (!Validator.Verify(user.Password,thisUser.Password))
                return BadRequest();
            return Ok(GenerateJwtToken());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    //register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]User user)
    {
        try
        {
            user.Id = Guid.NewGuid();
            user.Password = Validator.Encrypt(user.Password);
            await _userService.AddUser(user);
            return Ok(user.Id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }
    
}

