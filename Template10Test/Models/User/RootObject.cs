using System.Collections.Generic;
using Newtonsoft.Json;

namespace Template10Test.Models.User
{
    public class RootObject
    {
        [JsonProperty("follows")]
        public List<Follow> follows { get; set; }

        [JsonProperty("_total")]
        public int _total { get; set; }

        [JsonProperty("_links")]
        public Links3 _links { get; set; }
    }
}