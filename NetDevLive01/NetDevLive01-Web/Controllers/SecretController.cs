using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetDevLive01.Web.configs;

namespace NetDevLive01.Web.Controllers;

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

    [HttpGet("appsettings/sitekey")]
    public IActionResult GetAppSettingKey()
    {
        var keyName = _appSettings.SiteKey;
        if (string.IsNullOrWhiteSpace(keyName))
        {
            return NotFound();
        }

        var section = _config.GetSection("AppSettings:SiteKey");

        return Ok(section);
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

    [HttpGet("connection")]
    public IActionResult GetConnectionString()
    {
        var connectionStringKey = _config.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionStringKey))
        {
            return NotFound();
        }

        var siteKey = _config[connectionStringKey];
        return Ok(siteKey);
    }
}