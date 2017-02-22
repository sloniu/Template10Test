﻿using System.Collections.Generic;

namespace WebApi.Models.MatchById
{
    public class Team
    {
        public int teamId { get; set; }
        public bool winner { get; set; }
        public bool firstBlood { get; set; }
        public bool firstTower { get; set; }
        public bool firstInhibitor { get; set; }
        public bool firstBaron { get; set; }
        public bool firstDragon { get; set; }
        public bool firstRiftHerald { get; set; }
        public int towerKills { get; set; }
        public int inhibitorKills { get; set; }
        public int baronKills { get; set; }
        public int dragonKills { get; set; }
        public int riftHeraldKills { get; set; }
        public int vilemawKills { get; set; }
        public int dominionVictoryScore { get; set; }
        public List<Ban> bans { get; set; }
    }
}