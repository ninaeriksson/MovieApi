namespace MovieCore.Models.Paging
{
    public class PagingParameters
    {
        private int maxPageSize = 100;
        private int pageSize = 10;

        public int Page { get; set; } = 1;

        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > maxPageSize ? maxPageSize : value;
        }
    }
}
