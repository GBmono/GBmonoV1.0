using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Gbmono.EF
{
    public interface IRepository<T>
    {
        // CRUD
        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(int id);

        void Save();

        T Get(int id);

        T Get(Expression<Func<T, bool>> predicate);

        IQueryable<T> Table { get; }

        IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate);

        int Count(Expression<Func<T, bool>> predicate);

        bool Any(Expression<Func<T, bool>> predicate);
    }
}
