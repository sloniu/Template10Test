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
        public T Parser<T>(string region, string summonerName)
        {
            using (var w = new HttpClient())
            {
                var settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    ContractResolver = new CustomNamesContractResolver(summonerName.Replace(" ", string.Empty))
                };
                var json =
                    w.GetStringAsync(
                            $"https://na.api.pvp.net/api/lol/{region}/v1.4/summoner/by-name/{summonerName}?api_key=RGAPI-a231c9d7-98c8-436a-b77b-49bbec82462c")
                        .Result;
                return JsonConvert.DeserializeObject<T>(json, settings);
            }
        }

        public int GetSummonerId(string region, string summonerName)
        {
            return Parser<RootObject>(region, summonerName).Summoner.Id;
        }

        public long GetMatchId(string region, string summonerName)
        {
            using (var w = new HttpClient())
            {
                var json =
                    w.GetStringAsync(
                            $"https://na.api.pvp.net/api/lol/{region}/v2.2/matchlist/by-summoner/{GetSummonerId(region, summonerName)}?api_key=RGAPI-a231c9d7-98c8-436a-b77b-49bbec82462c")
                        .Result;
                return JsonConvert.DeserializeObject<MatchListBySummonerId.RootObject>(json).Matches.First().MatchId;
            }
        }

        public MatchById.RootObject GetMatch(string region, string summonerName)
        {
            using (var w = new HttpClient())
            {
                var json =
                    w.GetStringAsync(
                            $"https://na.api.pvp.net/api/lol/{region}/v2.2/match/{GetMatchId(region, summonerName)}?api_key=RGAPI-a231c9d7-98c8-436a-b77b-49bbec82462c")
                        .Result;

                return JsonConvert.DeserializeObject<MatchById.RootObject>(json);
            }
        }

        public MatchById.Participant GetPlayer(string region, string summonerName)
        {
            var r = GetMatch(region, summonerName);
            var playerId = r.ParticipantIdentities.Find(x => x.Player.SummonerName.ToLower() == summonerName);
            return r.Participants.Find(y => y.ParticipantId == playerId.ParticipantId);
        }

        public MatchById.Stats GetStats(string region, string summonerName)
        {
            return GetPlayer(region, summonerName).Stats;
        }

        public int GetChampionId(string region, string summonerName)
        {
            return GetPlayer(region, summonerName).ChampionId;
        }

        public List<MatchById.Mastery> GetMasteries(string region, string summonerName)
        {
            return GetPlayer(region, summonerName).Masteries;
        }

        public List<MatchById.Rune> GetRunes(string region, string summonerName)
        {
            return GetPlayer(region, summonerName).Runes;
        }

        private class CustomNamesContractResolver : DefaultContractResolver
        {
            private string SummonerName { get; }

            public CustomNamesContractResolver(string summonerName)
            {
                SummonerName = summonerName;
            }

            protected override IList<JsonProperty> CreateProperties(System.Type type,
                MemberSerialization memberSerialization)
            {
                var list = base.CreateProperties(type, memberSerialization);

                foreach (var prop in list)
                    if (prop.UnderlyingName == "Summoner")
                        prop.PropertyName = SummonerName;
                return list;
            }
        }
    }
}