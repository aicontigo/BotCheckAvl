using System;

public class PingHistory
{
    public long Id { get; set; }
    public DateTime Timestamp { get; set; }
    public bool IsUp { get; set; }
    public int? ResponseTimeMs { get; set; }
    public string? ResponseCode { get; set; }

    public int MonitoredServiceId { get; set; }
    public MonitoredService? MonitoredService { get; set; }
}
