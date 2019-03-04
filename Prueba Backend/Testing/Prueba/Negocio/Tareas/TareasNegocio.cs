using AutoMapper;
using Comun;
using Comun.Interfaces;
using Comun.Tareas;
using Newtonsoft.Json;
using Persistencia.Entidades;
using Persistencia.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Negocio.TareasNegocio
{
    public class TareasNegocio: ITareasNegocio
    {
        public readonly ITareasRepositorio _tareasRepositorio;
        public readonly IUsuariosRepositorio _usuariosRepositorio;

        public TareasNegocio(ITareasRepositorio tareasRepositorio, IUsuariosRepositorio usuariosRepositorio)
        {
            _tareasRepositorio = tareasRepositorio;
            _usuariosRepositorio = usuariosRepositorio;
        }

        public async Task<List<TareasModel>> Consultar(CriteriosTareas input)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {

                    cfg.CreateMap<List<Tareas>, List<TareasModel>>();

                });

                IMapper iMapper = config.CreateMapper();
                //Se parametrizan los predicados
                var inner = PredicateBuilder.False<Tareas>(); //OR
                var outer = PredicateBuilder.True<Tareas>(); //AND
        
                if (input.ConsultarSoloMisTareas)
                {
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity("UserData");
                    var jsonUserInfo = claimsIdentity.Claims.FirstOrDefault().Value;

                    var usuarioActual = JsonConvert.DeserializeObject<UsuarioModel>(jsonUserInfo);
                    //Suponiendo que el usuario es unico
                    var usuarioInfo = (from usuario in await _usuariosRepositorio.GetAllAsync()
                                   where usuario.Usuario == usuarioActual.Usuario
                                   select usuario).FirstOrDefault();
                    /*
                    var listaTareasInfo = (from tarea in await _tareasRepositorio.GetAllAsync()
                                      where tarea.UsuarioRefId == usuarioInfo.Id
                                      select tarea).ToList(); */

                    outer = outer.And(t => t.UsuarioRefId == usuarioInfo.Id);

                    //return iMapper.Map<List<TareasModel>>(listaTareasInfo);
                }

                if (input.ConsultarTareasPendientes)
                {/*
                    var listaTareasInfo = (from tarea in await _tareasRepositorio.GetAllAsync()
                                           where tarea.EstadoTarea == "Pendiente"
                                           select tarea).ToList(); */
                    inner = inner.Or(t => t.EstadoTarea == "Pendiente");
                    //return iMapper.Map<List<TareasModel>>(listaTareasInfo);
                }

                if (input.ConsultarTareasFinalizadas)
                {/*
                    var listaTareasInfo = (from tarea in await _tareasRepositorio.GetAllAsync()
                                           where tarea.EstadoTarea == "Finalizado"
                                           select tarea).ToList();*/
                    inner = inner.Or(t => t.EstadoTarea == "Finalizado");
                    //return iMapper.Map<List<TareasModel>>(listaTareasInfo);
                }
                outer = outer.And(inner);
                if (input.OrdenarConsultaPorFechaVencimiento)
                {
                    /*
                    var listaTareasInfo = (from tarea in await _tareasRepositorio.GetAllAsync()
                                           orderby tarea.FechaVencimiento
                                           select tarea).ToList();*/
                    var listaTareasOrdenadas = (await _tareasRepositorio.GetAllAsync()).Where(outer).OrderBy(s => s.FechaVencimiento);

                    return iMapper.Map<List<TareasModel>>(listaTareasOrdenadas);

                    //return iMapper.Map<List<TareasModel>>(listaTareasInfo);
                }
                

                var listaTareas = (await _tareasRepositorio.GetAllAsync()).Where(outer).ToList();

                return iMapper.Map<List<TareasModel>>(listaTareas);
            }
            catch (Exception)
            {
                throw new Exception("Error en consulta");
            }
        }

        public async Task<List<TareasModel>> ObtenerTodo()
        {
            try
            {
                var config = new MapperConfiguration(cfg => {

                    cfg.CreateMap<List<Tareas>, List<TareasModel>>();

                });

                IMapper iMapper = config.CreateMapper();

                var consulta = await _tareasRepositorio.GetAllAsync();
                var lista = consulta.ToList();
                return iMapper.Map<List<TareasModel>>(lista);
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener todas las tareas : " + e.Message);
            }

        }

        public async Task Crear(TareasModel tarea)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {

                    cfg.CreateMap<TareasModel, Tareas>();

                });

                IMapper iMapper = config.CreateMapper();
                var tareaEntidad = iMapper.Map<Tareas>(tarea);

                var buscarTarea = await _tareasRepositorio.GetByIdAsync(tarea.Id);
                if(buscarTarea == null)
                {
                    await _tareasRepositorio.CreateAsync(tareaEntidad);
                }
                else
                {
                    throw new Exception("La tarea ya existe");
                } 
            }
            catch (Exception e)
            {
                throw new Exception("Error al crear tarea : " + e.Message);
            }

        }

        public async Task Eliminar(TareasModel tarea)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {

                    cfg.CreateMap<TareasModel, Tareas>();

                });

                IMapper iMapper = config.CreateMapper();
                var tareaEntidad = iMapper.Map<Tareas>(tarea);

                var buscarTarea = await _tareasRepositorio.GetByIdAsync(tarea.Id);
                if(buscarTarea != null)
                {
                    await _tareasRepositorio.DeleteAsync(tareaEntidad);
                }
                else
                {
                    throw new Exception("La tarea no existe");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al eliminar tarea : " + e.Message);
            }

        }

        public async Task Actualizar(TareasModel tarea)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {

                    cfg.CreateMap<TareasModel, Tareas>();

                });

                IMapper iMapper = config.CreateMapper();
                var tareaEntidad = iMapper.Map<Tareas>(tarea);

                var buscarTarea = await _tareasRepositorio.GetByIdAsync(tarea.Id);
                if(buscarTarea != null)
                {
                    await _tareasRepositorio.UpdateAsync(tareaEntidad);
                }
                else
                {
                    throw new Exception("La tarea no existe");
                }
                
            }
            catch (Exception e)
            {
                throw new Exception("Error al actualizar tarea : " + e.Message);
            }

        }
    }
}
