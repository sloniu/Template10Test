using Newtonsoft.Json;

namespace Template10Test.Models.User
{
    public class Links3
    {
        [JsonProperty("self")]
        public string self { get; set; }

        [JsonProperty("next")]
        public string next { get; set; }
    }
}