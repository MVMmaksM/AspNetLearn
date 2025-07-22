using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthTest.Controllers;

[Authorize]
[Route("api/test")]
[ApiController]
public class SecuredController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetData()
    {
        return Ok("Teststsst");
    }
}