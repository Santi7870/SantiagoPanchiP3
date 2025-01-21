using SantiagoPanchiP3.Views;

namespace SantiagoPanchiP3
{
    public partial class AppShell : Shell
    {
        public AppShell(SearchPage searchPage) 
        {
            InitializeComponent();

            
            Items.Add(new ShellContent
            {
                Title = "Buscar",
                Content = searchPage
            });
        }

    }
}
