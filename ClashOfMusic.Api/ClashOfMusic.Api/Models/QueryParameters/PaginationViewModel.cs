namespace ClashOfMusic.Api.Models.QueryParameters
{
    public class PaginationViewModel
    {
        public int? Page { get; set; }

        public int? PageSize { get; set; }

        public int PageCount { get; set; }
    }
}
