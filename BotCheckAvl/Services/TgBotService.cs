using BotCheckAvl.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;

public class TgBotService : BackgroundService
{
    private readonly ILogger<TgBotService> _logger;
    private readonly TgBotSettings _settings;
    private readonly TelegramBotClient _botClient;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly TgBotConnector _connector;

    private IBotDataService _monitoringService;

    public TgBotService(
        IOptions<TgBotSettings> options,
        ILogger<TgBotService> logger,
        IServiceScopeFactory scopeFactory,
        ILogger<TgBotConnector> connectorLogger)
    {
        _settings = options.Value;
        _logger = logger;
        _scopeFactory = scopeFactory;
        try
        {
            _botClient = new TelegramBotClient(_settings.Token);
            _connector = new TgBotConnector(_botClient, connectorLogger);
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

        _connector.Start(stoppingToken);

        //TEMP!!!
        using var scope = _scopeFactory.CreateScope();
        _monitoringService = scope.ServiceProvider.GetRequiredService<IBotDataService>();

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // Demo database access
                var services = await _monitoringService.GetServices(500, stoppingToken);
                _logger.LogDebug($"Loaded {services.Count} services from DB");
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Cannot get the service list from DB: {exception}", ex);
            }
            finally
            {
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
        _logger.LogInformation("TgBotService is stopping");
    }
}
