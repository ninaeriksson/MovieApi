namespace MovieApi.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string ReviewerName { get; set; } = null!;
        public string Comment { get; set; } = null!;
        public int Rating { get; set; }


        // Foreign key
        public int MovieId { get; set; }

        // Navigation prop
        public Movie Movie { get; set; } = null!;
    }
}
