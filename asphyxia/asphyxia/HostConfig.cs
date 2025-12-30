using System.Text.Json.Serialization;

namespace asphyxia
{
    public class HostConfig
    {
        [JsonPropertyName("host_url")]
        public required string HostUrl { get; set; }

        [JsonPropertyName("keepalive")]
        public required string KeepAlive { get; set; }
    }
}
