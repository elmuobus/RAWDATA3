﻿namespace WebApi.Domain.MovieDomain
{
    public class OmdbData
    {
        public string Id { get; set; }
        public string Poster { get; set; }
        public string Awards { get; set; }
        public string Plot { get; set; }
        
        public TitleBasics TitleBasics { get; set; }
    }
}
