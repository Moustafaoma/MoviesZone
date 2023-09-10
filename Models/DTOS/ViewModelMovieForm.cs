using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesZone.Models.DTOS
{
    public class ViewModelMovieForm
    {
        public int ID { get; set; }
        [Required, StringLength(250)]
        public string Title { get; set; }
        [Range(1900, 2050)]
        public int Year { get; set; }

        [Range(0, 10)]
        public double Rate { get; set; }
        [Required, StringLength(2500)]
        public string StoryLine { get; set; }
        [Display(Name ="Choose Poster")]
        public byte[] Poster { get; set; }
        [Display(Name ="Genre")]
        public byte GenreID { get; set; }
        public IEnumerable<Genres> genres { get; set; }

    }
}
