using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

class Program
{
    public static async Task Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureLogging((context, logging) =>
            {
                var logLevel = context.Configuration["AppSettings:LogLevel"];
                logging.ClearProviders();
                logging.AddConsole();
                logging.SetMinimumLevel(ParseLogLevel(logLevel));
            })
            .Build();

        var config = host.Services.GetRequiredService<IConfiguration>();
        var logger = host.Services.GetRequiredService<ILogger<Program>>();
        var env = host.Services.GetRequiredService<IHostEnvironment>();

        var featureXEnabled = config.GetValue<bool>("AppSettings:FeatureXEnabled");

        logger.LogInformation($"Environment: {env.EnvironmentName}");
        logger.LogInformation($"FeatureXEnabled: {featureXEnabled}");

        if (featureXEnabled)
            logger.LogDebug("Feature X is enabled!");
        else
            logger.LogWarning("Feature X is disabled.");

        await host.RunAsync();
    }

    private static LogLevel ParseLogLevel(string? level) => level?.ToLower() switch
    {
        "trace" => LogLevel.Trace,
        "debug" => LogLevel.Debug,
        "information" => LogLevel.Information,
        "warning" => LogLevel.Warning,
        "error" => LogLevel.Error,
        "critical" => LogLevel.Critical,
        _ => LogLevel.Information
    };
}
