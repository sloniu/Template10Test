using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace Twitch10WcfService.Models
{
    public sealed class AuthenticateToken
    {
        private static volatile AuthenticateToken instance;
        private static object syncRoot = new Object();

        public string UserName { get; set; }
        public bool Valid { get; set; } = false;

        public static AuthenticateToken Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new AuthenticateToken();
                    }
                }

                return instance;
            }
        }

        public bool Authenticate(string token)
        {
            using (var w = new HttpClient())
            {
                try
                {
                    string json = w.GetStringAsync($"https://api.twitch.tv/kraken?oauth_token={token}").Result;
                    var r = JsonConvert.DeserializeObject<Token.RootObject>(json);
                    if (!r.Token.Valid) Valid = false;
                    UserName = r.Token.UserName;
                    Valid = true;
                    return true;


                }
                catch (Exception e)
                {
                    // ignored
                    return false;
                }
            }
        }
    }
}