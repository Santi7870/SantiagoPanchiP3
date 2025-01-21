using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SantiagoPanchiP3.Models;
using SantiagoPanchiP3.Data;

namespace SantiagoPanchiP3.ViewModels
{
    public class SearchViewModel
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public ObservableCollection<Movie> Movies { get; set; } = new ObservableCollection<Movie>();

        public async Task SearchMovieAsync(string query)
        {
            string url = $"https://freetestapi.com/api/v1/movies?search={query}&limit=1";
            var response = await _httpClient.GetStringAsync(url);
            var movies = JsonSerializer.Deserialize<List<Movie>>(response);

            if (movies != null && movies.Any())
            {
                var movie = movies.First();
                await MovieDatabase.SaveMovieAsync(movie);
            }
        }
    }

}

