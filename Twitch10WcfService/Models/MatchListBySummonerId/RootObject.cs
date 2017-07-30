using System.Collections.Generic;

namespace Twitch10WcfService.Models.MatchListBySummonerId
{
    public class RootObject
    {
        public List<Match> Matches { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public int TotalGames { get; set; }
    }
}