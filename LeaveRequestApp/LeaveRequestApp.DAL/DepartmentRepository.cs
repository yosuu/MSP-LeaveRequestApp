using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveRequestApp.Models;
using PagedList;

namespace LeaveRequestApp.DAL
{
    public class DepartmentRepository : GenericRepository<Departments>
    {
        public DepartmentRepository(LeaveContext context, UnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public PagedModel<Departments> GetDepartments(int page, int pageSize)
        {
            var q = from d in context.Departments
                    orderby d.ID
                    select d;

            var data = q.ToPagedList(page, pageSize);

            return new PagedModel<Departments> { MetaData = new Models.PagedListMetaData(data.GetMetaData()), Data = data, Total = data.TotalItemCount };
        }
    }
}
