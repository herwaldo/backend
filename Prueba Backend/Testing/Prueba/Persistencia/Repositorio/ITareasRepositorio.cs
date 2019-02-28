using Persistencia.Entidades;
using System.Linq;
using System.Threading.Tasks;

namespace Persistencia.Repositorio
{
    public interface ITareasRepositorio
    {
        IQueryable<Tareas> GetAll();
        Task<Tareas> GetByIdAsync(int id);
        Task<IQueryable<Tareas>> GetAllAsync();
        Task CreateAsync(Tareas usuario);
        Task UpdateAsync(Tareas usuario);
        Task DeleteAsync(Tareas usuario);
    }
}
