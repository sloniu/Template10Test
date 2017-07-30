using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Twitch10WcfService.DAL.Entities;
using Twitch10WcfService.Models.SummonerByName;
using Formatting = System.Xml.Formatting;

namespace Twitch10WcfService.Models
{
    public class JsonParser
    {
        private const string ApiKey = "RGAPI-164c3fb2-3e40-474b-bf7e-18e864ed4a28";

        public T Parser<T>(string region, string summonerName)
        {
            using (var w = new HttpClient())
            {
                var settings = new JsonSerializerSettings
                {
                    Formatting = (Newtonsoft.Json.Formatting) Formatting.Indented,
                    ContractResolver = new CustomNamesContractResolver(summonerName.Replace(" ", string.Empty))
                };
                var json =
                    w.GetStringAsync(
                            $"https://{region}.api.riotgames.com/lol/summoner/v3/summoners/by-name/{summonerName}?api_key={ApiKey}")
                        .Result;
                return JsonConvert.DeserializeObject<T>(json, settings);
            }
        }

        public int GetSummonerId(string region, string summonerName)
        {
            return Parser<RootObject>(region, summonerName).id;
        }

        public int GetAccountId(string region, string summonerName)
        {
            return Parser<RootObject>(region, summonerName).accountId;
        }

        public long GetMatchId(string region, string summonerName)
        {
            using (var w = new HttpClient())
            {
                var json =
                    w.GetStringAsync(
                        $"https://{region}.api.riotgames.com/lol/match/v3/matchlists/by-account/{GetAccountId(region,summonerName)}/recent?api_key={ApiKey}").Result;
                var result =
                    Convert.ToInt64(
                        JsonConvert.DeserializeObject<MatchListBySummonerId.RootObject>(json).matches.First().gameId);
                return result;
            }
        }

        public MatchById.RootObject GetMatch(string region, string summonerName)
        {
            using (var w = new HttpClient())
            {
                var json =
                    w.GetStringAsync(
                            $"https://{region}.api.riotgames.com/lol/match/v3/matches/{GetMatchId(region, summonerName)}?api_key={ApiKey}")
                        .Result;

                return JsonConvert.DeserializeObject<MatchById.RootObject>(json);
            }
        }

        public static MatchById.RootObject GetMatchById(string region, long matchId)
        {
            using (var w = new HttpClient())
            {
                var json =
                    w.GetStringAsync(
                            $"https://{region}.api.riotgames.com/lol/match/v3/matches/{matchId}?api_key={ApiKey}")
                        .Result;
                return JsonConvert.DeserializeObject<MatchById.RootObject>(json);
            }
        }

        public MatchById.Participant GetPlayer(string region, string summonerName)
        {
            var r = GetMatch(region, summonerName);
            var playerId = r.participantIdentities.Find(x => x.player.summonerName.ToLower() == summonerName);
            return r.participants.Find(y => y.participantId == playerId.participantId);
        }

        public MatchById.Stats GetStats(string region, string summonerName)
        {
            return GetPlayer(region, summonerName).stats;
        }

        public int GetChampionId(string region, string summonerName)
        {
            return GetPlayer(region, summonerName).championId;
        }

        public List<MatchById.Mastery> GetMasteries(string region, string summonerName)
        {
            return GetPlayer(region, summonerName).masteries;
        }

        public List<MatchById.Rune> GetRunes(string region, string summonerName)
        {
            return GetPlayer(region, summonerName).runes;
        }

        public Build GetLastBuild(string region, string summonerName)
        {
            var match = GetMatch(region, summonerName);
            var player = GetPlayer(region, summonerName);
            var stats = GetStats(region, summonerName);

            var build = new Build()
            {
                BuildId = 0,
                ChampionId = player.championId,
                Item1Id = stats.item1,
                Item2Id = stats.item2,
                Item3Id = stats.item3,
                Item4Id = stats.item4,
                Item5Id = stats.item5,
                Item6Id = stats.item6,
                MatchId = match.gameId,
                PlayerName = summonerName,
                Spell1Id = player.spell1Id,
                Spell2Id = player.spell2Id
            };
            return build;
        }

        public static Build GetBuild(string region, string summonerName, long matchid)
        {
            var match = GetMatchById(region, matchid);
            var playerId = match.participantIdentities.Find(x => x.player.summonerName.ToLower() == summonerName);
            var player = match.participants.Find(y => y.participantId == playerId.participantId);
            var stats = player.stats;

            var build = new Build()
            {
                BuildId = 0,
                ChampionId = player.championId,
                Item1Id = stats.item1,
                Item2Id = stats.item2,
                Item3Id = stats.item3,
                Item4Id = stats.item4,
                Item5Id = stats.item5,
                Item6Id = stats.item6,
                MatchId = match.gameId,
                PlayerName = summonerName,
                Spell1Id = player.spell1Id,
                Spell2Id = player.spell2Id
            };
            return build;
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