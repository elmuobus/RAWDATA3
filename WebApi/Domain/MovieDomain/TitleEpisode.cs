namespace WebApi.Domain.MovieDomain
{
    public class TitleEpisode
    {
        public string Id { get; set; }
        public string TitleId { get; set; }
        public int? SeasonNumber { get; set; }
        public int? EpisodeNumber { get; set; }
        public TitleBasics TitleBasics { get; set; }
    }
}