using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;


namespace MvcMovie.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcMovieContext(
                serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>()))
            {
                // Look for any movies.
                if (context.Movie.Any())
                {
                    return;   // DB has been seeded
                }

                context.Movie.AddRange(
                     new Movie
                     {
                         Title = "Brian's Test Movie",
                         ReleaseDate = DateTime.Parse("2042-4-2"),
                         Genre = "Action",
                         Price = 4.29M,
                         Rating = "G"
                     },

                     new Movie
                     {
                         Title = "Wall-E",
                         ReleaseDate = DateTime.Parse("2008-6-27"),
                         Genre = "Comedy",
                         Price = 8.99M,
                         Rating = "G"
                     },

                     new Movie
                     {
                         Title = "La La Land",
                         ReleaseDate = DateTime.Parse("2016-12-9"),
                         Genre = "Comedy",
                         Price = 9.99M,
                         Rating = "PG-13"
                     },

                   new Movie
                   {
                       Title = "Black Panther",
                       ReleaseDate = DateTime.Parse("2018-2-16"),
                       Genre = "Action",
                       Price = 12.99M,
                       Rating = "Black Panther"
                   }
                );
                context.SaveChanges();
            }
        }
    }
}
