﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using WebApi.Models.SummonerByName;

namespace WebApi.Models
{
    public class JsonParser
    {
        public void Parser(string region, string summonerName)
        {
            using (var w = new HttpClient())
            {
                var json = w.GetStringAsync($"https://na.api.pvp.net/api/lol/{region}/v1.4/summoner/by-name/{summonerName}?api_key=RGAPI-a231c9d7-98c8-436a-b77b-49bbec82462c").Result;
                var r = JsonConvert.DeserializeObject<RootObject>(json);
                Debug.WriteLine(r.loseisimprove.name);
            }
        }
    }
}