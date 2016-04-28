using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveRequestApp.Models;

namespace LeaveRequestApp.DAL
{
    public class UnitOfWork
    {
        private LeaveContext context = new LeaveContext();
        private DepartmentRepository departmentRepository;
        private YearRepository yearRepository;
        private RequestRepository requestRepository;
        private RequestStatusRepository requestStatusRepository;

        public DepartmentRepository DepartmentRepository
        {
            get
            {
                if (this.departmentRepository == null)
                {
                    this.departmentRepository = new DepartmentRepository(context, this);
                }
                return departmentRepository;
            }
        }

        public YearRepository YearRepository
        {
            get
            {
                if (this.yearRepository == null)
                {
                    this.yearRepository = new YearRepository(context, this);
                }
                return yearRepository;
            }
        }

        public RequestRepository RequestRepository
        {
            get
            {
                if (this.requestRepository == null)
                {
                    this.requestRepository = new RequestRepository(context, this);
                }
                return requestRepository;
            }
        }

        public RequestStatusRepository RequestStatusRepository
        {
            get
            {
                if (this.requestStatusRepository == null)
                {
                    this.requestStatusRepository = new RequestStatusRepository(context, this);
                }
                return requestStatusRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
