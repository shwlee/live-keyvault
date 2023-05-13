using Microsoft.Extensions.Options;
using NetDevLive01.Web.configs;
using NetDevLive01.Web.Contracts;
using NetDevLive01.Web.Exceptions;
using System.Runtime.InteropServices;

namespace NetDevLive01.Web.Services;

public class AppSettingVault : IAppSettingVault
{
    private readonly IConfiguration _config;
    private readonly IOptionsMonitor<AppSettings> _appSettingsMonitor;
    private AppSettings _appSettings;
    
    public AppSettingVault(IConfiguration config, IOptionsMonitor<AppSettings> options)
    {
        _config = config;
        _appSettingsMonitor = options;
        _appSettings = _appSettingsMonitor.CurrentValue;
        _appSettingsMonitor.OnChange(appSettings =>
        {
            _appSettings = appSettings;
        });
    }

    public string GetSiteKey()
    {
        var siteKeyName = _appSettingsMonitor.CurrentValue.SiteKey;
        if (string.IsNullOrWhiteSpace(siteKeyName))
        {
            throw new SettingNotFoundException(nameof(AppSettings.SiteKey));
        }

        var siteKey = _config[siteKeyName];
        if (string.IsNullOrWhiteSpace(siteKey))
        {
            throw new SettingNotFoundException(nameof(AppSettings.SiteKey), "Can't get SiteKey Value.");
        }

        return siteKey;
    }

    public string GetSiteKeyName()
    {
        //var siteKeyName = _appSettingsMonitor.CurrentValue.SiteKey;
        var siteKeyName = _appSettings.SiteKey;
        if (string.IsNullOrWhiteSpace(siteKeyName))
        {
            throw new SettingNotFoundException(nameof(AppSettings.SiteKey));
        }
        
        return siteKeyName;
    }

    public string GetConnectionString([Optional] [DefaultParameterValue("DefaultConnection")] string name)
    {
        var keyName = _config.GetConnectionString(name);
        if (string.IsNullOrWhiteSpace(keyName))
        {
            throw new SettingNotFoundException(name);
        }

        var connectionString = _config[keyName];
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new SettingNotFoundException(name, "Can't get the connection string.");
        }

        return connectionString;
    }

    public string GetSection(string field)
    {
        var section = _config.GetSection(field);
        if (section is null)
        {
            throw new SectionNotFoundException(field);
        }

        var value = section.Value;
        return string.IsNullOrWhiteSpace(value) is false ? value : throw new SettingNotFoundException(field);
    }
}