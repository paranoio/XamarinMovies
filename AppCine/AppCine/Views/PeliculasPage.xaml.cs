using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCine.Models;
using AppCine.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCine.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PeliculasPage : ContentPage
    {
        private PeliculasViewModel viewModel;
        Effect NoLineEffect;

        public PeliculasPage()
        {
            InitializeComponent();
            
            viewModel = new PeliculasViewModel();
            this.BindingContext = viewModel;
            
            NoLineEffect = Effect.Resolve("SuperAwesome.NoUnderlineEffect");
            EntrySearch.Effects.Add(NoLineEffect);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //inicializacion de viewmodel y sus requests
            viewModel.OnAppearing();
        }

    }
}