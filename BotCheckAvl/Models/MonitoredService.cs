using System.Collections.Generic;

public class MonitoredService
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = true;
    public int TimeoutMs { get; set; }
    public int MaxRetryes { get; set; }
    public int ResponseTimeWarningMs { get; set; }
    public bool? IsLive { get; set; }


    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }

    public ICollection<PingHistory> History { get; set; } = new List<PingHistory>();
}
