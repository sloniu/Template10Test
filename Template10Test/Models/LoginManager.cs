using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;

namespace Template10Test.Models
{
    class LoginManager
    {
        public string ClientId { get; set; } = "4hz5hgythniudwl0frrequyu6wxbv02";
        public string RedirectUri { get; set; } = "http://localhost";
        public string Scope { get; set; } = "user_read+channel_read";
        public string EndUrl { get; set; } = "http://localhost";
        public string Response { get; set; } = "token";
        public string Result { get; set; } = "";
        public string Token { get; set; }


        public async Task Login()
        {
            Debug.WriteLine(
                $"https://api.twitch.tv/kraken/oauth2/authorize?client_id={ClientId}&redirect_uri={RedirectUri}&scope={Scope}&response_type={Response}");

            Uri startUri = new Uri($"https://api.twitch.tv/kraken/oauth2/authorize?client_id={ClientId}&redirect_uri={RedirectUri}&scope={Scope}&response_type={Response}");
            Uri endUri = new Uri(EndUrl);
            try
            {
                WebAuthenticationResult webAuthenticationResult =
                    await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, startUri, endUri);

                switch (webAuthenticationResult.ResponseStatus)
                {
                    // Successful authentication. 
                    case WebAuthenticationStatus.Success:
                        Debug.WriteLine("Auth:Success");
                        Token =
                            webAuthenticationResult.ResponseData.Substring(
                                webAuthenticationResult.ResponseData.IndexOf("token=", StringComparison.Ordinal) + 6).Replace("&scope=" + Scope,"");
                        Debug.WriteLine(Token);
                        Debug.WriteLine(WebAuthenticationBroker.GetCurrentApplicationCallbackUri());
                        break;
                    // HTTP error. 
                    case WebAuthenticationStatus.ErrorHttp:
                        Result = webAuthenticationResult.ResponseErrorDetail.ToString();
                        Debug.WriteLine("Auth:ErrorHttp");
                        break;
                    default:
                        Result = webAuthenticationResult.ResponseData;
                        Debug.WriteLine("Auth:DefaultCase");
                        break;
                }
                // wbVewPostAuthentication is a webPane
                //if (!string.IsNullOrWhiteSpace(result)) wbVewPostAuthentication.NavigateToString(result);
                
            }
            catch (Exception ex)
            {
                Result = ex.Message;
            }
        }
    }
}
