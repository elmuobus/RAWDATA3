namespace WebApi.Domain.UserDomain
{
    public class Rating
    {
        public string Username { get; set; }
        public string TitleId { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
    }
}