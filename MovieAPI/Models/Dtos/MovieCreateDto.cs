using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.Dtos
{
    public class MovieCreateDto
    {
        [Required]
        [StringLength(30)]
        public string Title { get; set; } = null!;

        [Required]
        [Range(1900, 2025)]
        public int Year { get; set; }

        [Required]
        [StringLength(30)]
        public string Genre { get; set; } = null!;

        [Required]
        [Range(10, 300)]
        public int Duration { get; set; }

        [Required]
        [StringLength(200)]
        public string Synopsis { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string Language { get; set; } = null!;

        [Required]
        [Range(1000, 5000000)]
        public int Budget { get; set; }

    }
}
