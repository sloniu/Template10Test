using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Twitch10WcfService.DAL.Entities
{
    public class Build
    {
        public int BuildId { get; set; }

        public string PlayerName { get; set; }

        public int ChampionId { get; set; }

        public int Item1Id { get; set; }

        public int Item2Id { get; set; }

        public int Item3Id { get; set; }

        public int Item4Id { get; set; }

        public int Item5Id { get; set; }

        public int Item6Id { get; set; }

        public int Spell1Id { get; set; }

        public int Spell2Id { get; set; }

        public long MatchId { get; set; }

        public virtual ICollection<UserHasBuild> UserHasBuilds { get; set; }

        public string Region { get; set; }

    }
}