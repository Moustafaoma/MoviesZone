using Microsoft.EntityFrameworkCore;

namespace MoviesZone.Models
{
    public class MovieAppContext:DbContext
    {
        public MovieAppContext(DbContextOptions<MovieAppContext> options):base(options)
        {
                
        }
        public DbSet<Movies> Movie { get; set; }
        public DbSet<Genres> Genre { get; set; }
    }
}
