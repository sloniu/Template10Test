using Newtonsoft.Json;

namespace Template10Test.Models.User
{
    public class Follow
    {
        [JsonProperty("created_at")]
        public string created_at { get; set; }

        [JsonProperty("_links")]
        public Links _links { get; set; }

        [JsonProperty("notifications")]
        public bool notifications { get; set; }

        [JsonProperty("channel")]
        public Channel channel { get; set; }
    }
}