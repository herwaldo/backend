using Comun;
using Comun.Interfaces;
using Comun.Tareas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TareasController : ControllerBase
    {
        private readonly ITareasNegocio _tareasNegocio;

        public TareasController(ITareasNegocio tareasNegocio)
        {
            _tareasNegocio = tareasNegocio;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Crear([FromBody] TareasModel tarea)
        {
            await _tareasNegocio.Crear(tarea);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Actualizar([FromBody] TareasModel tarea)
        {
            await _tareasNegocio.Actualizar(tarea);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Borrar([FromBody] TareasModel tarea)
        {
            await _tareasNegocio.Eliminar(tarea);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<List<TareasModel>> ObtenerTodo()
        {
            return await _tareasNegocio.ObtenerTodo();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<List<TareasModel>> Consultar([FromQuery] CriteriosTareas input)
        {
            return await _tareasNegocio.Consultar(input);
        }
    }
}
