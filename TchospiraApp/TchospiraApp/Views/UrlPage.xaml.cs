using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TchospiraApp.Models;
using TchospiraApp.Services;
using TchospiraApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TchospiraApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UrlPage : ContentPage
    {

        private UrlViewModel ViewModel => BindingContext as UrlViewModel;

        //public News ContentWeb { get; }

        public UrlPage()
        {
            InitializeComponent();
            //var monkeyHubApiService = DependencyService.Get<ITchospiraService>();
            ////ContentWeb = news;
            //BindingContext = new UrlViewModel(monkeyHubApiService, news);
        }
    }
}