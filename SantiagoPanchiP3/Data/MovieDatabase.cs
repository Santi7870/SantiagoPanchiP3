using SQLite;
using System.Threading.Tasks;
using System.Collections.Generic;
using SantiagoPanchiP3.Models;
using System;

namespace SantiagoPanchiP3.Data
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Movie>().Wait(); // Asegura que la tabla existe
        }

        // M�todo para guardar una pel�cula en la base de datos
        public async Task SaveMovieAsync(Movie movie)
        {
            await _database.InsertAsync(movie);
        }

        //  M�todo para obtener todas las pel�culas guardadas
        public async Task<List<Movie>> GetMoviesAsync()
        {
            return await _database.Table<Movie>().ToListAsync();
        }
    }
}

