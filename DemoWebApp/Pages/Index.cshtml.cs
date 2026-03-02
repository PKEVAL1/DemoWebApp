using DemoWebApp.Models;
using DemoWebApp.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IAppInfoService _appInfoService;
        private readonly ILogger<IndexModel> _logger;

        public AppInfoModel AppInfo { get; private set; } = default!;

        public IndexModel(IAppInfoService appInfoService, ILogger<IndexModel> logger)
        {
            _appInfoService = appInfoService;
            _logger         = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation("Index page visited at {Time}", DateTime.UtcNow);
            AppInfo = _appInfoService.GetAppInfo();
        }
    }
}
