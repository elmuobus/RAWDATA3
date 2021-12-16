namespace WebApi.ViewModels.ListViewModel.User
{
    public class TitleBookmarkListViewModel
    {
        public string Url { get; set; }
        public string TitleId { get; set; }
        
        public string OriginalTitle { get; set; }
        public string Genres { get; set; }
        public string Poster { get; set; }
        public string Plot { get; set; }
        public float? Rating { get; set; }
    }
}