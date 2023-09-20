using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace TestWebProject.Controllers;

[Route("/dummy")]
[Produces("application/json")]
[Consumes("application/json")]
[ApiController]
public class DummyController : ControllerBase
{
    public const string AuthToken = "1q2w3e4r5t6y7u8i9o0p";
    private readonly ILogger<DummyController> _logger;

    public DummyController(ILogger<DummyController> logger)
    {
        _logger = logger;
    }

    [HttpGet("hello_world")]
    public Task<ActionResult<string>> GetHelloWorld()
    {
        return Task.FromResult<ActionResult<string>>(Ok("Hello World!"));
    }

    [HttpGet("fail")]
    public Task<ActionResult<string>> GetFail()
    {
        return Task.FromResult<ActionResult<string>>(BadRequest("Fail!"));
    }

    [HttpGet("token")]
    public Task<ActionResult<string>> GetToken()
    {
        Claim[] claims =
        {
            new(ClaimTypes.NameIdentifier, "1"),
            new(ClaimTypes.Name, "Test"),
        };
        SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(AuthToken));

        // Credentials
        SigningCredentials cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        JwtSecurityToken token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddYears(1),
            signingCredentials: cred
        );

        var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
        return Task.FromResult<ActionResult<string>>(Ok(tokenAsString));
    }

    [HttpGet("fail_authorize")]
    [Authorize]
    public Task<ActionResult<string>> GetFailAuthorize()
    {
        return Task.FromResult<ActionResult<string>>(BadRequest("Fail!"));
    }

    [HttpGet("log")]
    public Task<IActionResult> Log()
    {
        _logger.LogInformation("Test log");

        return Task.FromResult<IActionResult>(Ok());
    }
}