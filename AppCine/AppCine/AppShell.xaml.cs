using System;
using System.Collections.Generic;
using AppCine.ViewModels;
using AppCine.Views;
using Xamarin.Forms;

namespace AppCine
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
        }

        /*private async void OnMenuItemClicked(object sender, EventArgs e)
        {            
            await Shell.Current.GoToAsync("//LoginPage");
        }*/
    }
}
