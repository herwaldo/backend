using Persistencia.Entidades;
using System.Linq;
using System.Threading.Tasks;

namespace Persistencia.Repositorio
{
    public class UsuariosRepositorio : IUsuariosRepositorio
    {
        public readonly IRepositorioBase<Usuarios> _repositorioBase;

        public UsuariosRepositorio()
        {
            var reference = new DesignTimeDbContextFactory();
            var context = reference.CreateDbContext(null);
            _repositorioBase = new RepositorioBase<Usuarios>(context);
        }

        public IQueryable<Usuarios> GetAll()
        {
            return _repositorioBase.GetAll();
        }

        public async Task<Usuarios> GetByIdAsync(int id)
        {
            var result = (_repositorioBase.Find(t => t.Id == id)).FirstOrDefault();
            return await Task.FromResult(result);
        }

        public async Task<IQueryable<Usuarios>> GetAllAsync()
        {
            return await _repositorioBase.GetAllAsync();
        }

        public async Task CreateAsync(Usuarios usuario)
        {
            await Task.Factory.StartNew(() =>
            {
                _repositorioBase.Add(usuario);
            });   
        }

        public async Task UpdateAsync(Usuarios usuario)
        {
            await Task.Factory.StartNew(() =>
            {
                _repositorioBase.Edit(usuario);
            });
        }

        public async Task DeleteAsync(Usuarios usuario)
        {
            await Task.Factory.StartNew(() =>
            {
                _repositorioBase.Delete(usuario);
            });
        }
    }
}
