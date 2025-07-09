using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models
{
    public class MovieDetails
    {
        public int Id { get; set; }
        public string Synopsis { get; set; } = null!;
        public string Language { get; set; } = null!;
        public int Budget { get; set; }

        public int MovieId { get; set; }

        // Navigation prop
        public Movie? Movie { get; set; }
    }
}
