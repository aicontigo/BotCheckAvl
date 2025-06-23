using System.Collections.Generic;

public class MonitoredService
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = true;

    public int UserId { get; set; }
    public User? User { get; set; }

    public ICollection<PingHistory> History { get; set; } = new List<PingHistory>();
}
