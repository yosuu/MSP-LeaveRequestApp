using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using LeaveRequestApp.Models;
using PagedList;
using System.Data.Entity.Migrations;

namespace LeaveRequestApp.DAL
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal LeaveContext context;
        internal UnitOfWork unitOfWork;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(LeaveContext context, UnitOfWork unitOfWork)
        {
            this.context = context;
            this.unitOfWork = unitOfWork;
            this.dbSet = context.Set<TEntity>();
        }

        protected IQueryable<TEntity> DoGet(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = DoGet(filter, orderBy, includeProperties);

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual PagedModel<TEntity> GetPaged(int page, int pageSize,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = DoGet(filter, orderBy, includeProperties);

            IPagedList<TEntity> data = null;
            if (orderBy != null)
            {
                data = orderBy(query).ToPagedList(page, pageSize);
            }
            else
            {
                data = query.ToPagedList(page, pageSize);
            }

            return new PagedModel<TEntity> { MetaData = new LeaveRequestApp.Models.PagedListMetaData(data.GetMetaData()), Data = data };
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void AddOrUpdate(TEntity entity)
        {
            dbSet.AddOrUpdate(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

    }
}
