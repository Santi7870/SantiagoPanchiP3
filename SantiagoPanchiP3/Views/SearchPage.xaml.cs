using SantiagoPanchiP3.ViewModels;

namespace SantiagoPanchiP3.Views;

public partial class SearchPage : ContentPage
{
	public SearchPage()
	{
		InitializeComponent();

    }

	  public SearchPage(SearchViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

}