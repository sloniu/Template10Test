using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Template10Test.Models;
using Template10Test.Views;

namespace Template10Test.ViewModels
{
    public class BuildsPageViewModel : ViewModelBase
    {
        private const string Url = "http://twitch10webapitest.azurewebsites.net";

        private string _playerName;
        public string PlayerName
        {
            get { return _playerName; }
            set
            {
                _playerName = value;
                RaisePropertyChanged(() => PlayerName);
            }
        }

        public ObservableCollection<Region> RegionOptions { get; } = new ObservableCollection<Region>()
        {
            new Region() {Name = "North America", Value = "na"},
            new Region() {Name = "Europe West", Value = "euw"}
        };

        private Region _region;
        public Region Region
        {
            get { return _region; }
            set
            {
                _region = value;
                RaisePropertyChanged(() => Region);
            }
        }

        private string _builds;

        public string Builds
        {
            get { return _builds; }
            set
            {
                _builds = value;
                RaisePropertyChanged(() => Builds);
            }
        }

        private Build _build;

        public Build Build
        {
            get { return _build; }
            set
            {
                _build = value;
                RaisePropertyChanged(() => Build);
            }
        }

        #region OnNavigate

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode,
           IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        #endregion

        #region Goto

        public void GotoDetailsPage() =>
            NavigationService.Navigate(typeof(DetailPage));

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(SettingsPage), 0);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(SettingsPage), 2);

        #endregion

        public async void FetchBuilds()
        {
            if (PlayerName != null)
            {
                using (var w = new HttpClient())
                {
                    try
                    {
                        var o = w.GetStringAsync($"{Url}/api/builds/{Region.Value}/{PlayerName}");
                        await o;
                        var json = o.Result;
                        var build = JsonConvert.DeserializeObject<Build>(json);
                        Builds = build.MatchId.ToString();
                        Build = build;
                    }
                    catch (Exception e)
                    {

                    }
                    
                }
            }
        }

        private string _matchId;

        public string MatchId
        {
            get { return _matchId; }
            set
            {
                _matchId = value;
                RaisePropertyChanged(() => MatchId);
            }
        }


        public async void PushBuild()
        {
                using (var client = new HttpClient())
                {
                    var values = new Dictionary<string, string>
                    {
                        { "region", "" },
                        { "summonerName", "" }
                    };

                    var content = new FormUrlEncodedContent(values);
                    try
                    {
                        var response =
                            client.PostAsync(
                                $"{Url}/api/Builds/{LoginManager.Instance.Token}/{Region.Value}/{PlayerName}/{MatchId}",
                                content);
                        await response;
                    }
                    catch (Exception e)
                    {

                    }

                }
        }
    }
}
