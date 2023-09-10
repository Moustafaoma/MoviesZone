using MoviesZone.Models;
using System.Collections.Generic;

namespace MoviesZone.Services
{
    public interface IGenreRepo
    {
        public List<Genres> GetAll();
        public Genres GetDetails(int id);
        
    }
}
