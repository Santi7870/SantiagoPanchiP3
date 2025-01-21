using SantiagoPanchiP3;
using SantiagoPanchiP3.Data;
using SantiagoPanchiP3.ViewModels;
using SantiagoPanchiP3.Views;
using Microsoft.Extensions.DependencyInjection;


using Microsoft.Extensions.Logging;

namespace SantiagoPanchiP3
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "movies.db");

            builder.Services.AddHttpClient<MovieService>(client =>
            {
                client.BaseAddress = new Uri("https://freetestapi.com/api/v1/");
            });

            builder.Services.AddSingleton<DatabaseService>(s => new DatabaseService(dbPath));

            builder.Services.AddSingleton<SearchViewModel>(s =>
                new SearchViewModel(
                    s.GetRequiredService<MovieService>(),
                    s.GetRequiredService<DatabaseService>()
                ));

            builder.Services.AddSingleton<MovieListViewModel>(s =>
                new MovieListViewModel(
                    s.GetRequiredService<DatabaseService>()
                ));

            builder.Services.AddSingleton<SearchPage>();
            builder.Services.AddSingleton<MovieListPage>();

            builder.Services.AddSingleton<AppShell>();

            return builder.Build();
        }
    }
}
