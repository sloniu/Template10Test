namespace WebApi.Models.MatchById
{
    public class Player
    {
        public int summonerId { get; set; }
        public string summonerName { get; set; }
        public string matchHistoryUri { get; set; }
        public int profileIcon { get; set; }
    }
}