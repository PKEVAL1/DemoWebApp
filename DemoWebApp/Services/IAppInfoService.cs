using DemoWebApp.Models;

namespace DemoWebApp.Services
{
    /// <summary>
    /// Provides application information to pages and controllers.
    /// </summary>
    public interface IAppInfoService
    {
        AppInfoModel GetAppInfo();
        string GetApplicationName();
        string GetVersion();
        string GetEnvironment();
    }
}
