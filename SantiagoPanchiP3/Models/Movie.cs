using SQLite;

namespace SantiagoPanchiP3.Models
{
    public class Movie
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }

        public string Genre { get; set; }
        public string Actor { get; set; }


        public string Awards { get; set; }
        public string Website { get; set; }
        public string Spanchi { get; set; }




    }
}
