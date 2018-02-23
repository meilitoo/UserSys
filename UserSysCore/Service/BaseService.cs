using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UserSysCore.Extend;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace UserSysCore.Service
{
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        public BaseService(IApplicationContext applicationContext,UserContext userContext)
        {
            ApplicationContext = applicationContext;
            DbContext = userContext;
        }
        public IApplicationContext ApplicationContext { get; set; }

        public  UserContext DbContext
        {
            get;
            set;
        }

        public virtual T Add(T item)
        {
            DbContext.Set<T>().Add(item);
            SaveChanges();
            return item;
        }

        public virtual void AddRange(params T[] items)
        {
            
            DbContext.Set<T>().AddRange(items);
            SaveChanges();
        }

        public void BeginTransaction(Action action)
        {
            
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    action.Invoke();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public virtual int Count(Expression<Func<T, bool>> filter)
        {
            if(filter!=null)
            {
                return DbContext.Set<T>().Where(filter).Count(); 
            }
            return DbContext.Set<T>().Count();
        }

        public virtual void Dispose()
        {
            
        }

        public virtual IQueryable<T> Get()
        {
            return DbContext.Set<T>();
        }

        public virtual IList<T> Get(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
                return Get().ToList();
            return DbContext.Set<T>().Where(filter).ToList();
        }

        public virtual IList<T> Get(Expression<Func<T, bool>> filter, Pagination pagination)
        {
            pagination.RecordCount = Count(filter);
            IQueryable<T> result;
            if (filter != null)
            {
                result = DbContext.Set<T>().Where(filter);
            }
            else
            {
                result = DbContext.Set<T>();
            }
            if (!string.IsNullOrWhiteSpace(pagination.OrderBy)|| !string.IsNullOrWhiteSpace(pagination.OrderByDescending))
            {
                if (!string.IsNullOrWhiteSpace(pagination.OrderBy))
                {
                    result = result.OrderBy(pagination.OrderBy);
                }
                else
                {
                    result = result.OrderByDescending(pagination.OrderByDescending);
                }
            }
            return result.Skip(pagination.PageIndex * pagination.PageSize).Take(pagination.PageSize).ToList();
        }

        public virtual T Get(params object[] primaryKey)
        {
            return DbContext.Set<T>().Find(primaryKey);
        }

        public virtual T GetSingle(Expression<Func<T, bool>> filter)
        {
            return DbContext.Set<T>().Single(filter);
        }

        public virtual void Remove(params object[] primaryKey)
        {
            var model = Get((primaryKey));
            Remove(model);
        }

        public virtual void Remove(T item, bool saveImmediately = true)
        {
            DbContext.Set<T>().Remove(item);
            if (saveImmediately)
                SaveChanges();
        }

        public virtual void Remove(Expression<Func<T, bool>> filter)
        {
            var set = DbContext.Set<T>();
            set.RemoveRange(set.Where(filter));
            SaveChanges();
            
        }

        public virtual void RemoveRange(params T[] items)
        {
            DbContext.Set<T>().RemoveRange(items);
            SaveChanges();
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        public void Update(T item, bool saveImmediately = true)
        {
            DbContext.Set<T>().Update(item);
            if (saveImmediately)
                SaveChanges();
        }

        public void UpdateRange(params T[] items)
        {
            DbContext.Set<T>().UpdateRange(items);
            SaveChanges();
        }

        public virtual IList<T> GetFromSql(string sql)
        {
           return DbContext.Set<T>().FromSql(sql).ToList();
        }
    }
}
