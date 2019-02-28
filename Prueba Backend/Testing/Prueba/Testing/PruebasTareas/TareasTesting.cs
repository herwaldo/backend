using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Comun;
using Comun.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

//Se utiliza libreria Xunit para pruebas unitarias
namespace Testing.PruebasTareas
{
    public class TareasTesting
    {
        private readonly ITareasNegocio _tareasNegocio;
        private readonly IUsuariosNegocio _usuariosNegocio;

        public TareasTesting()
        {
            var container = new WindsorContainer();
            // Register the CompositionRoot type with the container
            container.Register(Component.For<ITareasNegocio>());
            container.Register(Component.For<IUsuariosNegocio>());
   
            // Resolve an object of type ICompositionRoot (ask the container for an instance)
            // This is analagous to calling new() in a non-IoC application.
            _tareasNegocio = container.Resolve<ITareasNegocio>();
            _usuariosNegocio = container.Resolve<IUsuariosNegocio>();
        }

        [Fact]
        public async Task CrearTarea_Exito()
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
            await _usuariosNegocio.Crear(usuario);
            
            var tarea = new TareasModel
            {
                Id = 1,
                Descripcion = "Tarea 1",
                EstadoTarea = "Pendiente",
                FechaCreacion = DateTime.Now,
                FechaVencimiento = DateTime.Now.AddDays(5),
                UsuarioRefId = 1
            };
            Task.WaitAll();

            var exception = Record.ExceptionAsync(() => _tareasNegocio.Crear(tarea));
            Assert.IsNotType<Exception>(exception);

            //Assert.ThrowsAsync<Exception>(() => _tareasNegocio.Crear(tarea));
        }

        [Fact]
        public async Task CrearTarea_TareaYaExiste()
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
            await _usuariosNegocio.Crear(usuario);
            
            //Se instancia una primera tarea con Id = 1
            var tarea1 = new TareasModel
            {
                Id = 1,
                Descripcion = "Tarea 1",
                EstadoTarea = "Pendiente",
                FechaCreacion = DateTime.Now,
                FechaVencimiento = DateTime.Now.AddDays(5),
                UsuarioRefId = 1
            };

            //Se instancia una segunda tarea con Id = 2
            var tarea2 = new TareasModel
            {
                Id = 1,
                Descripcion = "Tarea 2",
                EstadoTarea = "Finalizado",
                FechaCreacion = DateTime.Now,
                FechaVencimiento = DateTime.Now.AddDays(5),
                UsuarioRefId = 1
            };

            await _tareasNegocio.Crear(tarea1);
            Task.WaitAll();

            var exception = Record.ExceptionAsync(() =>  _tareasNegocio.Crear(tarea2));
            Assert.IsType<Exception>(exception);

