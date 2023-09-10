using MoviesZone.Models;
using System.Collections.Generic;
using System.Linq;

namespace MoviesZone.Services
{
    public class GenreRepo : IGenreRepo
    {
        private readonly MovieAppContext _context;
        public GenreRepo(MovieAppContext context)
        {
            _context = context;       
        }
       

        public List<Genres> GetAll()
        {
            return _context.Genre.OrderBy(g=>g.Name).ToList();
        }

        public Genres GetDetails(int id)
        {
            return _context.Genre.Find(id);
        }

       
    }
}
