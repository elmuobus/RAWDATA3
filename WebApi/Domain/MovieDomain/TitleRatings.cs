namespace WebApi.Domain.MovieDomain
{
    public class TitleRatings
    {
        public string Id { get; set; }
        public float AverageRating { get; set; }
        public int NumVotes { get; set; }
        public TitleBasics TitleBasics { get; set; }
    }
}
