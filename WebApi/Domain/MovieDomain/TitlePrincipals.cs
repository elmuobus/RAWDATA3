namespace WebApi.Domain.MovieDomain
{
    public class TitlePrincipals
    {
        public string TitleId { get; set; }
        public int Ordering { get; set; }
        public string NameId { get; set; }
        public string Category { get; set; }
        public string Job { get; set; }
        public string Characters { get; set; }
        public TitleBasics TitleBasics { get; set; }
    }
}