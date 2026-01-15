using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KFC_EXD.Classes
{
    internal class Course
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }

        [JsonProperty("isNew")]
        public long IsNew { get; set; }

        [JsonProperty("hasGod", NullValueHandling = NullValueHandling.Ignore)]
        public long? HasGod { get; set; }

        [JsonProperty("courses")]
        public CourseElement[] Courses { get; set; }
    }

    public partial class CourseElement
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("level")]
        public long Level { get; set; }

        [JsonProperty("nameID")]
        public long NameId { get; set; }

        [JsonProperty("assist")]
        public long Assist { get; set; }

        [JsonProperty("tracks")]
        public Track[] Tracks { get; set; }
    }

    public partial class Track
    {
        [JsonProperty("no")]
        public long No { get; set; }

        [JsonProperty("mid")]
        public long Mid { get; set; }

        [JsonProperty("mty")]
        public long Mty { get; set; }
    }
}
