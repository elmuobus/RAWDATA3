using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApi.Domain;
using WebApi.Domain.UserDomain;
using WebApi.Domain.MovieDomain;

namespace WebApi
{
    public class PortfolioContext: DbContext
    {
        public DbSet<TitleBasics> TitleBasics { get; set; }
        public DbSet<NameBasics> NameBasics { get; set; }
        public DbSet<OmdbData> OmdbDatas { get; set; }
        public DbSet<Wi> Wi { get; set; }
        public DbSet<TitleEpisode> TitleEpisodes { get; set; }
        public DbSet<TitleAkas> TitleAkas { get; set; }
        public DbSet<TitleCrew> TitleCrews { get; set; }
        public DbSet<TitlePrincipals> TitlePrincipals { get; set; }
        public DbSet<TitleRatings> TitleRatings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<SearchHistory> SearchHistories { get; set; }
        public DbSet<TitleBookmark> TitleBookmarks { get; set; }
        public DbSet<NameBookmark> NameBookmarks { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseNpgsql($"host={Environment.GetEnvironmentVariable("HOST")};" +
                                     $"db={Environment.GetEnvironmentVariable("DB")};" +
                                     $"uid={Environment.GetEnvironmentVariable("UID")};" +
                                     $"pwd={Environment.GetEnvironmentVariable("PWD")}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //movie schema
            modelBuilder.Entity<TitleBasics>().ToTable("titlebasics", "movie");
            modelBuilder.Entity<TitleBasics>()
                .HasOne(t => t.OmdbData).WithOne(o => o.TitleBasics)
                .HasForeignKey<TitleBasics>(t => t.Id).IsRequired(false);
            modelBuilder.Entity<TitleBasics>()
                .HasMany(t => t.ListTitleAkas).WithOne(o => o.TitleBasics)
                .HasForeignKey(t => t.TitleId).IsRequired(false);
            modelBuilder.Entity<TitleBasics>()
                .HasMany(t => t.Wis).WithOne(o => o.TitleBasics)
                .HasForeignKey(t => t.TitleId).IsRequired(false);
            modelBuilder.Entity<TitleBasics>()
                .HasOne(t => t.TitleRatings).WithOne(o => o.TitleBasics)
                .HasForeignKey<TitleBasics>(t => t.Id).IsRequired(false);
            modelBuilder.Entity<TitleBasics>()
                .HasOne(t => t.TitleCrew).WithOne(o => o.TitleBasics)
                .HasForeignKey<TitleBasics>(t => t.Id).IsRequired(false);
            modelBuilder.Entity<TitleBasics>()
                .HasMany(t => t.ListTitlePrincipals).WithOne(o => o.TitleBasics)
                .HasForeignKey(t => t.TitleId).IsRequired(false);
            modelBuilder.Entity<TitleBasics>()
                .HasMany(t => t.TitleEpisodes).WithOne(o => o.TitleBasics)
                .HasForeignKey(t => t.TitleId).IsRequired(false);
            modelBuilder.Entity<TitleBasics>().Property(x => x.Id).HasColumnName("titleid");
            modelBuilder.Entity<TitleBasics>().Property(x => x.TitleType).HasColumnName("titletype");
            modelBuilder.Entity<TitleBasics>().Property(x => x.PrimaryTitle).HasColumnName("primarytitle");
            modelBuilder.Entity<TitleBasics>().Property(x => x.OriginalTitle).HasColumnName("originaltitle");
            modelBuilder.Entity<TitleBasics>().Property(x => x.IsAdult).HasColumnName("isadult");
            modelBuilder.Entity<TitleBasics>().Property(x => x.StartYear).HasColumnName("startyear");
            modelBuilder.Entity<TitleBasics>().Property(x => x.EndYear).HasColumnName("endyear");
            modelBuilder.Entity<TitleBasics>().Property(x => x.RuntimeMinutes).HasColumnName("runtimeminutes");
            modelBuilder.Entity<TitleBasics>().Property(x => x.Genres).HasColumnName("genres");

            modelBuilder.Entity<TitlePrincipals>().ToTable("titleprincipals", "movie");
            modelBuilder.Entity<TitlePrincipals>().HasKey(x => new { x.TitleId, x.Ordering, x.NameId });
            modelBuilder.Entity<TitlePrincipals>().Property(x => x.TitleId).HasColumnName("titleid");
            modelBuilder.Entity<TitlePrincipals>().Property(x => x.Ordering).HasColumnName("ordering");
            modelBuilder.Entity<TitlePrincipals>().Property(x => x.NameId).HasColumnName("nameid");
            modelBuilder.Entity<TitlePrincipals>().Property(x => x.Category).HasColumnName("category");
            modelBuilder.Entity<TitlePrincipals>().Property(x => x.Job).HasColumnName("job");
            modelBuilder.Entity<TitlePrincipals>().Property(x => x.Characters).HasColumnName("characters");

            modelBuilder.Entity<TitleCrew>().ToTable("titlecrew", "movie");
            modelBuilder.Entity<TitleCrew>().Property(x => x.Id).HasColumnName("titleid");
            modelBuilder.Entity<TitleCrew>().Property(x => x.Directors).HasColumnName("directors");
            modelBuilder.Entity<TitleCrew>().Property(x => x.Writers).HasColumnName("writers");

            modelBuilder.Entity<TitleAkas>().ToTable("titleakas", "movie");
            modelBuilder.Entity<TitleAkas>().HasKey(x => new { x.TitleId, x.Ordering });
            modelBuilder.Entity<TitleAkas>().Property(x => x.TitleId).HasColumnName("titleid");
            modelBuilder.Entity<TitleAkas>().Property(x => x.Ordering).HasColumnName("ordering");
            modelBuilder.Entity<TitleAkas>().Property(x => x.Title).HasColumnName("title");
            modelBuilder.Entity<TitleAkas>().Property(x => x.Region).HasColumnName("region");
            modelBuilder.Entity<TitleAkas>().Property(x => x.Language).HasColumnName("language");
            modelBuilder.Entity<TitleAkas>().Property(x => x.Types).HasColumnName("types");
            modelBuilder.Entity<TitleAkas>().Property(x => x.Attributes).HasColumnName("attributes");
            modelBuilder.Entity<TitleAkas>().Property(x => x.IsOriginalTitle).HasColumnName("isoriginaltitle");

            modelBuilder.Entity<TitleEpisode>().ToTable("titleepisode", "movie");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.Id).HasColumnName("episodeid");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.TitleId).HasColumnName("titleid");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.SeasonNumber).HasColumnName("seasonnumber");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.EpisodeNumber).HasColumnName("episodenumber");

            modelBuilder.Entity<TitleRatings>().ToTable("titleratings", "movie");
            modelBuilder.Entity<TitleRatings>().Property(x => x.Id).HasColumnName("titleid");
            modelBuilder.Entity<TitleRatings>().Property(x => x.AverageRating).HasColumnName("averagerating");
            modelBuilder.Entity<TitleRatings>().Property(x => x.NumVotes).HasColumnName("numvotes");

            modelBuilder.Entity<NameBasics>().ToTable("namebasics", "movie");
            modelBuilder.Entity<NameBasics>().Property(x => x.Id).HasColumnName("nameid");
            modelBuilder.Entity<NameBasics>().Property(x => x.PrimaryName).HasColumnName("primaryname");
            modelBuilder.Entity<NameBasics>().Property(x => x.BirthYear).HasColumnName("birthyear");
            modelBuilder.Entity<NameBasics>().Property(x => x.DeathYear).HasColumnName("deathyear");
            modelBuilder.Entity<NameBasics>().Property(x => x.PrimaryProfession).HasColumnName("primaryprofession");
            modelBuilder.Entity<NameBasics>().Property(x => x.KnownForTitles).HasColumnName("knownfortitles");

            modelBuilder.Entity<Wi>().ToTable("wi", "movie");
            modelBuilder.Entity<Wi>().HasKey(x => new { x.TitleId, x.Word, x.Field });
            modelBuilder.Entity<Wi>().Property(x => x.TitleId).HasColumnName("titleid");
            modelBuilder.Entity<Wi>().Property(x => x.Word).HasColumnName("word");
            modelBuilder.Entity<Wi>().Property(x => x.Field).HasColumnName("field");
            modelBuilder.Entity<Wi>().Property(x => x.Lexeme).HasColumnName("lexeme");

            modelBuilder.Entity<OmdbData>().ToTable("omdb_data", "movie");
            modelBuilder.Entity<OmdbData>()
                .HasOne(t => t.TitleBasics).WithOne(o => o.OmdbData)
                .HasForeignKey<TitleBasics>(t => t.Id);
            modelBuilder.Entity<OmdbData>().Property(x => x.Id).HasColumnName("titleid");
            modelBuilder.Entity<OmdbData>().Property(x => x.Poster).HasColumnName("poster");
            modelBuilder.Entity<OmdbData>().Property(x => x.Awards).HasColumnName("awards");
            modelBuilder.Entity<OmdbData>().Property(x => x.Plot).HasColumnName("plot");

            
            //user schema
            modelBuilder.Entity<User>().ToTable("user", "user");
            modelBuilder.Entity<User>().HasKey(x => x.Username);
            modelBuilder.Entity<User>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<User>().Property(x => x.Password).HasColumnName("password");
            modelBuilder.Entity<User>().Property(x => x.Salt).HasColumnName("salt");
            modelBuilder.Entity<User>().Property(x => x.IsAdmin).HasColumnName("isadmin");
            modelBuilder.Entity<User>().Property(x => x.IsAdult).HasColumnName("isadult");
            
            modelBuilder.Entity<Rating>().ToTable("ratings", "user");
            modelBuilder.Entity<Rating>().HasKey(x => new { x.Username, x.TitleId });
            modelBuilder.Entity<Rating>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<Rating>().Property(x => x.TitleId).HasColumnName("titleid");
            modelBuilder.Entity<Rating>().Property(x => x.Rate).HasColumnName("rate");
            modelBuilder.Entity<Rating>().Property(x => x.Comment).HasColumnName("comment");
            
            modelBuilder.Entity<SearchHistory>().ToTable("searchhistory", "user");
            modelBuilder.Entity<SearchHistory>().HasKey(x => new { x.Username, x.SearchKey });
            modelBuilder.Entity<SearchHistory>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<SearchHistory>().Property(x => x.SearchKey).HasColumnName("searchkey");
            
            modelBuilder.Entity<TitleBookmark>().ToTable("titlebookmark", "user");
            modelBuilder.Entity<TitleBookmark>().HasKey(x => new { x.Username, x.TitleId });
            modelBuilder.Entity<TitleBookmark>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<TitleBookmark>().Property(x => x.TitleId).HasColumnName("titleid");
            
            modelBuilder.Entity<NameBookmark>().ToTable("namebookmark", "user");
            modelBuilder.Entity<NameBookmark>().HasKey(x => new { x.Username, x.NameId });
            modelBuilder.Entity<NameBookmark>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<NameBookmark>().Property(x => x.NameId).HasColumnName("nameid");

            
        }
    }
}
