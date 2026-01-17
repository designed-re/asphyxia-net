using System.Text.Json.Serialization;

namespace luna
{
    public class HostConfig
    {
        [JsonPropertyName("host_url")]
        public static string HostUrl { get; set; }

        [JsonPropertyName("keepalive")]
        public static string KeepAlive { get; set; }


        [JsonPropertyName("mariadb_connstr")]
        public static string MariaDbConnectString { get; set; }
    }
}
