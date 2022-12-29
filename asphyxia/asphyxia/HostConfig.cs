using System.Text.Json.Serialization;

namespace asphyxia
{
    public class HostConfig
    {
        [JsonPropertyName("host_url")]
        public string HostUrl { get; set; }

        [JsonPropertyName("keepalive")]
        public string KeepAlive { get; set; }
    }
}
