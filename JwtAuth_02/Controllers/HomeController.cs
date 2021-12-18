namespace JwtAuth.Controllers;

[Authorize]
[ApiController]
[Route("api")]
public class HomeController : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    public IActionResult GetResponse()
    {
        return Ok();
    }
}
