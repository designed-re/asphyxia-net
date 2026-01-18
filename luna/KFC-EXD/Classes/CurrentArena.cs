using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KFC_EXD.Classes
{// rule: 0 score 1 point 2 vote
    // rank_match_target: 0 arena 1 single 2 mega
    public class CurrentArena
    {
        [JsonProperty("season")]
        public int Season { get; set; }

        [JsonProperty("rule")]
        public short Rule { get; set; }

        [JsonProperty("rank_match_target")]
        public short RankMatchTarget { get; set; }

        [JsonProperty("time_start")]
        public DateTimeOffset TimeStart { get; set; }

        [JsonProperty("time_end")]
        public DateTimeOffset TimeEnd { get; set; }

        [JsonProperty("shop_start")]
        public DateTimeOffset ShopStart { get; set; }

        [JsonProperty("shop_end")]
        public DateTimeOffset ShopEnd { get; set; }
    }
}
