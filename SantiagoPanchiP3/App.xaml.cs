using SantiagoPanchiP3.Views;

namespace SantiagoPanchiP3
{
    public partial class App : Application
    {
        public App(SearchPage searchPage) 
        {
            InitializeComponent();
            MainPage = new AppShell(searchPage); 
        }

    }
}
