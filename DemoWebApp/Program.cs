using DemoWebApp.Middleware;
using DemoWebApp.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// ─── Logging ───────────────────────────────────────────────────
builder.Logging
    .ClearProviders()
    .AddConsole()
    .AddDebug()
    .SetMinimumLevel(LogLevel.Information);

// ─── Services ──────────────────────────────────────────────────
builder.Services.AddRazorPages();
builder.Services.AddControllers();

// App-specific DI registration
builder.Services.AddScoped<IAppInfoService, AppInfoService>();

// Health Checks
builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy("Application is running."));

// ───────────────────────────────────────────────────────────────
var app = builder.Build();

// ─── Middleware Pipeline ────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Custom request logging middleware (must be before routing)
app.UseRequestLogging();

app.UseRouting();
app.UseAuthorization();

// ─── Endpoint Mapping ──────────────────────────────────────────
app.MapRazorPages();
app.MapControllers();
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResultStatusCodes =
    {
        [HealthStatus.Healthy]   = StatusCodes.Status200OK,
        [HealthStatus.Degraded]  = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    }
});

app.Run();
