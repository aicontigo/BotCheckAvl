
namespace BotCheckAvl.Services
{
    public interface IBotDataService
    {
        Task AddPingResult(int serviceId, bool isUp, string responseCode, int responseTimeMs, CancellationToken ct);
        Task<MonitoredService> AddService(long telegramUserId, string url, CancellationToken ct);
        Task<User> AddUser(long telegramUserId, string userName, CancellationToken ct);
        Task<List<PingHistory>> GetHistory(int serviceId, CancellationToken ct);
        Task<List<MonitoredService>> GetServices(long telegramUserId, CancellationToken ct);
        Task<User> GetUser(long userTelegramId, CancellationToken ct);
    }
}