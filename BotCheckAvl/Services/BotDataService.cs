using Microsoft.EntityFrameworkCore;

namespace BotCheckAvl.Services
{
    public class BotDataService : IBotDataService
    {
        private readonly BotDbContext _db;

        public BotDataService(BotDbContext db)
        {
            _db = db;
        }

        public async Task<List<MonitoredService>> GetServices(long telegramUserId, CancellationToken ct)
        {
            if (telegramUserId == 0)
                throw new ArgumentException("Telegram ID should have a value");

            return await _db.MonitoredServices
                .Include(m => m.User)
                .Where(m => m.User.TelegramUserId == telegramUserId)
                .ToListAsync(ct);
        }

        public async Task<MonitoredService> AddService(long telegramUserId, string url, CancellationToken ct)
        {
            if (telegramUserId == 0)
                throw new ArgumentException("Telegram ID should have a value");

            var user = await _db.Users.FirstOrDefaultAsync(u => u.TelegramUserId == telegramUserId, ct);
            if (user == null)
            {
                user = new User { TelegramUserId = telegramUserId };
                _db.Users.Add(user);
                await _db.SaveChangesAsync(ct);
            }

            var svc = new MonitoredService { Url = url, UserId = user.Id, IsEnabled = true, CreatedAt = DateTime.UtcNow };
            _db.MonitoredServices.Add(svc);
            await _db.SaveChangesAsync(ct);
            return svc;
        }

        public async Task AddPingResult(int serviceId, bool isUp, string responseCode, int responseTimeMs, CancellationToken ct)
        {
            if (serviceId == 0)
                throw new ArgumentException("Service ID should have a value");

            var ping = new PingHistory
            {
                MonitoredServiceId = serviceId,
                Timestamp = DateTime.UtcNow,
                ResponseCode = responseCode,
                ResponseTimeMs = responseTimeMs,
                IsUp = isUp
            };
            _db.PingHistories.Add(ping);
            await _db.SaveChangesAsync(ct);
        }

        public async Task<List<PingHistory>> GetHistory(int serviceId, CancellationToken ct)
        {
            if (serviceId == 0)
                throw new ArgumentException("Service ID should have a value");

            return await _db.PingHistories
                .Where(p => p.MonitoredServiceId == serviceId)
                .OrderByDescending(p => p.Timestamp)
                .ToListAsync(ct);
        }

        public async Task<User> AddUser(long telegramUserId, string userName, CancellationToken ct)
        {
            if (telegramUserId == 0)
                throw new ArgumentNullException("Telegram ID should have a value");

            var user = new User
            {
                TelegramUserId = telegramUserId,
                Username = userName,
                CreatedAt = DateTime.UtcNow
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync(ct);
            return user;
        }

        public async Task<User> GetUser(long userTelegramId, CancellationToken ct)
        {
            if (userTelegramId == 0)
                throw new ArgumentNullException("Telegram ID should have a value");

            return await _db.Users.FirstOrDefaultAsync(u => u.TelegramUserId == userTelegramId, ct) ?? throw new Exception("User not found");
        }
    }
}
