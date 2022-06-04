namespace Application.RequestHelpers
{
    public class MetaData
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int HasNext { get; set; }
        public int HasPrevious { get; set; }
    }
}