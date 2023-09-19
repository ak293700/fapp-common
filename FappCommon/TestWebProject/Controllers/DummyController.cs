using Microsoft.AspNetCore.Mvc;

namespace TestWebProject.Controllers;

[Route("/dummy")]
[Produces("application/json")]
[Consumes("application/json")]
[ApiController]
public class DummyController : ControllerBase
{
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
}