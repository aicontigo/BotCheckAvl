using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;

public class TgBotService : BackgroundService
{
    private readonly ILogger<TgBotService> _logger;
    private readonly TgBotSettings _settings;
    private readonly TelegramBotClient _botClient;

    public TgBotService(IOptions<TgBotSettings> options, ILogger<TgBotService> logger)
    {
        _settings = options.Value;
        _logger = logger;
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
            // Placeholder for bot logic. In a real bot, you'd handle updates here.
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
        _logger.LogInformation("TgBotService is stopping");
    }
}
