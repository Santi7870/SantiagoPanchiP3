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

        // Lista observable de pel�culas
        [ObservableProperty]
        private ObservableCollection<Movie> movies;

        // Constructor sin par�metros requerido por la inyecci�n de dependencias
        public MovieListViewModel() { }

        // Constructor con DatabaseService inyectado
        public MovieListViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
            Movies = new ObservableCollection<Movie>(); // Inicializa Movies para evitar NullReferenceException
        }

        // Comando para cargar las pel�culas desde la base de datos
        [RelayCommand]
        private async Task LoadMovies()
        {
            try
            {
                // Cargar pel�culas desde la base de datos
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
                    Console.WriteLine("No se encontraron pel�culas.");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error durante la carga de las pel�culas
                Console.WriteLine($"Error al cargar las pel�culas: {ex.Message}");
            }
        }
    }
}

