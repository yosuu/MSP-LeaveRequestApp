using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveRequestApp.Models;
using PagedList;

namespace LeaveRequestApp.DAL
{
    public class RequestRepository : GenericRepository<Requests>
    {
        public RequestRepository(LeaveContext context, UnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public PagedModel<RequestViewModel> GetRequests(int page, int pageSize, bool isUser, string userName)
        {
            var q = from d in context.Requests
                    from e in context.Users.Where(x => x.EmployeeID == d.EmployeeID)
                    from s in context.RequestStatus.Where(x => x.ID == d.RequestStatusID)
                    from y in context.Years.Where(x => x.ID == d.year)
                    orderby d.BeginDate descending
                    select new RequestViewModel
                    {
                        ID = d.ID,
                        Requestor = (e.FirstName + " " + e.LastName).Trim(),
                        BeginDate = d.BeginDate,
                        LeaveDay = d.LeaveDay,
                        Reason = d.Reason,
                        RequestStatus = s.Name,
                        Year = y.Year
                    };

            if (isUser)
            {
                q = q.Where(x => x.Requestor == userName);
            }

            var data = q.ToPagedList(page, pageSize);

            return new PagedModel<RequestViewModel> { MetaData = new Models.PagedListMetaData(data.GetMetaData()), Data = data, Total = data.TotalItemCount };
        }

        public PagedModel<Requests> GetTaskList(int page, int pageSize)
        {
            var q = from d in context.Requests
                    where d.RequestStatusID == 21 // Pending
                    orderby d.BeginDate descending
                    select d;

            var data = q.ToPagedList(page, pageSize);

            return new PagedModel<Requests> { MetaData = new Models.PagedListMetaData(data.GetMetaData()), Data = data, Total = data.TotalItemCount };
        }
    }
}
