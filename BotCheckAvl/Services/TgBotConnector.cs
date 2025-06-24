using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

public class TgBotConnector
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<TgBotConnector> _logger;

    public TgBotConnector(ITelegramBotClient botClient, ILogger<TgBotConnector> logger)
    {
        _botClient = botClient;
        _logger = logger;
    }

    public void Start(CancellationToken cancellationToken)
    {
        var receiverOptions = new ReceiverOptions();
        _botClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions, cancellationToken);
        _logger.LogInformation("TgBotConnector started receiving updates");
    }

    public Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken ct)
    {
        _logger.LogInformation("Received update of type {Type}", update.Type);
        // TODO: handle messages
        return Task.CompletedTask;
    }

    public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken ct)
    {
        _logger.LogError(exception, "Telegram bot error");
        return Task.CompletedTask;
    }
}
