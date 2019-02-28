using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Comun;
using Comun.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

//Se utiliza libreria Xunit para pruebas unitarias
namespace Testing.PruebasUsuarios
{
    public class UsuariosTesting
    {
        private readonly IUsuariosNegocio _usuariosNegocio;

        public UsuariosTesting()
        {
            var container = new WindsorContainer();
            // Register the CompositionRoot type with the container
            container.Register(Component.For<IUsuariosNegocio>());

            // Resolve an object of type ICompositionRoot (ask the container for an instance)
            // This is analagous to calling new() in a non-IoC application.
            _usuariosNegocio = container.Resolve<IUsuariosNegocio>();
        }

        [Fact]
        public void CrearUsuario_Exito()
        {
            var usuario = new UsuariosModel
            {
                 Id = 1,
                 Nombre = "Pepito",
                 Apellidos = "Perez",
                 Ciudad = "Villavicencio",
                 Usuario = "pepito",
                 Contrasena = "1234"
            };
            //Para que sea exitoso, no debe lanzar excepcion
            var exception = Record.ExceptionAsync(() => _usuariosNegocio.Crear(usuario));
            Assert.IsNotType<Exception>(exception);

            //Assert.ThrowsAsync<Exception>(() => _tareasNegocio.Crear(tarea));
        }

        [Fact]
        public async Task CrearUsuario_UsuarioYaExiste()
        {
            //Se instancia una primer usuario con Id = 1
            var usuario1 = new UsuariosModel
            {
                Id = 1,
                Nombre = "Pepito",
                Apellidos = "Perez",
                Ciudad = "Villavicencio",
                Usuario = "pepito",
                Contrasena = "1234"
            };

            //Se instancia una segunda tarea con Id = 2
            var usuario2 = new UsuariosModel
            {
                Id = 1,
                Nombre = "Pepito",
                Apellidos = "Paez",
                Ciudad = "Villavicencio",
                Usuario = "pepito",
                Contrasena = "1234"
            };

            await _usuariosNegocio.Crear(usuario1);
            Task.WaitAll();

            var exception = Record.ExceptionAsync(() => _usuariosNegocio.Crear(usuario2));
            Assert.IsType<Exception>(exception);

            //Assert.ThrowsAsync<Exception>(() => _tareasNegocio.Crear(tarea));
        }

        [Fact]
        public async Task EliminarUsuario_Exito()
        {
            //Se instancia una tarea a crear
            var usuarioCreado = new UsuariosModel
            {
                Id = 1,
                Nombre = "Pepito",
                Apellidos = "Perez",
                Ciudad = "Villavicencio",
                Usuario = "pepito",
                Contrasena = "1234"
            };

            //Se instancia una tarea a eliminar
            var usuarioAEliminar = new UsuariosModel
            {
                Id = 1,
                Nombre = "Pepito",
                Apellidos = "Perez",
                Ciudad = "Villavicencio",
                Usuario = "pepito",
                Contrasena = "1234"
            };

            await _usuariosNegocio.Crear(usuarioCreado);
            Task.WaitAll();

            var exception = Record.ExceptionAsync(() => _usuariosNegocio.Eliminar(usuarioAEliminar));
            Assert.IsNotType<Exception>(exception);

            //Assert.ThrowsAsync<Exception>(() => _tareasNegocio.Crear(tarea));
        }

        [Fact]
        public async Task EliminarUsuario_UsuarioNoExiste()
        {
            //Se instancia una tarea a crear
            var usuarioCreado = new UsuariosModel
            {
                Id = 1,
                Nombre = "Pepito",
                Apellidos = "Perez",
                Ciudad = "Villavicencio",
                Usuario = "pepito",
                Contrasena = "1234"
            };

            //Se instancia una tarea a eliminar
            var usuarioAEliminar = new UsuariosModel
            {
                Id = 2,
                Nombre = "Pepito",
                Apellidos = "Paez",
                Ciudad = "Villavicencio",
                Usuario = "pepito",
                Contrasena = "1234"
            };

            await _usuariosNegocio.Crear(usuarioCreado);
            Task.WaitAll();

            var exception = Record.ExceptionAsync(() => _usuariosNegocio.Eliminar(usuarioAEliminar));
            Assert.IsType<Exception>(exception);

            //Assert.ThrowsAsync<Exception>(() => _tareasNegocio.Crear(tarea));
        }

        [Fact]
        public async Task ActualizarUsuario_Exito()
        {
            //Se instancia una tarea a crear
            var usuarioCreado = new UsuariosModel
            {
                Id = 1,
                Nombre = "Pepito",
                Apellidos = "Perez",
                Ciudad = "Villavicencio",
                Usuario = "pepito",
                Contrasena = "1234"
            };

            //Se instancia una tarea a eliminar
            var usuarioAActualizar = new UsuariosModel
            {
                Id = 1,
                Nombre = "Pepito",
                Apellidos = "Perez",
                Ciudad = "Bogota",
                Usuario = "pepito",
                Contrasena = "1234"
            };

            await _usuariosNegocio.Crear(usuarioCreado);
            Task.WaitAll();

            var exception = Record.ExceptionAsync(() => _usuariosNegocio.Actualizar(usuarioAActualizar));
            Assert.IsNotType<Exception>(exception);

            //Assert.ThrowsAsync<Exception>(() => _tareasNegocio.Crear(tarea));
        }

        [Fact]
        public async Task ActualizarUsuario_UsuarioNoExiste()
        {
            //Se instancia una tarea a crear
            var usuarioCreado = new UsuariosModel
            {
                Id = 1,
                Nombre = "Pepito",
                Apellidos = "Perez",
                Ciudad = "Villavicencio",
                Usuario = "pepito",
                Contrasena = "1234"
            };

            //Se instancia una tarea a eliminar
            var usuarioAActualizar = new UsuariosModel
            {
                Id = 2,
                Nombre = "Pepito",
                Apellidos = "Paez",
                Ciudad = "Villavicencio",
                Usuario = "pepito",
                Contrasena = "1234"
            };

            await _usuariosNegocio.Crear(usuarioCreado);
            Task.WaitAll();

            var exception = Record.ExceptionAsync(() => _usuariosNegocio.Actualizar(usuarioAActualizar));
            Assert.IsType<Exception>(exception);

            //Assert.ThrowsAsync<Exception>(() => _tareasNegocio.Crear(tarea));
        }

        [Fact]
        public async Task ConsultarUsuarios()
        {
            var tamanoEsperado = 2;
            //Se instancia una tarea a crear
            var usuario1 = new UsuariosModel
            {
                Id = 1,
                Nombre = "Pepito",
                Apellidos = "Perez",
                Ciudad = "Villavicencio",
                Usuario = "pepito",
                Contrasena = "1234"
            };

            //Se instancia una tarea a eliminar
            var usuario2 = new UsuariosModel
            {
                Id = 2,
                Nombre = "Pepito",
                Apellidos = "Paez",
                Ciudad = "Villavicencio",
                Usuario = "pepito",
                Contrasena = "1234"
            };

            await _usuariosNegocio.Crear(usuario1);
            await _usuariosNegocio.Crear(usuario2);
            Task.WaitAll();
            var listaUsuarios = await _usuariosNegocio.ObtenerTodo();
            Assert.Equal(tamanoEsperado, listaUsuarios.Count);
        }
    }
}
