using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Template10Test.Models;
using Template10Test.Models.Riot.ChampionById;
using Template10Test.Twitch10ServiceReference;
using Template10Test.Views;

namespace Template10Test.ViewModels
{
    public class BuildsPageViewModel : ViewModelBase
    {
        private const string Url = "http://twitch10webapitest.azurewebsites.net";
        private const string ChampionUrl = "https://ddragon.leagueoflegends.com/cdn/7.15.1/img/champion";
        private const string ItemUrl = "http://ddragon.leagueoflegends.com/cdn/7.15.1/img/item";
        private const string ApiKey = "RGAPI-164c3fb2-3e40-474b-bf7e-18e864ed4a28";

        private BuildContract _build;

        private string _builds;

        private ObservableCollection<DisplayBuild> _buildsList;


        private string _championImage = "ms-appx://Template10Test/Assets/StoreLogo.png";
        private readonly Service1Client _client = new Service1Client();

        private List<DisplayBuild> _currentBuilds;
        private string _item1Img = "ms-appx://Template10Test/Assets/StoreLogo.png";

        private string _item2Img = "ms-appx://Template10Test/Assets/StoreLogo.png";

        private string _item3Img = "ms-appx://Template10Test/Assets/StoreLogo.png";

        private string _item4Img = "ms-appx://Template10Test/Assets/StoreLogo.png";

        private string _item5Img = "ms-appx://Template10Test/Assets/StoreLogo.png";

        private readonly string _item6Img = "ms-appx://Template10Test/Assets/StoreLogo.png";


        private string _matchId;


        private string _playerName;

        private Region _region;

        private DisplayBuild _selectedCurrentBuild;
        private string _warningMessage;

        public string PlayerName
        {
            get { return _playerName; }
            set
            {
                _playerName = value;
                RaisePropertyChanged(() => PlayerName);
            }
        }

        public ObservableCollection<Region> RegionOptions { get; } = new ObservableCollection<Region>
        {
            new Region {Name = "North America", Value = "na1"},
            new Region {Name = "Europe West", Value = "euw1"}
        };

        public Region Region
        {
            get { return _region; }
            set
            {
                _region = value;
                RaisePropertyChanged(() => Region);
            }
        }

        public string Builds
        {
            get { return _builds; }
            set
            {
                _builds = value;
                RaisePropertyChanged(() => Builds);
            }
        }

        public BuildContract Build
        {
            get { return _build; }
            set
            {
                _build = value;
                RaisePropertyChanged(() => Build);
            }
        }

        public string ChampionImage
        {
            get { return _championImage; }
            set
            {
                _championImage = value;
                RaisePropertyChanged(() => ChampionImage);
            }
        }

        public string Item1Img
        {
            get { return _item1Img; }
            set
            {
                _item1Img = value;
                RaisePropertyChanged(() => Item1Img);
            }
        }

        public string Item2Img
        {
            get { return _item2Img; }
            set
            {
                _item2Img = value;
                RaisePropertyChanged(() => Item2Img);
            }
        }

        public string Item3Img
        {
            get { return _item3Img; }
            set
            {
                _item3Img = value;
                RaisePropertyChanged(() => Item3Img);
            }
        }

        public string Item4Img
        {
            get { return _item4Img; }
            set
            {
                _item4Img = value;
                RaisePropertyChanged(() => Item4Img);
            }
        }

        public string Item5Img
        {
            get { return _item5Img; }
            set
            {
                _item5Img = value;
                RaisePropertyChanged(() => Item5Img);
            }
        }

        public string Item6Img
        {
            get { return _item6Img; }
            set
            {
                _item1Img = value;
                RaisePropertyChanged(() => Item6Img);
            }
        }

        public List<DisplayBuild> CurrentBuilds
        {
            get { return _currentBuilds; }
            set
            {
                _currentBuilds = value;
                RaisePropertyChanged(() => CurrentBuilds);
            }
        }

        public ObservableCollection<DisplayBuild> BuildsList
        {
            get { return _buildsList; }
            set
            {
                _buildsList = value;
                RaisePropertyChanged(() => BuildsList);
            }
        }

        public string MatchId
        {
            get { return _matchId; }
            set
            {
                _matchId = value;
                RaisePropertyChanged(() => MatchId);
            }
        }

        public string WarningMessage
        {
            get { return _warningMessage; }
            set
            {
                _warningMessage = value;
                RaisePropertyChanged(() => WarningMessage);
            }
        }

        public DisplayBuild SelectedCurrentBuild
        {
            get { return _selectedCurrentBuild; }
            set
            {
                _selectedCurrentBuild = value;
                RaisePropertyChanged(() => SelectedCurrentBuild);
            }
        }

