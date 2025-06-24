using BotCheckAvl.Services;
using BotCheckAvl.Services.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

public class TgBotService : BackgroundService
{
    private readonly ILogger<TgBotService> _logger;
    private readonly TgBotSettings _settings;
    private readonly TelegramBotClient _botClient;
    private readonly IServiceScopeFactory _scopeFactory;

    private IBotDataService _monitoringService;

    public TgBotService(
        IOptions<TgBotSettings> options,
        ILogger<TgBotService> logger,
        IServiceScopeFactory scopeFactory)
    {
        _settings = options.Value;
        _logger = logger;
        _scopeFactory = scopeFactory;
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

        StartBot(stoppingToken);

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

    public void StartBot(CancellationToken cancellationToken)
    {
        var receiverOptions = new ReceiverOptions();
        _botClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions, cancellationToken);
        _logger.LogInformation("TgBotConnector started receiving updates");
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken ct)
    {
        _logger.LogInformation("Received update of type {Type}", update.Type);
        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            _logger.LogInformation($"Received message '{update.Message.Text}' from {update.Message.Chat.Username}");

            var command = update.Message!.Text?.Trim().ToLowerInvariant() switch
            {
                "add service" => Commands.BotCommand.AddService,
                "disable service" => Commands.BotCommand.DisableService,
                "enable service" => Commands.BotCommand.EnableService,
                "delete service" => Commands.BotCommand.DeleteService,
                "show service" => Commands.BotCommand.ShowService,
                "show all" => Commands.BotCommand.ShowAll,
                "check service" => Commands.BotCommand.CheckService,
                _ => (Commands.BotCommand?)null
            };

            if (command.HasValue)
            {
                var handler = Commands.CommandHandlerFactory.Create(command.Value);
                var result = await handler.HandleCommand(ct);
                var response = result.IsSuccess
                    ? result.SuccessMessage ?? "Command executed"
                    : (result.Errors.Count > 0 ? string.Join("; ", result.Errors) : "Command failed");
                await botClient.SendTextMessageAsync(update.Message.Chat, response, cancellationToken: ct);
            }
            else
            {
                await botClient.SendTextMessageAsync(update.Message.Chat, "Unknown command", cancellationToken: ct);
            }
        }
        // TODO: handle other update types
        return;
    }

    public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken ct)
    {
        _logger.LogError(exception, "Telegram bot error");
        return Task.CompletedTask;
    }
}
