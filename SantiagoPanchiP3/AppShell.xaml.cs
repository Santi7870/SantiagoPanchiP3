using SantiagoPanchiP3.Views;

namespace SantiagoPanchiP3
{
    public partial class AppShell : Shell
    {
        public AppShell(SearchPage searchPage) // Constructor con parámetro
        {
            InitializeComponent();

            // 🔹 Agregar SearchPage manualmente
            Items.Add(new ShellContent
            {
                Title = "Buscar",
                Content = searchPage
            });
        }

    }
}
