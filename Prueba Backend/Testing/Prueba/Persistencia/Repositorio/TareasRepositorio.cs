using Persistencia.Entidades;
using System.Linq;
using System.Threading.Tasks;

namespace Persistencia.Repositorio
{
    public class TareasRepositorio: ITareasRepositorio
    {
        public readonly IRepositorioBase<Tareas> _repositorioBase;

        public TareasRepositorio()
        {
            var reference = new DesignTimeDbContextFactory();
            var context = reference.CreateDbContext(null);
            _repositorioBase = new RepositorioBase<Tareas>(context);
        }

        public IQueryable<Tareas> GetAll()
        {
            return _repositorioBase.GetAll();
        }

        public async Task<Tareas> GetByIdAsync(int id)
        {
            var result = (_repositorioBase.Find(t => t.Id == id)).FirstOrDefault();
            return await Task.FromResult(result);
        }

        public async Task<IQueryable<Tareas>> GetAllAsync()
        {
            return await _repositorioBase.GetAllAsync();
        }

        public async Task CreateAsync(Tareas tarea)
        {
            await Task.Factory.StartNew(() =>
            {
                _repositorioBase.Add(tarea);
				_repositorioBase.Save();
            });
        }

        public async Task UpdateAsync(Tareas tarea)
        {
            await Task.Factory.StartNew(() =>
            {
                _repositorioBase.Edit(tarea);
				_repositorioBase.Save();
            });
        }

        public async Task DeleteAsync(Tareas tarea)
        {
            await Task.Factory.StartNew(() =>
            {
                _repositorioBase.Delete(tarea);
				_repositorioBase.Save();
            });
        }
    }
}
