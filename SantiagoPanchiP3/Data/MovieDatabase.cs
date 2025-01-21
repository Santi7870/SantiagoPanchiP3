using SQLite;
using System.Threading.Tasks;
using System.Collections.Generic;
using SantiagoPanchiP3.Models;
using System;

namespace SantiagoPanchiP3.Data
{
    public class MovieDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        // Constructor que inicializa la conexión con la base de datos
        public MovieDatabase(string dbPath)
        {
            try
            {
                _database = new SQLiteAsyncConnection(dbPath);
                _database.CreateTableAsync<Movie>().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear la tabla: {ex.Message}");
            }
        }

        // Método para guardar una película en la base de datos
        public async Task<int> SaveMovieAsync(Movie movie)
        {
            try
            {
                return await _database.InsertAsync(movie);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar la película: {ex.Message}");
                return 0;
            }
        }

        // Método para obtener todas las películas almacenadas
        public async Task<List<Movie>> GetMoviesAsync()
        {
            try
            {
                return await _database.Table<Movie>().ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener películas: {ex.Message}");
                return new List<Movie>();
            }
        }

        // Método para buscar una película por título
        public async Task<Movie> GetMovieByTituloAsync(string titulo)
        {
            try
            {
                return await _database.Table<Movie>()
                    .Where(m => m.Titulo == titulo)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al buscar la película: {ex.Message}");
                return null;
            }
        }

        // Método para eliminar una película de la base de datos
        public async Task<int> DeleteMovieAsync(Movie movie)
        {
            try
            {
                return await _database.DeleteAsync(movie);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar la película: {ex.Message}");
                return 0;
            }
        }

        // Método para actualizar los datos de una película
        public async Task<int> UpdateMovieAsync(Movie movie)
        {
            try
            {
                return await _database.UpdateAsync(movie);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar la película: {ex.Message}");
                return 0;
            }
        }
    }
}

