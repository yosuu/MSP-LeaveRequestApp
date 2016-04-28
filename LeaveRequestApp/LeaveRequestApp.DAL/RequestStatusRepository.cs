using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveRequestApp.Models;
using PagedList;

namespace LeaveRequestApp.DAL
{
    public class RequestStatusRepository : GenericRepository<RequestStatus>
    {
        public RequestStatusRepository(LeaveContext context, UnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public PagedModel<RequestStatus> GetRequestStatus(int page, int pageSize)
        {
            var q = from d in context.RequestStatus
                    orderby d.ID
                    select d;

            var data = q.ToPagedList(page, pageSize);

            return new PagedModel<RequestStatus> { MetaData = new Models.PagedListMetaData(data.GetMetaData()), Data = data, Total = data.TotalItemCount };
        }
    }
}
