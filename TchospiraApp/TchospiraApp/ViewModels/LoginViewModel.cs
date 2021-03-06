﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TchospiraApp.Helpers;
using TchospiraApp.Services;
using TchospiraApp.Views;
using Xamarin.Forms;

namespace TchospiraApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ITchospiraService _tchospiraService;
        AzureService _azureService;
        INavigation navigation;
        private bool _isBusy;
        public Command LoginCommand { get; }
        public Command LogoutCommand { get; }

        //ICommand loginCommand;

        // public Command LoginCommand { get; }

        //public ICommand LoginCommand =>
        //  loginCommand ?? (loginCommand = new Command(async () => await ExecuteLoginCommandAsync()));


        public LoginViewModel(ITchospiraService monkeyHubApiService)
        {
            //não utilizar toker armazenado
            Settings.AuthToken = string.Empty;
            Settings.UserId = string.Empty;
            _tchospiraService = monkeyHubApiService;

            //_navigation = navigation;
            _azureService = DependencyService.Get<AzureService>();

            LoginCommand = new Command(async () => await ExecuteLoginCommandAsync());
            LogoutCommand = new Command(async () => await ExecuteLogoutCommandAsync());
            Title = "Social Login Demo";
        }

        public LoginViewModel(INavigation nav)
        {
            _azureService = DependencyService.Get<AzureService>();
            navigation = nav;
            LoginCommand = new Command(async () => await ExecuteLoginCommandAsync());
            LogoutCommand = new Command(async () => await ExecuteLogoutCommandAsync());
            Title = "Tela login";

            if (Settings.IsLoggedIn)
            {
                Task.Delay(3000);
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }

        }

        //private void RemovePageFromStack()
        //{
         //   var existingPages = navigation.NavigationStack.ToList();
         //   foreach(var page in existingPages)
         //   {
         //       if (page.GetType() == typeof(LoginPage))
          //      {
          //          navigation.RemovePage(page);
          //      }
           // }
       // }
       private async Task ExecuteLogoutCommandAsync()
        {
            if (_isBusy || !(await LogoutAsync()))
            {
                return;
            }
            else
            {
                Application.Current.MainPage = new LoginPage();
                //await PushAsync<MainViewModel>();

                //RemovePageFromStack();
            }
            _isBusy = false;
        }

        private async Task ExecuteLoginCommandAsync()
        {
            if (_isBusy || ! (await LoginAsync()))
            {
                return;
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(new MainPage());
                //await PushAsync<MainViewModel>();

                //RemovePageFromStack();
            }
            _isBusy = false;
        }

        private Task<bool> LogoutAsync()
        {
            _isBusy = true;
            if (Settings.IsLoggedIn)
            {
                return _azureService.LogoutAsync(); 
                
            }
            return Task.FromResult(false);
        }

        private Task<bool> LoginAsync()
        {
            _isBusy = true;
            if (Settings.IsLoggedIn)
            {
                return Task.FromResult(true);
            }
            return _azureService.LoginAsync();
        }
    }
}
