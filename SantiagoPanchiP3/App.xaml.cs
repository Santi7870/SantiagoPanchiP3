using SantiagoPanchiP3.Views;

namespace SantiagoPanchiP3
{
    public partial class App : Application
    {
        public App(SearchPage searchPage) //  Inyectar SearchPage
        {
            InitializeComponent();
            MainPage = new AppShell(searchPage); //  Pasar SearchPage a AppShell
        }

    }
}
