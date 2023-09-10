using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesZone.Models
{
    public class Movies
    {
        public int MoviesId { get; set; }
        [Required,MaxLength(250)]
        public string Title { get; set; }
        [Range(1900,2050)]
        public int Year { get; set; }

        [Range(0,10)]
        public double Rate { get; set; }
        [Required,MaxLength(2500)]
        public string StoryLine { get; set; }
        [Required]
        public byte[]Poster { get; set; }
        public byte GenreID { get; set; }
        public virtual Genres genre { get; set; }

    }
}
