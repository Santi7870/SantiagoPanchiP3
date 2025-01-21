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
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Runtime.CompilerServices;
using System.Linq;
using Microsoft.Maui.Controls;

namespace SantiagoPanchiP3.ViewModels
{
    [ObservableObject]
    public partial class SearchViewModel
    {
        private readonly MovieService _movieService;
        private readonly DatabaseService _databaseService;

        public SearchViewModel() { }

        public SearchViewModel(MovieService movieService, DatabaseService databaseService)
        {
            _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
        }

        [ObservableProperty]
        private string searchText;

        [ObservableProperty]
        private string message;

        [RelayCommand]
        private async Task SearchMovie()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Message = "Ingresar un título de película.";
                return;
            }

            var movie = await _movieService.SearchMovieAsync(SearchText);
            if (movie != null)
            {
                await _databaseService.SaveMovieAsync(movie);
                Message = "Película exitosamente guardada.";
            }
            else
            {
                Message = "No se encontraron resultados.";
            }
        }

        [RelayCommand]
        private void ClearSearch()
        {
            SearchText = string.Empty;
            Message = string.Empty;
        }
    }
}






