using System;

public class PingHistory
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public bool IsUp { get; set; }

    public int MonitoredServiceId { get; set; }
    public MonitoredService? MonitoredService { get; set; }
}
