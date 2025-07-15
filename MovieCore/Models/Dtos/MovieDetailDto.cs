using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.Dtos
{
    public class MovieDetailDto
    {
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public string Genre { get; set; } = null!;
        public int Duration { get; set; }

        public string Synopsis { get; set; } = null!;
        public string Language { get; set; } = null!;
        public int Budget { get; set; }

        public List<ReviewDto> Comments { get; set; } = null!;

        public List<ActorDto> Actors { get; set; } = null!;
    }
}
