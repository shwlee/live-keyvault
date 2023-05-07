namespace NetDevLive01.Web.Exceptions;

public class SettingNotFoundException : Exception
{
    public string SettingName { get; }

    public SettingNotFoundException(string settingName, string? message = null) 
        : this(settingName, message, null)
    {
    }

    public SettingNotFoundException(string settingName, string? message, Exception? inner)
        : base(message, inner)
    {
        SettingName = settingName;
    }
}