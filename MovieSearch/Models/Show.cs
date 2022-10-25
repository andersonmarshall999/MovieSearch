namespace MovieSearch.Models
{
    public class Show : Media
    {
        public int Season { get; set; }
        public int Episode { get; set; }
        public string Writers { get; set; }

        public override string Display()
        {
            return $"Id: {Id}\nTitle: {Title}\nSeason {Season}, Episode {Episode}\nWriters: {Writers}\n";
        }
    }
}