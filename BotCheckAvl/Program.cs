using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

class Program
{
    // Entry point with async Task Main
    public static async Task Main(string[] args)
    {
        // Get environment name from environment variable (default: Production)
        var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
            ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            ?? "Production";

        // Build configuration
        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        var config = configBuilder.Build();

        // GetValue extension method is available for IConfigurationSection, not IConfiguration directly
        var logLevel = config["AppSettings:LogLevel"];
        var featureXEnabledRaw = config["AppSettings:FeatureXEnabled"];
        var featureXEnabled = bool.TryParse(featureXEnabledRaw, out var parsed) && parsed;

        // Setup logger
        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddConsole()
                .SetMinimumLevel(ParseLogLevel(logLevel));
        });
        var logger = loggerFactory.CreateLogger<Program>();

        logger.LogInformation($"Environment: {environment}");
        logger.LogInformation($"LogLevel: {logLevel}");
        logger.LogInformation($"FeatureXEnabled: {featureXEnabled}");

        if (featureXEnabled)
            logger.LogDebug("Feature X is enabled!");
        else
            logger.LogWarning("Feature X is disabled.");

        await Task.CompletedTask;
    }

    // Helper to parse log level from string
    private static LogLevel ParseLogLevel(string? level)
    {
        return level?.ToLower() switch
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
}
