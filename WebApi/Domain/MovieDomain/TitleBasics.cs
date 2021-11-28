using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Domain.MovieDomain
{
    public class TitleBasics
    {
        public string Id { get; set; }
        public string TitleType { get; set; }
        public string PrimaryTitle { get; set; }
        public string OriginalTitle { get; set; }
        public bool IsAdult { get; set; }
        public string StartYear { get; set; }
        public string EndYear { get; set; }
        public int? RuntimeMinutes { get; set; }
        public string Genres { get; set; }
        public ICollection<TitleAkas> ListTitleAkas { get; set; }
        public ICollection<Wi> Wis { get; set; }
        public TitleRatings TitleRatings { get; set; }
        public OmdbData OmdbData { get; set; }
        public ICollection<TitlePrincipals> ListTitlePrincipals { get; set; }
        public TitleCrew TitleCrew { get; set; }
        public ICollection<TitleEpisode> TitleEpisodes { get; set; }
    }
}
