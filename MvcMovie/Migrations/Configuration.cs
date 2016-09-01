namespace MvcMovie.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MvcMovie.Models.Identity.ApplicationDbContext>
    {
        public Configuration()
        {
           AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MvcMovie.Models.Identity.ApplicationDbContext context)
        {
            context.Movies.AddOrUpdate(i => i.Title,
                new Movie
                {
                    Title = "When Harry Met Sally",
                    ReleaseDate = DateTime.Parse("1989-1-11"),
                    Genre = "Romantic Comedy",
                    Rating = "PG",
                    Price = 7.99M,
                    posterLink = "http://static.rogerebert.com/uploads/movie/movie_poster/when-harry-met-sally----1989/large_u0zjs3lmmNizQf7XE0iTh4IG7gX.jpg",
                    DiscountPrice = 3.99M,
                    isFeatured = false,
                    isDiscounted = false
                },

                 new Movie
                 {
                     Title = "Ghostbusters ",
                     ReleaseDate = DateTime.Parse("1984-3-13"),
                     Genre = "Comedy",
                     Rating = "G",
                     Price = 8.99M,
                     posterLink = "https://mdowney25.files.wordpress.com/2012/05/ghostbusters.jpg",
                     DiscountPrice = 3.99M,
                     isFeatured = false,
                     isDiscounted = false
                 },

                 new Movie
                 {
                     Title = "Ghostbusters 2",
                     ReleaseDate = DateTime.Parse("1986-2-23"),
                     Genre = "Comedy",
                     Rating = "G",
                     Price = 9.99M,
                     posterLink = "http://assets.flicks.co.nz/images/movies/poster/97/9718db12cae6be37f7349779007ee589_500x735.jpg",
                     DiscountPrice = 3.99M,
                     isFeatured = false,
                     isDiscounted = false
                 },

               new Movie
               {
                   Title = "Rio Bravo",
                   ReleaseDate = DateTime.Parse("1959-4-15"),
                   Genre = "Western",
                   Rating = "None",
                   Price = 3.99M,
                   posterLink = "http://www.doctormacro.com/Images/Posters/R/Poster%20-%20Rio%20Bravo_01.jpg",
                   DiscountPrice = 3.99M,
                   isFeatured = false,
                   isDiscounted = false
               }
           );
        }
    }
}
