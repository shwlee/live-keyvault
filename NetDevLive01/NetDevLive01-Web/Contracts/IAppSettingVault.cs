using System.Runtime.InteropServices;

namespace NetDevLive01.Web.Contracts;

public interface IAppSettingVault
{
    string GetSiteKey();

    string GetSiteKeyName();

    string GetConnectionString([Optional] [DefaultParameterValue("DefaultConnection")] string name);

    string GetSection(string field);
}