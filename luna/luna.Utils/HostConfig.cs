using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;

namespace luna.Utils
{
    public class HostConfig
    {
        [ConfigurationKeyName("host_url")]
        public string HostUrl { get; set; }

        [ConfigurationKeyName("keepalive")]
        public string KeepAlive { get; set; }


        [ConfigurationKeyName("mariadb_connstr")]
        public string MariaDbConnectString { get; set; }

        [ConfigurationKeyName("enforce_pcbid")]
        public bool EnforcePCBId { get; set; }

        [ConfigurationKeyName("arena_open")]
        public bool ArenaOpen { get; set; }


        [ConfigurationKeyName("arena_session")]
        public int ArenaSession { get; set; }


    }
}
