using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TchospiraApp.Helpers;
using TchospiraApp.Models;
using TchospiraApp.Services;
using TchospiraApp.Views;
using Xamarin.Forms;

namespace TchospiraApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ITchospiraService _tchospiraService;
        public ObservableCollection<News> News { get; }
        private bool _hasNews;
        public bool HasNews
        {
            get { return _hasNews; }
            set
            {
                SetProperty(ref _hasNews, value);
                //SearchCommand.ChangeCanExecute();
                //SearchResults.Clear();
            }
        }

        public Command AboutCommand { get; }

        public Command LogoutCommand { get; }

        public Command VisitUrlCommand { get; }

        public Command SeeMoreCommand { get; }

        public Command<Tag> ShowCategoriaCommand { get; }

        public string UserId { get; private set; }
        public string Token { get; private set; }
        

        public MainViewModel()
        {
            UserId = Settings.UserId;
            Token = Settings.AuthToken;
        }

        public MainViewModel(ITchospiraService monkeyHubApiService)
        {
            _tchospiraService = monkeyHubApiService;
            News = new ObservableCollection<News>();
            AboutCommand = new Command(ExecuteAboutCommand);
            LogoutCommand = new Command(ExecuteLogoutCommand);
            VisitUrlCommand = new Command<News>(ExecuteVisitUrlCommand);
            ShowCategoriaCommand = new Command<Tag>(ExecuteShowCategoriaCommand);
            SeeMoreCommand = new Command(ExecuteSeeMoreCommand);
            LoadAsync();

            //Title = "Monkey Hub";
        }

        private void ExecuteLogoutCommand()
        {
            Application.Current.MainPage = new LoginPage();
        }

        private async void ExecuteSeeMoreCommand()
        {
            await PushAsync<UrlViewModel>();
        }

        private async void ExecuteVisitUrlCommand(News news)
        {
            await PushAsync<UrlViewModel>(news);
        }

        private async void ExecuteSearchCommand()
        {
            await PushAsync<ListPollsViewModel>();
        }

        private async void ExecuteShowCategoriaCommand(Tag tag)
        {
            await PushAsync<ListPollsViewModel>(tag);
        }

        private async void ExecuteAboutCommand()
        {
            await PushAsync<SobreViewModel>();
        }

        //private bool HasNews()
        //{
        //    //await Task.Delay(5000);
        //    SetProperty(ref _hasNews, News.Count > 0);
        //    return News.Count > 0;
        //}

        public override async Task LoadAsync()
        {
            var news = await _tchospiraService.GetNewsAsync();

            System.Diagnostics.Debug.WriteLine("FOUND {0} TAGS", news.Count);
            News.Clear();
            foreach (var n in news)
            {
                News.Add(n);
            }
            HasNews = News.Count > 0;

            OnPropertyChanged(nameof(News));
            OnPropertyChanged(nameof(HasNews));
        }
    }
}