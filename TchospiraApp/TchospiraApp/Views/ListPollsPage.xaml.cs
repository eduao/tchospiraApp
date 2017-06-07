using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TchospiraApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TchospiraApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPollsPage : ContentPage
    {

        private ListPollsViewModel ViewModel => BindingContext as ListPollsViewModel;

        public ListPollsPage()
        {
            InitializeComponent();

            // BindingContext = new ListPollsViewModel();
        }
    }
}