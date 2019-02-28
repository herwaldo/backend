using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Persistencia.Repositorio
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : class
    {
        internal DbContext _context;

        public RepositorioBase(DbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Edit(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);
            return query;
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = _context.Set<T>();
            return query;
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            IQueryable<T> query = _context.Set<T>();
            return await Task.FromResult(query);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
