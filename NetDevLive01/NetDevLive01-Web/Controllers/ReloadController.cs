using Microsoft.AspNetCore.Mvc;

namespace NetDevLive01.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ReloadController : ControllerBase
{
    private readonly IConfiguration _config;

    public ReloadController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet]
    public IActionResult Reload()
    {
        if (_config is IConfigurationRoot root is false)
        {
            return Problem(statusCode:500);
        }
        
        root.Reload();
        return Ok();
    }
}