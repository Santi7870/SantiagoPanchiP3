using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using SantiagoPanchiP3.Models;
using System.Threading.Tasks;

namespace SantiagoPanchiP3.Data
{
    public class MovieService
    {
        private readonly HttpClient _httpClient;

        // Inyección de dependencia
        public MovieService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Movie> SearchMovieAsync(string query)
        {
            try
            {
                string url = $"https://freetestapi.com/api/v1/movies?search={query}";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error en la API: {response.StatusCode}");
                    return null;
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Respuesta JSON: {jsonResponse}");

                var movies = JsonSerializer.Deserialize<List<MovieResponse>>(jsonResponse);

                if (movies == null || movies.Count == 0)
                {
                    Console.WriteLine("No se encontraron películas.");
                    return null;
                }

                var movie = movies[0];

                return new Movie
                {
                    Title = movie.title,
                    Genre = movie.genre?.FirstOrDefault() ?? "N/A",  // <-- Aquí se cambia a "Genre"
                    Actor = movie.actors?.FirstOrDefault() ?? "N/A",
                    Awards = movie.awards,
                    Website = movie.website



                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en SearchMovieAsync: {ex.Message}");
                return null;
            }
        }


    }

    public class MovieResponse
    {
        public string title { get; set; }
        public List<string> genre { get; set; }
        public List<string> actors { get; set; }
        public string awards { get; set; }
        public string website { get; set; }
    }
}
