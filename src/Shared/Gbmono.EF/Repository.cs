using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Gbmono.EF
{
    public class Repository<T>: IRepository<T> where T: class
    {
        private readonly DbContext _context; // database context
        private DbSet<T> _entities; // entity collection
 
        // ctor
        public Repository(DbContext context)
        {
            _context = context;
        } 

        // private set to access entity collections
        private DbSet<T> Entities
        {
            get { return _entities ?? (_entities = _context.Set<T>()); }
        }

        public IQueryable<T> Table
        {
            get { return Entities; }
        }

        public void Create(T entity)
        {
            Entities.Add(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            Entities.Remove(entity);
        }

        public void Delete(int id)
        {
            var entityToDelete = Entities.Find(id);
            if (entityToDelete != null)
            {
                Entities.Remove(entityToDelete);
            }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public T Get(int id)
        {
            return Entities.Find(id); 
        }

        public async Task<T> GetAsync(int id)
        {
            return await Entities.FindAsync(id);
        }

        //public T Get(Expression<Func<T, bool>> predicate)
        //{
        //    // Returns the only element of a sequence, 
        //    // or a default value if the sequence is empty; 
        //    // this method throws an exception if there is more than one element in the sequence.
        //    return Fetch(predicate).SingleOrDefault();             
        //}

        //public IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate)
        //{
        //    return Table.Where(predicate);
        //}

        //public int Count(Expression<Func<T, bool>> predicate)
        //{
        //    return Fetch(predicate).Count();
        //}

        //public bool Any(Expression<Func<T, bool>> predicate)
        //{
        //    return Fetch(predicate).Any();
        //}
    }
}
