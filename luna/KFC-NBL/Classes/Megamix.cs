using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KFC_NBL.Classes
{
    internal class Megamix
    {
        [JsonProperty("megamix1")]
        public long[] Megamix1 { get; set; }

        [JsonProperty("megamix2")]
        public long[] Megamix2 { get; set; }

        [JsonProperty("megamix3")]
        public long[] Megamix3 { get; set; }

        [JsonProperty("megamix4")]
        public long[] Megamix4 { get; set; }
    }
}
