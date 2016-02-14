using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Gbmono.EF
{
    public interface IRepository<T>
    {
        // CRUD
        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(int id);

        int Save();
        Task<int> SaveAsync();

        T Get(int id);
        Task<T> GetAsync(int id);

        //T Get(Expression<Func<T, bool>> predicate);

        IQueryable<T> Table { get; }

        // IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate);
       
        //int Count(Expression<Func<T, bool>> predicate);

        //bool Any(Expression<Func<T, bool>> predicate);
    }
}
