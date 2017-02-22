using Newtonsoft.Json;

namespace WebApi.Models.SummonerByName
{
    public class RootObject
    {
        [JsonProperty("Loseisimprove")]
        public Loseisimprove loseisimprove { get; set; }
    }
}