using Microsoft.Maui.Controls;
using SantiagoPanchiP3.ViewModels;

namespace SantiagoPanchiP3.Views
{
    public partial class MovieListPage : ContentPage
    {
        private readonly MovieListViewModel _viewModel;

        // Constructor que recibe el ViewModel como parámetro
        public MovieListPage(MovieListViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;  // Asigna el ViewModel al campo privado _viewModel
            BindingContext = _viewModel; // Asigna el ViewModel al BindingContext de la página
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadMoviesCommand.ExecuteAsync(null); // Carga las películas al abrir la página
        }
    }
}
