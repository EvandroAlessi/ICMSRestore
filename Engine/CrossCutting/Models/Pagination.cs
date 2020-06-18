namespace CrossCutting.Models
{
    public class Pagination
    {
        public int Count { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
    }
}
