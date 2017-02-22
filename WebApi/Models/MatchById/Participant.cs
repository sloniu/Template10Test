using System.Collections.Generic;

namespace WebApi.Models.MatchById
{
    public class Participant
    {
        public int teamId { get; set; }
        public int spell1Id { get; set; }
        public int spell2Id { get; set; }
        public int championId { get; set; }
        public string highestAchievedSeasonTier { get; set; }
        public Timeline timeline { get; set; }
        public List<Mastery> masteries { get; set; }
        public Stats stats { get; set; }
        public int participantId { get; set; }
        public List<Rune> runes { get; set; }
    }
}