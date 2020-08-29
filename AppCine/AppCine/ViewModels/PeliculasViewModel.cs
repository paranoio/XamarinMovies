/*
 Copyright Luis Lemus 
    All rights reserverd 
 */

using AppCine.Models;
using AppCine.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCine.ViewModels
{
    public class PeliculasViewModel : BaseViewModel
    {
        
        private ObservableCollection<Movie> _topMovies;
        private ObservableCollection<Movie> _upcomingMovies;
        private ObservableCollection<Movie> _popularMovies;
        private IEnumerable<Movie> top;
        private IEnumerable<Movie> upcoming;
        private IEnumerable<Movie> popular;

        private string _searchText;



        public Command LoadItemsCommand { get; }
        public Command TapMovieCommand { get; }
        public Command SearchChangeCommand { get; }
        

        public PeliculasViewModel()
        {                     
            _topMovies = new ObservableCollection<Movie>();
            _upcomingMovies = new ObservableCollection<Movie>();
            _popularMovies = new ObservableCollection<Movie>();
            _searchText = "";


            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            TapMovieCommand = new Command(async (object parameter) => await onMovieTap(parameter));
            SearchChangeCommand = new Command(async () => await onSearch());
        }

        public async Task onSearch()
        {
            var search = SearchText.Trim();
            if (search == "" || search.Length<3)
            {
                if (TopMovies.Count!= top.Count())
                {
                    TopMovies = new ObservableCollection<Movie>(top);
                }
                if (UpcomingMovies.Count != upcoming.Count())
                {
                    UpcomingMovies = new ObservableCollection<Movie>(top);
                }
                if (PopularMovies.Count != popular.Count())
                {
                    PopularMovies = new ObservableCollection<Movie>(top);
                }                
            }
            else
            {
                var matches = top.Where(p => (p.title.IndexOf(search, StringComparison.OrdinalIgnoreCase) > -1) ||
                                            (p.original_title.IndexOf(search, StringComparison.OrdinalIgnoreCase) > -1) ||
                                            (p.overview.IndexOf(search, StringComparison.OrdinalIgnoreCase) > -1));                
                TopMovies = new ObservableCollection<Movie>(matches);
                matches = upcoming.Where(p => (p.title.IndexOf(search, StringComparison.OrdinalIgnoreCase) > -1) ||
                                            (p.original_title.IndexOf(search, StringComparison.OrdinalIgnoreCase) > -1) ||
                                            (p.overview.IndexOf(search, StringComparison.OrdinalIgnoreCase) > -1));
                UpcomingMovies = new ObservableCollection<Movie>(matches);
                matches = popular.Where(p => (p.title.IndexOf(search, StringComparison.OrdinalIgnoreCase) > -1) ||
                                            (p.original_title.IndexOf(search, StringComparison.OrdinalIgnoreCase) > -1) ||
                                            (p.overview.IndexOf(search, StringComparison.OrdinalIgnoreCase) > -1));
                PopularMovies = new ObservableCollection<Movie>(matches);
            }
        }




        public async Task onMovieTap(object parameter) 
        {
            Movie m = parameter as Movie;
            await Shell.Current.GoToAsync($"{nameof(AboutPage)}?movieId={m.id}");                                 
        }   
        

        public async void OnAppearing()
        {            
            await ExecuteLoadItemsCommand();
        }
        
        public async Task ExecuteLoadItemsCommand() {
            IsBusy = true;
            try
            {                
                top = await DataStore.GetTopMoviesAsync();
                if (top != null) TopMovies = new ObservableCollection<Movie>(top);

                upcoming = await DataStore.GetUpcomingMoviesAsync();
                if (upcoming != null) UpcomingMovies = new ObservableCollection<Movie>(upcoming);

                popular = await DataStore.GetPopularMoviesAsync();
                if (popular != null) PopularMovies = new ObservableCollection<Movie>(popular);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }            
        }


        public ObservableCollection<Movie> TopMovies
        {
            get { return _topMovies; }
            set
            {
                _topMovies = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Movie> UpcomingMovies
        {
            get { return _upcomingMovies; }
            set
            {
                _upcomingMovies = value;
                OnPropertyChanged();
            }
        }        

        public ObservableCollection<Movie> PopularMovies
        {
            get { return _popularMovies; }
            set
            {
                _popularMovies = value;
                OnPropertyChanged();
            }
        }

        public String SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }
    }
}
