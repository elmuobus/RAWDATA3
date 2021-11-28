namespace WebApi.Domain.MovieDomain
{
    public class TitleCrew
    {
        public string Id { get; set; }
        public string Directors { get; set; }
        public string Writers { get; set; }
        public TitleBasics TitleBasics { get; set; }
    }
}