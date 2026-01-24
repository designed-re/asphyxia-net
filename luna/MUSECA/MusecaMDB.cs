using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MUSECA
{
    public class MusecaMDB
    {
            [JsonProperty("music")]
            public Music[] Music { get; set; }
            
    }
    public partial class Music
    {
        [JsonProperty("music_id")]
        public Limited MusicId { get; set; }

        [JsonProperty("music_type")]
        public Limited MusicType { get; set; }

        [JsonProperty("limited")]
        public Limited Limited { get; set; }
    }

    public partial class Limited
    {
        [JsonProperty("@content")]
        public int[] Content { get; set; }
    }
}
