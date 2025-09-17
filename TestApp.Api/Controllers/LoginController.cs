using Microsoft.AspNetCore.Mvc;
using TestApp.Core.Dtos;
using TestApp.Infrastructure.Services;

namespace TestApp.Api.Controllers;

public class LoginController : ControllerBase
{
    private readonly JwtService _jwtService;

    public LoginController(JwtService jwtService)
    {
        _jwtService = jwtService;
    }
    
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        if (model.Username == "admin" && model.Password == "admin")
        {
            var token = _jwtService.GenerateToken(model.Username);
            return Ok(new { token });
        }

        return Unauthorized();
    }
}