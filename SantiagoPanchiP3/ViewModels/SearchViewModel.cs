using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SantiagoPanchiP3.Models;
using SantiagoPanchiP3.Data;
using System.IO;
using System;

namespace SantiagoPanchiP3.ViewModels
{
    public class SearchViewModel
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly MovieDatabase _movieDatabase;

        public ObservableCollection<Movie> Movies { get; set; } = new ObservableCollection<Movie>();

        public string SearchText { get; set; }

        public ICommand SearchCommand { get; }
        public ICommand ClearCommand { get; }

        public SearchViewModel()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "spanchi_movies.db");
            _movieDatabase = new MovieDatabase(dbPath);

            SearchCommand = new Command(async () => await SearchMovieAsync(SearchText));
            ClearCommand = new Command(ClearSearch);
        }

        public async Task SearchMovieAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor ingresa un nombre de película", "OK");
                return;
            }

            string url = $"https://freetestapi.com/api/v1/movies?search={query}&limit=1";
            var response = await _httpClient.GetStringAsync(url);
            var movies = JsonSerializer.Deserialize<List<Movie>>(response);

            if (movies != null && movies.Any())
            {
                var movie = movies.First();
                await _movieDatabase.SaveMovieAsync(movie);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se encontró la película", "OK");
            }
        }

        private void ClearSearch()
        {
            SearchText = string.Empty;
        }
    }
}


