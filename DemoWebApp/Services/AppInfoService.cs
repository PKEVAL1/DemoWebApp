using DemoWebApp.Models;

namespace DemoWebApp.Services
{
    /// <summary>
    /// Concrete implementation of IAppInfoService.
    /// Reads configuration values and provides structured app information.
    /// </summary>
    public class AppInfoService : IAppInfoService
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<AppInfoService> _logger;

        public AppInfoService(
            IConfiguration configuration,
            IWebHostEnvironment environment,
            ILogger<AppInfoService> logger)
        {
            _configuration = configuration;
            _environment = environment;
            _logger = logger;
        }

        public AppInfoModel GetAppInfo()
        {
            _logger.LogDebug("Building AppInfoModel");

            return new AppInfoModel
            {
                ApplicationName = GetApplicationName(),
                Version         = GetVersion(),
                Environment     = GetEnvironment(),
                ServerTime      = DateTime.UtcNow,
                MachineName     = System.Environment.MachineName
            };
        }

        public string GetApplicationName()
            => _configuration["ApplicationName"] ?? "DemoWebApp";

        public string GetVersion()
            => _configuration["Version"] ?? "1.0.0";

        public string GetEnvironment()
            => _environment.EnvironmentName;
    }
}
