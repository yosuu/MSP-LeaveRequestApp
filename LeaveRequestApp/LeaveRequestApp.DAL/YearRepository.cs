using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveRequestApp.Models;
using PagedList;

namespace LeaveRequestApp.DAL
{
    public class YearRepository : GenericRepository<Years>
    {
        public YearRepository(LeaveContext context, UnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public PagedModel<Years> GetYears(int page, int pageSize)
        {
            var q = from d in context.Years
                    orderby d.ID
                    select d;

            var data = q.ToPagedList(page, pageSize);

            return new PagedModel<Years> { MetaData = new Models.PagedListMetaData(data.GetMetaData()), Data = data, Total = data.TotalItemCount };
        }
    }
}
