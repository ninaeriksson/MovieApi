namespace MovieApi.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int BirthYear { get; set; }


        // Navigation prop
        public ICollection<Movie> Movies { get; set; } = [];
    }
}
