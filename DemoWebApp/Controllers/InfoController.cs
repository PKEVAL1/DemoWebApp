using DemoWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebApp.Controllers
{
    /// <summary>
    /// Minimal API-style controller exposing /api/info as a JSON endpoint.
    /// </summary>
    [ApiController]
    [Route("api")]
    public class InfoController : ControllerBase
    {
        private readonly IAppInfoService _appInfoService;
        private readonly ILogger<InfoController> _logger;

        public InfoController(IAppInfoService appInfoService, ILogger<InfoController> logger)
        {
            _appInfoService = appInfoService;
            _logger         = logger;
        }

        /// <summary>
        /// GET /api/info — Returns application metadata as JSON.
        /// </summary>
        [HttpGet("info")]
        [Produces("application/json")]
        public IActionResult GetInfo()
        {
            _logger.LogInformation("API /api/info called");
            var info = _appInfoService.GetAppInfo();
            //Response Add
            return Ok(new
            {
                applicationName = info.ApplicationName,
                version         = info.Version,
                environment     = info.Environment,
                serverTime      = info.ServerTime.ToString("o"),
                machineName     = info.MachineName
            });
        }
    }
}
