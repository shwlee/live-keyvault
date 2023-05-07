using Microsoft.AspNetCore.Mvc;
using NetDevLive01.Web.Contracts;

namespace NetDevLive01.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class SecretController : ControllerBase
{
    private readonly IAppSettingVault _appSettingVault;

    public SecretController(IAppSettingVault appSettingVault)
    {
        _appSettingVault = appSettingVault;
    }

    [HttpGet("appsettings/sitekey")]
    public IActionResult GetAppSettingKey() => Ok(_appSettingVault.GetSection("AppSettings:SiteKey"));

    [HttpGet("site")]
    public IActionResult GetSiteKeySecret() => Ok(_appSettingVault.GetSiteKey());

    [HttpGet("connection")]
    public IActionResult GetConnectionString() => Ok(_appSettingVault.GetConnectionString());
}