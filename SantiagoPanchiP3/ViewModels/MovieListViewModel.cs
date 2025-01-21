using SantiagoPanchiP3.Models;
using SantiagoPanchiP3.Data;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace SantiagoPanchiP3.ViewModels
{
    public partial class MovieListViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private ObservableCollection<Movie> movies;

        public MovieListViewModel() { }

        public MovieListViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
            Movies = new ObservableCollection<Movie>(); 
        }

        [RelayCommand]
        private async Task LoadMovies()
        {
            try
            {
                var moviesFromDb = await _databaseService.GetMoviesAsync();
                if (moviesFromDb != null && moviesFromDb.Any())
                {
                    Movies.Clear();
                    foreach (var movie in moviesFromDb)
                    {
                        Movies.Add(movie);
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron películas.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar las películas: {ex.Message}");
            }
        }
    }
}

