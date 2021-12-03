using System;
using System.Collections.Generic;
using System.Linq;
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
        /*
        public List<SimpleSearchResult> SimpleSearch(string title, string user)
        {

        }
        */

        public List<ExactMatchSearchResult> ExactMatchSearch(int nbResult, params string[] words) 
        {
            Console.WriteLine("Exact Match");
            var query = "select * from exact_match('" + words[0] + "'";

            for (int i = 1; i < words.Length; i++) 
            {
                query += ", '" + words[i] + "'";
            }
            query += ")";

            Console.WriteLine(query);

            var result = _ctx.ExactMatchSearchResults.FromSqlRaw(query).Take(nbResult); //SQL injections could be a danger, but had to construct string as we did not know number of arguments. 


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
        
        public List<StructuredActorSearchResult> StructuredActorSearch(string str1, string str2, string str3, string str4)
        {
            Console.WriteLine("Search Results from Structured Actor Search");
            
            //only structured string search needéd user=null for some reason. 
            var result = _ctx.StructuredActorSearchResults.FromSqlInterpolated($"select * from structured_actors_search({str1}, {str2}, {str3}, {str4})");


            List<StructuredActorSearchResult> searchResultsStructuredActorSearches = new List<StructuredActorSearchResult>();

            foreach (var searchResult in result) //Cannot handle null values. Added question mark, so okay. Now does not print properly.
            {
                Console.WriteLine($"{searchResult.NameId}, {searchResult.PrimaryName}"); //only shows firsts letters for some reason. Just had to change from char to string in object.
                searchResultsStructuredActorSearches.Add(searchResult);
            }
            return searchResultsStructuredActorSearches;
        }

        public List<StructuredStringSearchResult> StructuredStringSearch(string userName, string str1, string str2, string str3, string str4)
        {
            Console.WriteLine("Search Results from Structured String Search");
            


            var result = _ctx.StructuredStringSearchResults.FromSqlInterpolated($"select * from structured_string_search({userName}, {str1}, {str2}, {str3}, {str4})");


            List<StructuredStringSearchResult> searchResultsStringSearches = new List<StructuredStringSearchResult>();

            foreach (var searchResult in result) //Cannot handle null values. Added question mark, so okay. Now does not print properly.
            {
                Console.WriteLine($"{searchResult.TitleId}, {searchResult.PrimaryTitle}"); //only shows firsts letters for some reason. Just had to change from char to string in object.
                searchResultsStringSearches.Add(searchResult);
            }
            return searchResultsStringSearches;
        }
        
    }
}