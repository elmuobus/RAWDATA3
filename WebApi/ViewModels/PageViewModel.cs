using System.Collections.Generic;

namespace WebApi.ViewModels
{
    public class PageViewModel<T>
    {
        public int Total { get; set; }
        public string Previous { get; set; }
        public string Current { get; set; }
        public string Next { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}