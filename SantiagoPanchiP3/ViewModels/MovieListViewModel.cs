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

        // Lista observable de películas
        [ObservableProperty]
        private ObservableCollection<Movie> movies;

        // Constructor sin parámetros requerido por la inyección de dependencias
        public MovieListViewModel() { }

        // Constructor con DatabaseService inyectado
        public MovieListViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
            Movies = new ObservableCollection<Movie>(); // Inicializa Movies para evitar NullReferenceException
        }

        // Comando para cargar las películas desde la base de datos
        [RelayCommand]
        private async Task LoadMovies()
        {
            try
            {
                // Cargar películas desde la base de datos
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
                // Manejar cualquier error durante la carga de las películas
                Console.WriteLine($"Error al cargar las películas: {ex.Message}");
            }
        }
    }
}

