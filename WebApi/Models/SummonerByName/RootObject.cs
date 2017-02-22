using System.Collections.Generic;
using Newtonsoft.Json;

namespace WebApi.Models.SummonerByName
{
    public class RootObject
    {
        public List<Summoner> Summoner { get; set; }
    }
}