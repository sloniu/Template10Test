namespace WebApi.Models.MatchById
{
    public class Timeline
    {
        public CreepsPerMinDeltas creepsPerMinDeltas { get; set; }
        public XpPerMinDeltas xpPerMinDeltas { get; set; }
        public GoldPerMinDeltas goldPerMinDeltas { get; set; }
        public CsDiffPerMinDeltas csDiffPerMinDeltas { get; set; }
        public XpDiffPerMinDeltas xpDiffPerMinDeltas { get; set; }
        public DamageTakenPerMinDeltas damageTakenPerMinDeltas { get; set; }
        public DamageTakenDiffPerMinDeltas damageTakenDiffPerMinDeltas { get; set; }
        public string role { get; set; }
        public string lane { get; set; }
    }
}