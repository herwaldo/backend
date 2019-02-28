using Comun.Tareas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comun.Interfaces
{
    public interface ITareasNegocio
    {
        Task<List<TareasModel>> ObtenerTodo();
        Task Crear(TareasModel usuario);
        Task Eliminar(TareasModel usuario);
        Task Actualizar(TareasModel usuario);
        Task<List<TareasModel>> Consultar(CriteriosTareas input);
    }
}
