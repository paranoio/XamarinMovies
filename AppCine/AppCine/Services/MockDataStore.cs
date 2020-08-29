/*
 Copyright Luis Lemus 
    All rights reserverd 
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AppCine.Models;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;

namespace AppCine.Services
{
    public class MockDataStore : IDataStore<Movie>
    {

        //no hay forma de limitar cantidad de resultados ? https://www.themoviedb.org/talk/522eeae419c2955e90252e23
        public const string UrlRequestParameters = "?api_key=d0be84a5b4891e42ebf97a4ce0697f37&language=es-MX&page=1";
        public const string UrlTopMovies = "https://api.themoviedb.org/3/movie/top_rated";
        public const string UrlUpcomingMovies = "https://api.themoviedb.org/3/movie/upcoming";
        public const string urlPopularMovies = "https://api.themoviedb.org/3/movie/popular";
        public const string UrlMovieDetailParameters = "?api_key=d0be84a5b4891e42ebf97a4ce0697f37&language=es-MX";
        public const string UrlMovieDetail = "https://api.themoviedb.org/3/movie/";        
        public const string BaseImageUrl = "https://image.tmdb.org/t/p/w300/";

        public const string UrlCredits = "https://api.themoviedb.org/3/movie/";
        public const string UrlCreditsParameters = "/credits?api_key=d0be84a5b4891e42ebf97a4ce0697f37";
        private HttpClient httpClient;
        private List<Movie> movies;

        public MockDataStore()
        {
            httpClient = new HttpClient();

            movies = new List<Movie>();
        }

        
        public async Task<Movie> GetMovieAsync(int id)
        {
            return await Task.FromResult(movies.FirstOrDefault(s => s.id == id));
        }

        public async Task<IEnumerable<Movie>> GetTopMoviesAsync()
        {
            var result = await httpClient.GetAsync(UrlTopMovies+UrlRequestParameters);
            

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var resultPlain = await result.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<MovieResults>(resultPlain);
                var list = json.results.Take(10);
                foreach (var item in list)
                {
                    item.baseImageUrl = BaseImageUrl;
                }
                return list;
            }
            else
            {
                Log.Warning("WARNING", "GetTopMoviesAsync fallo al realizar request");                             
                return null;
            }            
        }

        public async Task<IEnumerable<Movie>> GetUpcomingMoviesAsync()
        {
            var result = await httpClient.GetAsync(UrlUpcomingMovies + UrlRequestParameters);


            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var resultPlain = await result.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<MovieResults>(resultPlain);
                var list = json.results.Take(10);
                foreach (var item in list)
                {
                    item.baseImageUrl = BaseImageUrl;
                }
                return list;
            }
            else
            {
                Log.Warning("WARNING", "GetUpcomingMoviesAsync fallo al realizar request");
                return null;
            }
        }

        public async Task<IEnumerable<Movie>> GetPopularMoviesAsync()
        {
            var result = await httpClient.GetAsync(urlPopularMovies + UrlRequestParameters);


            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var resultPlain = await result.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<MovieResults>(resultPlain);

                var list = json.results.Take(10);
                foreach (var item in list)
                {
                    item.baseImageUrl = BaseImageUrl;
                }
                return list;
            }
            else
            {
                Log.Warning("WARNING", "GetPopularMoviesAsync fallo al realizar request");
                return null;
            }
        }


        public async Task<MovieDetails> GetMovieDetailsAsync(string movieId)
        {
            var url = UrlMovieDetail + (movieId) + UrlMovieDetailParameters;
            var result = await httpClient.GetAsync(url);


            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var resultPlain = await result.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<MovieDetails>(resultPlain);
                json.baseImageUrl = BaseImageUrl;
                return json;
            }
            else
            {
                Log.Warning("WARNING", "GetMovieDetailsAsync fallo al realizar request");
                return null;
            }
        }



        public async Task<MovieCredits> GetCreditsAsync(string movieId)
        {
            var url = UrlCredits + (movieId) + UrlCreditsParameters;
            var result = await httpClient.GetAsync(url);


            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var resultPlain = await result.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<MovieCredits>(resultPlain);
                //json.baseImageUrl = BaseImageUrl;
                foreach (var item in json.cast)
                {
                    item.baseImageUrl = BaseImageUrl;
                }                
                return json;
            }
            else
            {
                Log.Warning("WARNING", "GetMovieDetailsAsync fallo al realizar request");
                return null;
            }
        }
    }
}