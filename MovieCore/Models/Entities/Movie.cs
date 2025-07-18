namespace MovieCore.Models.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public int Duration { get; set; }
        public int GenreId { get; set; }
        
        public Genre? Genre { get; set; }
        public MovieDetails? MovieDetails { get; set; }

        // Navigation prop
        public ICollection<Review> Reviews { get; set; } = [];
        public ICollection<Actor> Actors { get; set; } = [];
    }
}