            //Assert.ThrowsAsync<Exception>(() => _tareasNegocio.Crear(tarea));
        }

        [Fact]
        public async Task EliminarTarea_Exito()
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
            await _usuariosNegocio.Crear(usuario);
            
            //Se instancia una tarea a crear
            var tareaCreada = new TareasModel
            {
                Id = 1,
                Descripcion = "Tarea 1",
                EstadoTarea = "Pendiente",
                FechaCreacion = DateTime.Now,
                FechaVencimiento = DateTime.Now.AddDays(5),
                UsuarioRefId = 1
            };

            //Se instancia una tarea a eliminar
            var tareaAEliminar = new TareasModel
            {
                Id = 1,
                Descripcion = "Tarea 1",
                EstadoTarea = "Pendiente",
                FechaCreacion = DateTime.Now,
                FechaVencimiento = DateTime.Now.AddDays(5),
                UsuarioRefId = 1
            };

            await _tareasNegocio.Crear(tareaCreada);
            Task.WaitAll();

            var exception = Record.ExceptionAsync(() => _tareasNegocio.Eliminar(tareaAEliminar));
            Assert.IsNotType<Exception>(exception);

            //Assert.ThrowsAsync<Exception>(() => _tareasNegocio.Crear(tarea));
        }

        [Fact]
        public async Task EliminarTarea_TareaNoExiste()
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
            await _usuariosNegocio.Crear(usuario);
       
            //Se instancia una tarea a crear
            var tareaCreada = new TareasModel
            {
                Id = 1,
                Descripcion = "Tarea 1",
                EstadoTarea = "Pendiente",
                FechaCreacion = DateTime.Now,
                FechaVencimiento = DateTime.Now.AddDays(5),
                UsuarioRefId = 1
            };

            //Se instancia una tarea a eliminar
            var tareaAEliminar = new TareasModel
            {
                Id = 2,
                Descripcion = "Tarea 2",
                EstadoTarea = "Pendiente",
                FechaCreacion = DateTime.Now,
                FechaVencimiento = DateTime.Now.AddDays(5),
                UsuarioRefId = 1
            };

            await _tareasNegocio.Crear(tareaCreada);
            Task.WaitAll();

            var exception = Record.ExceptionAsync(() => _tareasNegocio.Eliminar(tareaAEliminar));
            Assert.IsType<Exception>(exception);

            //Assert.ThrowsAsync<Exception>(() => _tareasNegocio.Crear(tarea));
        }

        [Fact]
        public async Task ActualizarTarea_Exito()
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
            await _usuariosNegocio.Crear(usuario);
           
            //Se instancia una tarea a crear
            var tareaCreada = new TareasModel
            {
                Id = 1,
                Descripcion = "Tarea 1",
                EstadoTarea = "Pendiente",
                FechaCreacion = DateTime.Now,
                FechaVencimiento = DateTime.Now.AddDays(5),
                UsuarioRefId = 1
            };

            //Se instancia una tarea a eliminar
            var tareaAActualizar = new TareasModel
            {
                Id = 1,
                Descripcion = "Tarea 1",
                EstadoTarea = "Finalizado",
                FechaCreacion = DateTime.Now,
                FechaVencimiento = DateTime.Now,
                UsuarioRefId = 1
            };

            await _tareasNegocio.Crear(tareaCreada);
            Task.WaitAll();

            var exception = Record.ExceptionAsync(() => _tareasNegocio.Actualizar(tareaAActualizar));
            Assert.IsNotType<Exception>(exception);

            //Assert.ThrowsAsync<Exception>(() => _tareasNegocio.Crear(tarea));
        }

        [Fact]
        public async Task ActualizarTarea_TareaNoExiste()
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
            await _usuariosNegocio.Crear(usuario);
          
            //Se instancia una tarea a crear
            var tareaCreada = new TareasModel
            {
                Id = 1,
                Descripcion = "Tarea 1",
                EstadoTarea = "Pendiente",
                FechaCreacion = DateTime.Now,
                FechaVencimiento = DateTime.Now.AddDays(5),
                UsuarioRefId = 1
            };

            //Se instancia una tarea a eliminar
            var tareaAActualizar = new TareasModel
            {
                Id = 2,
                Descripcion = "Tarea 2",
                EstadoTarea = "Pendiente",
                FechaCreacion = DateTime.Now,
                FechaVencimiento = DateTime.Now.AddDays(5),
                UsuarioRefId = 1
            };

            await _tareasNegocio.Crear(tareaCreada);
            Task.WaitAll();

            var exception = Record.ExceptionAsync(() => _tareasNegocio.Actualizar(tareaAActualizar));
            Assert.IsType<Exception>(exception);

            //Assert.ThrowsAsync<Exception>(() => _tareasNegocio.Crear(tarea));
        }

        [Fact]
        public async Task ConsultarTareas()
        {
            var tamanoEsperado = 2;
            //Se instancia un usuario
            var usuario = new UsuariosModel
            {
                Id = 1,
                Nombre = "Pepito",
                Apellidos = "Perez",
                Ciudad = "Villavicencio",
                Usuario = "pepito",
                Contrasena = "1234"
            };
            await _usuariosNegocio.Crear(usuario);

            //Se instancia una tarea a crear
            var tarea1= new TareasModel
            {
                Id = 1,
                Descripcion = "Tarea 1",
                EstadoTarea = "Pendiente",
                FechaCreacion = DateTime.Now,
                FechaVencimiento = DateTime.Now.AddDays(5),
                UsuarioRefId = 1
            };

            //Se instancia una tarea a eliminar
            var tarea2 = new TareasModel
            {
                Id = 1,
                Descripcion = "Tarea 2",
                EstadoTarea = "Finalizado",
                FechaCreacion = DateTime.Now,
                FechaVencimiento = DateTime.Now,
                UsuarioRefId = 1
            };

            await _tareasNegocio.Crear(tarea1);
            await _tareasNegocio.Crear(tarea2);
            Task.WaitAll();
            var listaTareas = await _tareasNegocio.ObtenerTodo();
            Assert.Equal(tamanoEsperado, listaTareas.Count);
        }
    }
}
