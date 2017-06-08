using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TchospiraApp.Services;
using TchospiraApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TchospiraApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        private LoginViewModel ViewModel => BindingContext as LoginViewModel;

        public LoginPage()
        {
            InitializeComponent();
            var monkeyHubApiService = DependencyService.Get<ITchospiraService>();
            BindingContext = new LoginViewModel(monkeyHubApiService);
            //BindingContext = new LoginViewModel();
        }
    }
}