using Microsoft.EntityFrameworkCore;
using MoviesZone.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MoviesZone.Models.DTOS;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace MoviesZone.Services
{
    public class MovieRepo : IMovieRepo
    {
        private readonly MovieAppContext _context;

        public MovieRepo(MovieAppContext context)
        {
            _context = context;
        }
        public void Create(ViewModelMovieForm movie,MemoryStream datastream)
        {
            Movies movies = new Movies()
            {
                Title = movie.Title,
                Rate = movie.Rate,
                Year = movie.Year,
                StoryLine = movie.StoryLine,
                GenreID = movie.GenreID,
                Poster = datastream.ToArray()
            };
            _context.Movie.Add(movies);
            _context.SaveChanges();
        }

        public void Delete(Movies movie)
        {
            _context.Movie.Remove(movie);
            _context.SaveChanges();
        }

        public List<Movies>  GetAll()
        {
            return _context.Movie.OrderByDescending(m => m.Rate).ToList();
        }

        public Movies GetDetails(int?id)
        {
            return _context.Movie.SingleOrDefault(m => m.MoviesId == id);
        }

        public void Update(Movies selectedMovie,  ViewModelMovieForm movie)
        {
            selectedMovie.Title = movie.Title;
            selectedMovie.Rate = movie.Rate;
            selectedMovie.Year = movie.Year;
            selectedMovie.StoryLine = movie.StoryLine;
            selectedMovie.GenreID = movie.GenreID;
            _context.SaveChanges();
        }
        
       
    }
}
