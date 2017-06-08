﻿using TchospiraApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TchospiraApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private MainViewModel ViewModel => BindingContext as MainViewModel;

        public MainPage()
        {
            InitializeComponent();
            
            BindingContext = new MainViewModel();
        }

       // private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
       //     if (e.SelectedItem != null)
       //     {
       //         ViewModel.ShowCategoriaCommand.Execute(e.SelectedItem);
       //     }
      //  }
    }
}