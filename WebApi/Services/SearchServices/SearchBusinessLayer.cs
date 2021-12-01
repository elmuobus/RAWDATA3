using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.SearchDomain;

namespace WebApi.Services.SearchServices
{
    public class SearchBusinessLayer
    {
        private readonly PortfolioContext _ctx;
        public SearchBusinessLayer()
        {
            _ctx = new PortfolioContext(); //Had connectionString earlier as tests depended on test environment in IDE being able to recognize/save .env variables
        }

        
        public List<ExactMatchSearchResult> ExactMatchSearch(params string[] words) 
        {
            Console.WriteLine("Exact Match");
            var query = "select * from exact_match('" + words[0] + "'";

            for (int i = 1; i < words.Length; i++) 
            {
                query += ", '" + words[i] + "'";
            }
            query += ")";

            Console.WriteLine(query);

            var result = _ctx.ExactMatchSearchResults.FromSqlRaw(query); //SQL injections could be a danger, but had to construct string as we did not know number of arguments. 


            List<ExactMatchSearchResult> searchResultExactMatches = new List<ExactMatchSearchResult>();

            foreach (var searchResult in result)
            {
                Console.WriteLine($"{searchResult.TitleId}, {searchResult.PrimaryTitle}"); 
                searchResultExactMatches.Add(searchResult);
            }

            return searchResultExactMatches;
        }

        public List<BestMatchSearchResult> BestMatchSearch(params string[] words)
        {
            Console.WriteLine("Best Match");
            var query = "select * from best_match('" + words[0] + "'";

            for (int i = 1; i < words.Length; i++)
            {
                query += ", '" + words[i] + "'";
            }
            query += ")";

            Console.WriteLine(query);


           
            var result = _ctx.BestMatchSearchResults.FromSqlRaw(query);

            List<BestMatchSearchResult> searchResultBestMatches = new List<BestMatchSearchResult>();

            foreach (var searchResult in result)
            {
                Console.WriteLine($"{searchResult.TitleId}, {searchResult.PrimaryTitle}, {searchResult.Rank}");
                searchResultBestMatches.Add(searchResult);
            }

            return searchResultBestMatches; //return list of search results.
        }
    }
}