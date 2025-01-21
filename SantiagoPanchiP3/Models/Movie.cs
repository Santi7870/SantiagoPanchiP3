using SQLite;

namespace SantiagoPanchiP3.Models
{
    public class Movie
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public List<string> Genre { get; set; }
        public List<string> Actors { get; set; }


        public string Awards { get; set; }
        public string Website { get; set; }
        public string Spanchi { get; set; }


        // Campos adicionales para almacenar solo el primer elemento
        public string FirstGenre { get; set; }
        public string FirstActor { get; set; }


    }
}
