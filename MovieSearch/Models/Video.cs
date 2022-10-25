namespace MovieSearch.Models
{
    public class Video : Media
    {
        public string Format { get; set; }
        public int Length { get; set; }
        public string Regions { get; set; }

        public override string Display()
        {
            return $"Id: {Id}\nTitle: {Title}\nFormat: {Format}\nLength: {Length}\nRegions: {Regions}\n";
        }
    }
}