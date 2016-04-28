using System.ComponentModel.DataAnnotations.Schema;
using PagedList;

namespace LeaveRequestApp.Models
{
    [NotMapped]
    public class PagedModel<TModel> where TModel : class
    {
        public PagedListMetaData MetaData { get; set; }

        public IPagedList<TModel> Data { get; set; }

        public int Total { get; set; }
    }
}
