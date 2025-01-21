using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SantiagoPanchiP3.Models;
using SantiagoPanchiP3.Data;
using System.IO;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using Microsoft.Maui.Controls;

namespace SantiagoPanchiP3.ViewModels
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly MovieDatabase _movieDatabase;

        public ObservableCollection<Movie> Movies { get; set; } = new ObservableCollection<Movie>();

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchCommand { get; }
        public ICommand ClearCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public SearchViewModel()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "spanchi_movies.db");
            _movieDatabase = new MovieDatabase(dbPath);

            SearchCommand = new Command(async () => await SearchMovieAsync());
            ClearCommand = new Command(ClearSearch);
        }

        private async Task SearchMovieAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor ingresa un nombre de película", "OK");
                return;
            }

            string url = $"https://freetestapi.com/api/v1/movies?search={SearchText}&limit=1";

            try
            {
                var response = await _httpClient.GetStringAsync(url);
                var movies = JsonSerializer.Deserialize<List<Movie>>(response);

                if (movies != null && movies.Any())
                {
                    var movie = movies.First();
                    await _movieDatabase.SaveMovieAsync(movie);
                    Movies.Clear();
                    Movies.Add(movie);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No se encontró la película", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error de conexión: {ex.Message}", "OK");
            }
        }

        private void ClearSearch()
        {
            SearchText = string.Empty;
            Movies.Clear();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}



