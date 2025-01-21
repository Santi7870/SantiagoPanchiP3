using SQLite;

namespace SantiagoPanchiP3.Models
{
    public class Movie
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genero { get; set; }
        public string ActorPrincipal { get; set; }
        public string Awards { get; set; }
        public string Website { get; set; }
        public string Spanchi { get; set; }
    }
}
