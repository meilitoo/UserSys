using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace UserSysCore.Service
{
    public interface IBaseService<T>:IDisposable where T:class
    {
        IApplicationContext ApplicationContext { get; set; }
        void BeginTransaction(Action action);
        T Add(T item);
        void AddRange(params T[] items);
        IQueryable<T> Get();
        T GetSingle(Expression<Func<T, bool>> filter);
        IList<T> Get(Expression<Func<T, bool>> filter);
        IList<T> Get(Expression<Func<T, bool>> filter, Pagination pagination);
        IList<T> GetFromSql(string sql);
        T Get(params object[] primaryKey);
        int Count(Expression<Func<T, bool>> filter);
        void Update(T item, bool saveImmediately = true);
        void UpdateRange(params T[] items);
        void Remove(params object[] primaryKey);
        void Remove(T item, bool saveImmediately = true);
        void Remove(Expression<Func<T, bool>> filter);
        void RemoveRange(params T[] items);
        void SaveChanges();
    }
}
