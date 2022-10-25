namespace MovieSearch.Models
{
    public class Movie : Media
    {
        public string Genres { get; set; }

        public override string Display()
        {
            return $"Id: {Id}\nTitle: {Title}\nGenres: {Genres}\n";
        }
    }
}