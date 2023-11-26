using System.Text.Json.Serialization;

namespace backend_lab.Entities;

public class HealthCheckReturnInfo
{
    
    public DateTime Time { get; set; } = DateTime.Now;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ServerStatus Status { get; set; }
    
    public HealthCheckReturnInfo(ServerStatus status)
    {
        Status = status;
    }

}

public enum ServerStatus
{
    Healthy, NotHealthy
}; 