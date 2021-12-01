namespace WebApi.Domain.SearchDomain
{
    public class BestMatchSearchResult
    {
        public string TitleId { get; set; }
        public int Rank { get; set; }
        public string PrimaryTitle { get; set; }
    }
}