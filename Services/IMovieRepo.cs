using MoviesZone.Models;
using System.Collections.Generic;
using MoviesZone.Models.DTOS;
using System.IO;

namespace MoviesZone.Services
{
    public interface IMovieRepo
    {
        public  List <Movies> GetAll();
        public Movies GetDetails(int? id);
        public  void Create(ViewModelMovieForm movie, MemoryStream datastream);
        public void Update(Movies selectedMovie,ViewModelMovieForm movie);
        public void Delete(Movies movie);



    }
}
