using Newtonsoft.Json;

namespace Twitch10WcfService.Models.SummonerByName
{
    public class RootObject
    {
        [JsonProperty("Summoner")]
        public Summoner Summoner { get; set; }
    }
}