using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BotCheckAvl.Services;
using Telegram.Bot;

public class TgBotService : BackgroundService
{
    private readonly ILogger<TgBotService> _logger;
    private readonly TgBotSettings _settings;
    private readonly TelegramBotClient _botClient;
    private readonly MonitoringService _monitoringService;

    public TgBotService(
        IOptions<TgBotSettings> options,
        ILogger<TgBotService> logger,
        MonitoringService monitoringService)
    {
        _settings = options.Value;
        _logger = logger;
        _monitoringService = monitoringService;
        try
        {
            _botClient = new TelegramBotClient(_settings.Token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to initialize TelegramBotClient with provided token.");
            throw;
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("TgBotService is starting");
        while (!stoppingToken.IsCancellationRequested)
        {
            // Demo database access
            var services = await _monitoringService.GetServicesAsync(0);
            _logger.LogDebug($"Loaded {services.Count} services from DB");
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
        _logger.LogInformation("TgBotService is stopping");
    }
}
