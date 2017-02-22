using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WebApi.Models.SummonerByName
{
    public class Loseisimprove
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("profileIconId")]
        public int profileIconId { get; set; }
        [JsonProperty("revisionDate")]
        public long revisionDate { get; set; }
        [JsonProperty("summonerLevel")]
        public int summonerLevel { get; set; }
    }
}