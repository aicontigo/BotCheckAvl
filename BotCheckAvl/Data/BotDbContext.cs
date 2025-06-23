using Microsoft.EntityFrameworkCore;
using System.Reflection;

public class BotDbContext : DbContext
{
    public BotDbContext(DbContextOptions<BotDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<MonitoredService> MonitoredServices => Set<MonitoredService>();
    public DbSet<PingHistory> PingHistories => Set<PingHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.TelegramUserId)
            .IsUnique();
    }
}
