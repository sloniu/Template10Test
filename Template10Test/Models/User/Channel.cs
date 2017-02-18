﻿using Newtonsoft.Json;

namespace Template10Test.Models.User
{
    public class Channel
    {
        [JsonProperty("_links")]
        public Links2 _links { get; set; }

        [JsonProperty("background")]
        public object background { get; set; }

        [JsonProperty("banner")]
        public object banner { get; set; }

        [JsonProperty("broadcaster_language")]
        public string broadcaster_language { get; set; }

        [JsonProperty("display_name")]
        public string display_name { get; set; }

        [JsonProperty("game")]
        public string game { get; set; }

        [JsonProperty("logo")]
        public string logo { get; set; }

        [JsonProperty("mature")]
        public bool? mature { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("partner")]
        public bool partner { get; set; }

        [JsonProperty("url")]
        public string url { get; set; }

        [JsonProperty("video_banner")]
        public string video_banner { get; set; }

        [JsonProperty("_id")]
        public int _id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("created_at")]
        public string created_at { get; set; }

        [JsonProperty("updated_at")]
        public string updated_at { get; set; }

        [JsonProperty("delay")]
        public object delay { get; set; }

        [JsonProperty("followers")]
        public int followers { get; set; }

        [JsonProperty("profile_banner")]
        public string profile_banner { get; set; }

        [JsonProperty("profile_banner_background_color")]
        public string profile_banner_background_color { get; set; }

        [JsonProperty("views")]
        public int views { get; set; }

        [JsonProperty("language")]
        public string language { get; set; }
    }
}