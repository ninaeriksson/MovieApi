namespace MovieCore.Models.Dtos
{
    public class ReviewDto
    {
        public string ReviewerName { get; set; } = null!;
        public string Comment { get; set; } = null!;
        public int Rating { get; set; }
    }
}
