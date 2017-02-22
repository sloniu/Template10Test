using System.Collections.Generic;

namespace WebApi.Models.MatchListBySummonerId
{
    public class RootObject
    {
        public List<Match> matches { get; set; }
        public int startIndex { get; set; }
        public int endIndex { get; set; }
        public int totalGames { get; set; }
    }
}