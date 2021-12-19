namespace WebApi.ViewModels.Movie
{
    public class TitleBasicsViewModel
    {
        public string TitleType { get; set; }
        public string Id { get; set; }
        public string PrimaryTitle { get; set; }
        public string OriginalTitle { get; set; }
        public string Genres { get; set; }
        public string Poster { get; set; }
        public float? Rating { get; set; }
        public int? RuntimeMinutes { get; set; }
        public string StartYear { get; set; }
        public string EndYear { get; set; }
        public bool IsAdult { get; set; }
        public string Plot { get; set; }
    }
}
