using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetDevLive01_Web.configs;

namespace NetDevLive01_Web.Controllers;

[ApiController]
[Route("[controller]")]
public class SecretController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly AppSettings _appSettings;

    public SecretController(IConfiguration config, IOptions<AppSettings> options)
    {
        _config = config;
        _appSettings = options.Value;
    }

    [HttpGet("site")]
    public IActionResult GetSiteKeySecret()
    {
        var keyName = _appSettings.SiteKey;
        if (string.IsNullOrWhiteSpace(keyName))
        {
            return NotFound();
        }
        
        var siteKey = _config[keyName];
        return Ok(siteKey);
    }
}