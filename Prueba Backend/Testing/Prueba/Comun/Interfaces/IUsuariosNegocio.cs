using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comun.Interfaces
{
    public interface IUsuariosNegocio
    {
        Task<Boolean> LoguearUsuario(CredencialesUsuarios input);
        Task<List<UsuariosModel>> ObtenerTodo();
        Task Crear(UsuariosModel usuario);
        Task Eliminar(UsuariosModel usuario);
        Task Actualizar(UsuariosModel usuario);
    }
}
