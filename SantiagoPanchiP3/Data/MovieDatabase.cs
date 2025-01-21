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

        // Constructor que inicializa la conexi�n con la base de datos
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

        // M�todo para guardar una pel�cula en la base de datos
        public async Task<int> SaveMovieAsync(Movie movie)
        {
            try
            {
                return await _database.InsertAsync(movie);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar la pel�cula: {ex.Message}");
                return 0;
            }
        }

        // M�todo para obtener todas las pel�culas almacenadas
        public async Task<List<Movie>> GetMoviesAsync()
        {
            try
            {
                return await _database.Table<Movie>().ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener pel�culas: {ex.Message}");
                return new List<Movie>();
            }
        }

        // M�todo para buscar una pel�cula por t�tulo
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
                Console.WriteLine($"Error al buscar la pel�cula: {ex.Message}");
                return null;
            }
        }

        // M�todo para eliminar una pel�cula de la base de datos
        public async Task<int> DeleteMovieAsync(Movie movie)
        {
            try
            {
                return await _database.DeleteAsync(movie);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar la pel�cula: {ex.Message}");
                return 0;
            }
        }

        // M�todo para actualizar los datos de una pel�cula
        public async Task<int> UpdateMovieAsync(Movie movie)
        {
            try
            {
                return await _database.UpdateAsync(movie);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar la pel�cula: {ex.Message}");
                return 0;
            }
        }
    }
}

