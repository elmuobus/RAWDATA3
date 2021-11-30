using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    public class APagesController: ControllerBase
    {
        public class PagesQueryString
        {
            private int _pageSize = 10;
            public int Page { get; set; } = 0;

            public int PageSize
            {
                get => _pageSize;
                set => _pageSize = value > MaxPageSize ? MaxPageSize: value;
            }

            private int MaxPageSize { get; } = 25;
        }
        private readonly LinkGenerator _linkGenerator;

        public APagesController(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        protected PageViewModel<T> CreatePagingResult<T>(int page, int pageSize, int total, IEnumerable<T> model, string uriName)
        {
            var lastPage = (int)Math.Floor(total / (double)pageSize);
            var urlGenerator = GenerateGetUrlWithPage(uriName);

            return new PageViewModel<T>()
            {
                TotalPage = lastPage,
                PageSize = pageSize,
                Previous = page > 0 ? page - 1 : null,
                Current = page,
                Next = page < lastPage ? page + 1 : null,
                Items = model
            };
        }
        
        private Func<int, int, int, string> GenerateGetUrlWithPage(string uriName)
        {
            return (page, pageSize, lastPage) =>
            {
                if (page < 0 || page > lastPage)
                    return null;
                return _linkGenerator.GetUriByName(HttpContext, uriName, new { page, pageSize });
            };
        }

        protected string GetUrlObject(string uriName, object values)
        {
            return _linkGenerator.GetUriByName(HttpContext, uriName, values);
        }
    }
}