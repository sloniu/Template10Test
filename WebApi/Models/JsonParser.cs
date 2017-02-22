using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebApi.Models.SummonerByName;

namespace WebApi.Models
{
    public class JsonParser
    {
        public void Parser(string region, string summonerName)
        {
            
            using (var w = new HttpClient())
            {
                var settings = new JsonSerializerSettings();
                settings.Formatting = Formatting.Indented;
                settings.ContractResolver = new CustomNamesContractResolver("loseisimprove");
                var json = w.GetStringAsync($"https://na.api.pvp.net/api/lol/{region}/v1.4/summoner/by-name/{summonerName}?api_key=RGAPI-a231c9d7-98c8-436a-b77b-49bbec82462c").Result;
                var r = JsonConvert.DeserializeObject<RootObject>(json, settings);
                Debug.WriteLine(r.Summoner.Name);
            }
        }

        class CustomNamesContractResolver : DefaultContractResolver
        {
            private string SummonerName { get; }
            public CustomNamesContractResolver(string summonerName)
            {
                SummonerName = summonerName;
            }

            protected override IList<JsonProperty> CreateProperties(System.Type type, MemberSerialization memberSerialization)
            {
                // Let the base class create all the JsonProperties 
                // using the short names
                IList<JsonProperty> list = base.CreateProperties(type, memberSerialization);

                // Now inspect each property and replace the 
                // short name with the real property name
                foreach (JsonProperty prop in list)
                {
                    if (prop.UnderlyingName == "Summoner") //change this to your implementation!
                        prop.PropertyName = SummonerName;

                }

                return list;
            }
        }
    }
}