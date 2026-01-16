using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KFC_EXD.Classes
{
    internal class Valgene
    {
        [JsonProperty("info")]
        public Info[] Info { get; set; }

        [JsonProperty("rarity")]
        public Dictionary<string, long> Rarity { get; set; }

        [JsonProperty("catalog")]
        public Catalog[] Catalog { get; set; }
    }

    public partial class Catalog
    {
        [JsonProperty("volume")]
        public long Volume { get; set; }

        [JsonProperty("items")]
        public Item[] Items { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("item_ids")]
        public long[] ItemIds { get; set; }
    }

    public partial class Info
    {
        [JsonProperty("valgene_name")]
        public string ValgeneName { get; set; }

        [JsonProperty("valgene_name_english")]
        public string ValgeneNameEnglish { get; set; }

        [JsonProperty("valgene_id")]
        public long ValgeneId { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }
    }
}
