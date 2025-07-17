namespace MovieCore.Models.Paging
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Data { get; set; } = new List<T>();

        public PaginationMeta Meta { get; set; } = new();
    }

    public class PaginationMeta
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
    }
}
