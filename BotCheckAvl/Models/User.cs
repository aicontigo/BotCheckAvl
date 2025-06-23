using System.Collections.Generic;

public class User
{
    public int Id { get; set; }
    public long TelegramUserId { get; set; }
    public string? Username { get; set; }

    public ICollection<MonitoredService> Services { get; set; } = new List<MonitoredService>();
}
