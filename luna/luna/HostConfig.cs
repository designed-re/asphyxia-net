using System.Text.Json.Serialization;

namespace luna
{
    public class HostConfig
    {
        [JsonPropertyName("host_url")]
        public string HostUrl { get; set; }

        [JsonPropertyName("keepalive")]
        public string KeepAlive { get; set; }


        [JsonPropertyName("mariadb_connstr")]
        public string MariaDbConnectString { get; set; }

        [JsonPropertyName("enforce_pcbid")]
        public bool EnforcePCBId { get; set; }
    }
}
