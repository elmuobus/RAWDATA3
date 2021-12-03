using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.SiteFunctionsDomain;

namespace WebApi.Services.FunctionalServices
{
    public class SiteFunctionsBusinessLayer : Controller //Controller for stuff that the site uses, but not the user (showing recommended movies and such)
    { //Does not use paging
        private readonly PortfolioContext _ctx;
        public SiteFunctionsBusinessLayer()
        {
            _ctx = new PortfolioContext(); //Had connectionString earlier as tests depended on test environment in IDE being able to recognize/save .env variables
        }

        public List<RecommendedSearchResult> Recommended(string titleId)
        {
            Console.WriteLine("Search Results Recommended");

            var result = _ctx.RecommendedSearchResults.FromSqlInterpolated($"select * from recommended({titleId})");

            List<RecommendedSearchResult> searchResultsCoPlayers = new List<RecommendedSearchResult>();

            foreach (var searchResult in result) //Cannot handle null values. Added question mark, so okay. Now does not print properly.
            {
                Console.WriteLine($"{searchResult.PrimaryTitle}"); //only shows firsts letters for some reason. Just had to change from char to string in object.
                searchResultsCoPlayers.Add(searchResult);
            }
            return searchResultsCoPlayers;
        }


        public List<CoPlayersSearchResult> FindCoPlayers(string actorName)
        {
            Console.WriteLine("Search Results Coplayers");

            var actorsName = actorName;
            var result = _ctx.CoPlayersSearchResults.FromSqlInterpolated($"select * from find_co_players({actorsName})"); ;

            List<CoPlayersSearchResult> searchResultsCoPlayers = new List<CoPlayersSearchResult>();

            foreach (var searchResult in result) //Cannot handle null values. Added question mark, so okay. Now does not print properly.
            {
                Console.WriteLine($"{searchResult.CoPlayerId}, {searchResult.PrimaryName}, {searchResult.Frequency}"); //only shows firsts letters for some reason. Just had to change from char to string in object.
                searchResultsCoPlayers.Add(searchResult);
            }
            return searchResultsCoPlayers;
        }

        public List<CoPlayersSearchResult> FindCoPlayersByID(string id)
        {
            Console.WriteLine("Search Results Coplayers by Id");

            var actorsId = id;

            var result = _ctx.CoPlayersSearchResults.FromSqlInterpolated($"select * from find_co_players_by_id({actorsId})");


            List<CoPlayersSearchResult> searchResultsCoPlayers = new List<CoPlayersSearchResult>();

            foreach (var searchResult in result) //Cannot handle null values. Added question mark, so okay. Now does not print properly.
            {
                Console.WriteLine($"{searchResult.CoPlayerId}, {searchResult.PrimaryName}, {searchResult.Frequency}"); //only shows firsts letters for some reason. Just had to change from char to string in object.
                searchResultsCoPlayers.Add(searchResult);
            }
            return searchResultsCoPlayers;
        }


        public List<PopularActorsInMovieSearchResult> PopularActorsInMovieSearch(string titleId)
        {
            Console.WriteLine("Popular Actors In Movie " + titleId);
            var movieTitleId = titleId;

            var result = _ctx.PopularActorsInMovieSearchResults.FromSqlInterpolated($"select * from popular_actors_in_movie({movieTitleId})"); ;

            List<PopularActorsInMovieSearchResult> searchResultsPopularActorsInMovies = new List<PopularActorsInMovieSearchResult>();

            foreach (var searchResult in result) //Cannot handle null values. Added question mark, so okay. Now does not print properly.
            {
                Console.WriteLine($"{searchResult.Id}, {searchResult.Primaryname}, {searchResult.Rating}"); //only shows firsts letters for some reason. Just had to change from char to string in object.
                searchResultsPopularActorsInMovies.Add(searchResult);
            }
            return searchResultsPopularActorsInMovies;
        }

        /* //we talked about not using it?
        public int FindRating(string actorId) //Probably like this.
        {
            Console.WriteLine("Search Results from Find Rating");

            var result = _ctx.Database.ExecuteSqlInterpolated($"select * from find_rating({actorId})");

            return result;
        }
        */

        public List<PopularActorsCoPlayersSearchResult> SearchForPopularActorsCoPlayers(string actorId)
        {
            Console.WriteLine("Search Results Popular Coplayers by Id");
            //var ctx = new NorthwindContex(connectionString);

            var result = _ctx.PopularActorsCoPlayersSearchResults.FromSqlInterpolated($"select * from popular_actors_co_players({actorId})");


            List<PopularActorsCoPlayersSearchResult> searchResultsPopularActorsCoPlayers = new List<PopularActorsCoPlayersSearchResult>();

            foreach (var searchResult in result)
            {
                Console.WriteLine($"{searchResult.Id}, {searchResult.PrimaryName}, {searchResult.Rating}"); //only shows firsts letters for some reason. Just had to change from char to string in object.
                searchResultsPopularActorsCoPlayers.Add(searchResult);
            }
            return searchResultsPopularActorsCoPlayers;
        }
    }
}
