using Microsoft.EntityFrameworkCore;

namespace BotCheckAvl.Services
{
    public class MonitoringService
    {
        private readonly BotDbContext _db;

        public MonitoringService(BotDbContext db)
        {
            _db = db;
        }

        public async Task<List<MonitoredService>> GetServicesAsync(long telegramUserId)
        {
            return await _db.MonitoredServices
                .Include(m => m.User)
                .Where(m => m.User.TelegramUserId == telegramUserId)
                .ToListAsync();
        }

        public async Task<MonitoredService> AddServiceAsync(long telegramUserId, string url)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.TelegramUserId == telegramUserId);
            if (user == null)
            {
                user = new User { TelegramUserId = telegramUserId };
                _db.Users.Add(user);
                await _db.SaveChangesAsync();
            }

            var svc = new MonitoredService { Url = url, UserId = user.Id, IsEnabled = true };
            _db.MonitoredServices.Add(svc);
            await _db.SaveChangesAsync();
            return svc;
        }

        public async Task AddPingResultAsync(int serviceId, bool isUp)
        {
            var ping = new PingHistory
            {
                MonitoredServiceId = serviceId,
                Timestamp = DateTime.UtcNow,
                IsUp = isUp
            };
            _db.PingHistories.Add(ping);
            await _db.SaveChangesAsync();
        }

        public async Task<List<PingHistory>> GetHistoryAsync(int serviceId)
        {
            return await _db.PingHistories
                .Where(p => p.MonitoredServiceId == serviceId)
                .OrderByDescending(p => p.Timestamp)
                .ToListAsync();
        }
    }
}
