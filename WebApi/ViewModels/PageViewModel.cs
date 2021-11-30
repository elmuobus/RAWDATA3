using System.Collections.Generic;

namespace WebApi.ViewModels
{
    public class PageViewModel<T>
    {
        public int TotalPage { get; set; }
        public int? Previous { get; set; }
        public int Current { get; set; }
        public int? Next { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}