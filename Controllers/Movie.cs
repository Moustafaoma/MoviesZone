using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MoviesZone.Models;
using MoviesZone.Models.DTOS;
using MoviesZone.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.WebRequestMethods;

namespace MoviesZone.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieRepo movie;
        private readonly IGenreRepo genre;
        private readonly INotyfService _notyf;
        private readonly List<string> allowedPathFiles = new List<string> { ".jpg", ".png" };
       private readonly long maxValueLengthFiles= 1048576;
    public MovieController(IMovieRepo movie, IGenreRepo genre, INotyfService notyf)
        {
            this.movie = movie;
            this.genre = genre;
            _notyf = notyf;

        }
        public IActionResult Index()
        {
            return View(movie.GetAll());
        }
        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new ViewModelMovieForm()
            {
                genres = genre.GetAll()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ViewModelMovieForm model)
        {
            if(!ModelState.IsValid)
            {
                model.genres = genre.GetAll();
                return View(model);
            }
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null)
            {
                model. genres = genre.GetAll();
                ModelState.AddModelError("Poster", "Please Select Poster");
                return View(model);
            }
            if (!allowedPathFiles.Contains(Path.GetExtension(file.FileName.ToLower())))
            {
                model.genres = genre.GetAll();
                ModelState.AddModelError("Poster", "Please Select Poster(.png or .jpg)!");
                return View(model);
            }
            if (file.Length > maxValueLengthFiles)
            {
                model.genres = genre.GetAll();
                ModelState.AddModelError("Poster", "The poster cannot be more than 1MB;");
                return View(model);
            }
            using var dataStream = new MemoryStream();
            file.CopyTo(dataStream);
            movie.Create(model,dataStream);
            _notyf.Success("Movie created Successfully");
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int?id)
        {
            if (id == null)
                return BadRequest();
            var selectedMovie = movie.GetDetails(id);
            if (selectedMovie==null)
            {
                return NotFound();
            }
            var Viewmodel = new ViewModelMovieForm()
            {
                ID=selectedMovie.MoviesId,
                Title = selectedMovie.Title,
                GenreID = selectedMovie.GenreID,
                genres = genre.GetAll(),
                Rate=selectedMovie.Rate,
                StoryLine=selectedMovie.StoryLine,
                Poster=selectedMovie.Poster,
                Year=selectedMovie.Year,
            };
            return View(Viewmodel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int?id,ViewModelMovieForm model)
        {
            if (!ModelState.IsValid)
            {
                model.genres = genre.GetAll();
                return View(model);
            }
            var selectedMovie = movie.GetDetails(id);
            if (selectedMovie == null)
            {
                model.genres = genre.GetAll();
                return NotFound();
            }
            var files = Request.Form.Files;
            if (files.Any())
            {
                var poster = files.FirstOrDefault();
                using var dataStream = new MemoryStream();
                poster.CopyTo(dataStream);
                model.Poster = dataStream.ToArray();
                if (!allowedPathFiles.Contains(Path.GetExtension(poster.FileName.ToLower())))
                {
                    model.genres = genre.GetAll();
                    ModelState.AddModelError("Poster", "Please Select Poster(.png or .jpg)!");
                    return View(model);
                }
                if (poster.Length > maxValueLengthFiles)
                {
                    model.genres = genre.GetAll();
                    ModelState.AddModelError("Poster", "The poster cannot be more than 1MB;");
                    return View(model);
                }
                selectedMovie.Poster =  model.Poster;
            }
            movie.Update(selectedMovie,model);
            _notyf.Success("Movie Updated Successfully");
            return RedirectToAction("Index");
        }
        public IActionResult Details(int?id)
        {
            if (id == null)
                return BadRequest();
            var selectedMovie=movie.GetDetails(id);
            if(selectedMovie == null)
                return NotFound();
            return View(selectedMovie);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            var selectedMovie = movie.GetDetails(id);
            if (selectedMovie == null)
                return NotFound();
            return View(selectedMovie);
        }
        [HttpPost]
        public IActionResult DeleteItem(int ?id)
        {
            var selectedMovie = movie.GetDetails(id);
            movie.Delete(selectedMovie);
            _notyf.Success("Movie Deleted Successfully");
            return RedirectToAction("Index");   

        }

    }
}