        public async void FetchBuilds()
        {
            if (PlayerName == null)
                return;
            try
            {
                var get = _client.GetAsync(Region.Value, PlayerName);
                await get;
                var build = get.Result;
                Builds = build.MatchId.ToString();
                Build = build;

                using (var w = new HttpClient())
                {
                    var json =
                        w.GetStringAsync(
                                $"https://{Region.Value}.api.riotgames.com/lol/static-data/v3/champions/{Build.ChampionId}?locale=en_US&tags=image&api_key={ApiKey}")
                            ;
                    await json;
                    var o = json.Result;
                    var r = JsonConvert.DeserializeObject<RootObject>(o);
                    ChampionImage = $"{ChampionUrl}/{r.image.full}";
                }

                CurrentBuilds = new List<DisplayBuild>
            {
                new DisplayBuild
                {
                    ChampionUrl = ChampionImage,
                    Item1Url = $"{ItemUrl}/{Build.Item1Id}.png",
                    Item2Url = $"{ItemUrl}/{Build.Item2Id}.png",
                    Item3Url = $"{ItemUrl}/{Build.Item3Id}.png",
                    Item4Url = $"{ItemUrl}/{Build.Item4Id}.png",
                    Item5Url = $"{ItemUrl}/{Build.Item5Id}.png",
                    Item6Url = $"{ItemUrl}/{Build.Item6Id}.png",
                    MatchId = Build.MatchId.ToString(),
                    PlayerName = Build.PlayerName,
                    Region = Build.Region
                }
            };
            }
            catch (FaultException<ServiceException> e)
            {
                WarningMessage = e.Detail.Message;
                throw;
            }
            
        }

        public async void GetUsersBuilds()
        {
            if (string.IsNullOrEmpty(Region.Value))
                return;
            if (string.IsNullOrEmpty(LoginManager.Instance.Token))
            {
                WarningMessage = "You need to log in to post build.\n" +
                                 "Please log in on HOME page.";
                return;
            }
            WarningMessage = "";
            try
            {
                var get = _client.GetBuildsByUserAsync(LoginManager.Instance.Token);
                await get;
                var builds = get.Result;

                using (var w = new HttpClient())
                {
                    BuildsList = new ObservableCollection<DisplayBuild>();
                    foreach (var build in builds)
                    {
                        var json =
                            w.GetStringAsync(
                                    $"https://{Region.Value}.api.riotgames.com/lol/static-data/v3/champions/{build.ChampionId}?locale=en_US&tags=image&api_key={ApiKey}")
                                .Result;
                        var r = JsonConvert.DeserializeObject<RootObject>(json);

                        BuildsList.Add(new DisplayBuild
                        {
                            ChampionUrl = $"{ChampionUrl}/{r.image.full}",
                            Item1Url = $"{ItemUrl}/{build.Item1Id}.png",
                            Item2Url = $"{ItemUrl}/{build.Item2Id}.png",
                            Item3Url = $"{ItemUrl}/{build.Item3Id}.png",
                            Item4Url = $"{ItemUrl}/{build.Item4Id}.png",
                            Item5Url = $"{ItemUrl}/{build.Item5Id}.png",
                            Item6Url = $"{ItemUrl}/{build.Item6Id}.png",
                            MatchId = MatchId,
                            PlayerName = build.PlayerName,
                            Region = build.Region
                        });
                    }
                }
            }
            catch (FaultException<ServiceException> e)
            {
                WarningMessage = e.Detail.Message;
                throw;
            }
            
        }

        public async void AddBuild()
        {
            if (SelectedCurrentBuild == null)
                return;
            if (string.IsNullOrEmpty(LoginManager.Instance.Token))
            {
                WarningMessage = "You need to log in to post build.\n" +
                                 "Please log in on HOME page.";
                return;
            }
            WarningMessage = "";
            try
            {
                var post = _client.PostBuildAsync(LoginManager.Instance.Token, SelectedCurrentBuild.Region,
                SelectedCurrentBuild.PlayerName, SelectedCurrentBuild.MatchId);
                await post;
            }
            catch (FaultException<ServiceException> e)
            {
                WarningMessage = e.Detail.Message;
                throw;
            }
            
        }

        public async void PushBuild()
        {
            if (string.IsNullOrEmpty(LoginManager.Instance.Token))
            {
                WarningMessage = "You need to log in to post build.\n" +
                                 "Please log in on HOME page.";
                return;
            }
            WarningMessage = "";
            try
            {
                var post = _client.PostBuildAsync(LoginManager.Instance.Token, Region.Value, PlayerName, MatchId);
                await post;
            }
            catch (FaultException<ServiceException> e)
            {
                WarningMessage = e.Detail.Message;
                throw;
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
    }
}