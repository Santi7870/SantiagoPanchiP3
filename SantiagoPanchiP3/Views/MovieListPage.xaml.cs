using Microsoft.Maui.Controls;
using SantiagoPanchiP3.ViewModels;

namespace SantiagoPanchiP3.Views
{
    public partial class MovieListPage : ContentPage
    {
        private readonly MovieListViewModel _viewModel;

        public MovieListPage(MovieListViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;  
            BindingContext = _viewModel; 
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadMoviesCommand.ExecuteAsync(null); // Carga las películas al abrir la página
        }
    }
}
