using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveRequestApp.Models
{
    [NotMapped]
    public class PagedListMetaData
    {
        public int FirstItemOnPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; private set; }
        public bool IsFirstPage { get; private set; }
        public bool IsLastPage { get; private set; }
        public int LastItemOnPage { get; private set; }
        public int PageCount { get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int TotalItemCount { get; private set; }

        public PagedListMetaData(PagedList.IPagedList pagedList)
        {
            FirstItemOnPage = pagedList.FirstItemOnPage;
            HasNextPage = pagedList.HasNextPage;
            HasPreviousPage = pagedList.HasPreviousPage;
            IsFirstPage = pagedList.IsFirstPage;
            IsLastPage = pagedList.IsLastPage;
            LastItemOnPage = pagedList.LastItemOnPage;
            PageCount = pagedList.PageCount;
            PageNumber = pagedList.PageNumber;
            PageSize = pagedList.PageSize;
            TotalItemCount = pagedList.TotalItemCount;
        }
    }
}
