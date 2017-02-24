using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace WebApi.Models
{
    public class AuthenticateToken
    {

        public string Authenticate(string token)
        {
            var uri = new Uri($"https://api.twitch.tv/kraken?oauth_token={token}");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    string s = reader.ReadToEnd().Replace("\0", "");

                    return s;
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }

            //return reader.ToString();
        }
    }
}