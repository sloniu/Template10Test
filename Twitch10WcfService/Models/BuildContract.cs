using System.Runtime.Serialization;

namespace Twitch10WcfService.Models
{
    [DataContract]
    public class BuildContract
    {
        [DataMember]
        public int BuildId { get; set; }

        [DataMember]
        public string PlayerName { get; set; }

        [DataMember]
        public int ChampionId { get; set; }

        [DataMember]
        public int Item1Id { get; set; }

        [DataMember]
        public int Item2Id { get; set; }

        [DataMember]
        public int Item3Id { get; set; }

        [DataMember]
        public int Item4Id { get; set; }

        [DataMember]
        public int Item5Id { get; set; }

        [DataMember]
        public int Item6Id { get; set; }

        [DataMember]
        public int Spell1Id { get; set; }

        [DataMember]
        public int Spell2Id { get; set; }

        [DataMember]
        public long MatchId { get; set; }

        [DataMember]
        public string Region { get; set; }
    }
}