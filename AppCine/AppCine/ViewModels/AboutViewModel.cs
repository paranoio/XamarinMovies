using AppCine.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppCine.ViewModels
{
    [QueryProperty(nameof(movieId), nameof(movieId))]    
    public class AboutViewModel : BaseViewModel
    {
        private string _movieId = "";
        private bool initialized = false;

        private string _title;
        private string _overview;
        private string _studio;
        private string _genre;
        private string _release;
        private string _poster;
        public ICommand BackCommand { get; set; }

        private ObservableCollection<Cast> _casting;

        public AboutViewModel()
        {
            BackCommand = new Command(GoBack);
        }

        internal void OnAppearing()
        {
            Init();
        }

        private async void GoBack(object obj)
        {
            await Shell.Current.GoToAsync("..");            
        }

        private async void Init()
        {
            if (initialized) return;
            initialized = true;
            var result = await DataStore.GetMovieDetailsAsync(movieId);
            Title = result.title;            
            Overview = result.overview;
            var genres = "";
            foreach (var item in result.genres)
            {
                genres += item.name + ",";
            }
            Genre = genres.Substring(0,genres.Length-2);
            Release = result.release_date.Substring(0,4);
            Poster = result.Thumbnail;

            var credits = await DataStore.GetCreditsAsync(movieId);            

            Casting = new ObservableCollection<Cast>(credits.cast.Take(10));

        }

        
        
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
       
        public string Overview
        {
            get => _overview;
            set
            {
                _overview = value;
                OnPropertyChanged();
            }
        }
       
        public string Studio
        {
            get => _studio;
            set
            {
                _studio = value;
                OnPropertyChanged();
            }
        }
        
        public string Genre
        {
            get => _genre;
            set
            {
                _genre = value;
                OnPropertyChanged();
            }
        }

        public string Release
        {
            get => _release;
            set
            {
                _release = value;
                OnPropertyChanged();
            }
        }

        public string movieId
        {
            get => _movieId;
            set
            {
                _movieId = value;
                //Init();
                OnPropertyChanged();
            }
        }

        public string Poster
        {
            get => _poster;
            set
            {
                _poster = value;                
                OnPropertyChanged();
            }
        }
        
        public ObservableCollection<Cast> Casting
        {
            get => _casting;
            set
            {
                _casting = value;
                OnPropertyChanged();
            }
        }
    }
}