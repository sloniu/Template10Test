using System;
using System.Linq;
using System.Web;

namespace WebApi.Models.MatchListBySummonerId
{

    public class Match
    {
        public string region { get; set; }
        public string platformId { get; set; }
        public object matchId { get; set; }
        public int champion { get; set; }
        public string queue { get; set; }
        public string season { get; set; }
        public object timestamp { get; set; }
        public string lane { get; set; }
        public string role { get; set; }
    }
}