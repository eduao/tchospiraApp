﻿using System.Collections.ObjectModel;
using TchospiraApp.Models;
using TchospiraApp.Services;
using Xamarin.Forms;

namespace TchospiraApp.ViewModels
{
    class ListPollsViewModel : BaseViewModel
    {
        private readonly ITchospiraService _tchospiraService;
        private string _searchTerm;
        private Tag _tag;

        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                SetProperty(ref _searchTerm, value);
                SearchCommand.ChangeCanExecute();
                SearchResults.Clear();
            }
        }

        public Tag Tag
        {
            get { return _tag; }
            set
            {
                SetProperty(ref _tag, value);
                SearchCommand.ChangeCanExecute();
                SearchResults.Clear();
            }
        }

        public Command SearchCommand { get; }

        public ObservableCollection<New> SearchResults { get; }

        public Command<New> ShowContentCommand { get; }

        public ListPollsViewModel(ITchospiraService tchospiraService, Tag tag)
        {
            _tchospiraService = tchospiraService;
            _tag = tag;

            SearchResults = new ObservableCollection<New>();
            SearchCommand = new Command(ExecuteSearchCommand, CanExecuteSearchCommand);
            ShowContentCommand = new Command<New>(ExecuteShowContentCommand);
        }

        public ListPollsViewModel(ITchospiraService tchospiraService)
        {
            _tchospiraService = tchospiraService;
            //_tag = tag;

            SearchResults = new ObservableCollection<New>();
            SearchCommand = new Command(ExecuteSearchCommand, CanExecuteSearchCommand);
            ShowContentCommand = new Command<New>(ExecuteShowContentCommand);
        }

        private async void ExecuteShowContentCommand(New content)
        {
            
             await PushAsync<ListPollsViewModel>(content);
        }

        private bool CanExecuteSearchCommand()
        {
            return string.IsNullOrWhiteSpace(SearchTerm) == false;
        }

        private async void ExecuteSearchCommand()
        {
            SearchTerm = "";

            var searchResults = await _tchospiraService.GetNewsByFilterAsync(SearchTerm);

            SearchResults.Clear();
            if (searchResults == null)
            {
                await DisplayAlert("MonkeyHub", "Nenhum resultado encontrado.", "Ok");
                return;
            }

            foreach (var searchResult in searchResults)
            {
                SearchResults.Add(searchResult);
            }
        }
    }
}
