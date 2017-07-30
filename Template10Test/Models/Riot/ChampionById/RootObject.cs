namespace Template10Test.Models.Riot.ChampionById
{
    public class Image
    {
        public string full { get; set; }
        public string sprite { get; set; }
        public string group { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class RootObject
    {
        public Image image { get; set; }
        public string name { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string key { get; set; }
    }
}