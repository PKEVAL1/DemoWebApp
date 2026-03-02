namespace DemoWebApp.Models
{
    /// <summary>
    /// Represents application information returned by the /api/info endpoint.
    /// </summary>
    public class AppInfoModel
    {
        public string ApplicationName { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string Environment { get; set; } = string.Empty;
        public DateTime ServerTime { get; set; }
        public string MachineName { get; set; } = string.Empty;
    }
}
