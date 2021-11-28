﻿namespace WebApi.Domain.MovieDomain
{
    public class TitleAkas
    {
        public string TitleId { get; set; }
        public int Ordering { get; set; }
        public string Title { get; set; }
        public string Region { get; set; }
        public string Language { get; set; }
        public string Types { get; set; }
        public string Attributes { get; set; }
        public bool IsOriginalTitle { get; set; }
        
        public TitleBasics TitleBasics { get; set; }
    }
}
