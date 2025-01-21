using SantiagoPanchiP3.Models;
using SantiagoPanchiP3.Data;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;
using System;
using Microsoft.Maui.Controls;

namespace SantiagoPanchiP3.ViewModels
{
    public class MovieListViewModel : BindableObject
    {
        private readonly MovieDatabase _movieDatabase;
        public ObservableCollection<MovieViewModel> Movies { get; set; } = new ObservableCollection<MovieViewModel>();

        public ICommand GoBackCommand { get; }

        public MovieListViewModel()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "spanchi_movies.db");
            _movieDatabase = new MovieDatabase(dbPath);

            LoadMoviesAsync();

            GoBackCommand = new Command(async () => await Application.Current.MainPage.Navigation.PopAsync());
        }

        private async Task LoadMoviesAsync()
        {
            var moviesList = await _movieDatabase.GetMoviesAsync();
            Movies.Clear();

            foreach (var movie in moviesList)
            {
                Movies.Add(new MovieViewModel(movie));
            }
        }
    }

    public class MovieViewModel
    {
        public string FormattedMovie { get; }

        public MovieViewModel(Movie movie)
        {
            FormattedMovie = $"Título: {movie.Title}, Género: {movie.Genero}, Actor Principal: {movie.ActorPrincipal}, " +
                             $"Awards: {movie.Awards}, Website: {movie.Website},Spanchi {movie.Spanchi}";
        }
    }
}
