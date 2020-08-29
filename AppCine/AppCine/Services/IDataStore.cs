using AppCine.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppCine.Services
{
    public interface IDataStore<T>
    {
        Task<T> GetMovieAsync(int id);        
        Task<IEnumerable<T>> GetTopMoviesAsync();
        Task<IEnumerable<T>> GetUpcomingMoviesAsync();
        Task<IEnumerable<T>> GetPopularMoviesAsync();

        Task<MovieDetails> GetMovieDetailsAsync(string movieId);

        Task<MovieCredits> GetCreditsAsync(string movieId);
    }
}
