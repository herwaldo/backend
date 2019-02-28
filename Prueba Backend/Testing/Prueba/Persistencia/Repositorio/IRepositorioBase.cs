using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Persistencia.Repositorio
{
    public interface IRepositorioBase<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<IQueryable<T>> GetAllAsync();
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        void Save();
    }
}
