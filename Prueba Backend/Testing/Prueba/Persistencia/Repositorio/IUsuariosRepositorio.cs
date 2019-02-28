using Persistencia.Entidades;
using System.Linq;
using System.Threading.Tasks;

namespace Persistencia.Repositorio
{
    public interface IUsuariosRepositorio
    {
        IQueryable<Usuarios> GetAll();
        Task<Usuarios> GetByIdAsync(int id);
        Task<IQueryable<Usuarios>> GetAllAsync();
        Task CreateAsync(Usuarios usuario);
        Task UpdateAsync(Usuarios usuario);
        Task DeleteAsync(Usuarios usuario);
    }
}
