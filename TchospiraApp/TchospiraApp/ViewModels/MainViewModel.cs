using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TchospiraApp.Helpers;
using TchospiraApp.Models;
using TchospiraApp.Services;
using Xamarin.Forms;

namespace TchospiraApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ITchospiraService _tchospiraService;
        public ObservableCollection<Tag> Tags { get; }

        public Command AboutCommand { get; }

        public Command SearchCommand { get; }

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
            Tags = new ObservableCollection<Tag>();
            AboutCommand = new Command(ExecuteAboutCommand);
            SearchCommand = new Command(ExecuteSearchCommand);
            ShowCategoriaCommand = new Command<Tag>(ExecuteShowCategoriaCommand);
            LoadAsync();

            Title = "Monkey Hub";
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

        public override async Task LoadAsync()
        {
            var tags = await _tchospiraService.GetTagsAsync();

            System.Diagnostics.Debug.WriteLine("FOUND {0} TAGS", tags.Count);
            Tags.Clear();
            foreach (var tag in tags)
            {
                Tags.Add(tag);
            }

            OnPropertyChanged(nameof(Tags));
        }
    }
}