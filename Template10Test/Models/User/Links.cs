using Newtonsoft.Json;

namespace Template10Test.Models.User
{

    public class Links
    {
        [JsonProperty("self")]
        public string self { get; set; }
    }
}
