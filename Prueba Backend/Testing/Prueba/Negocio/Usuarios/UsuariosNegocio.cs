using AutoMapper;
using Comun;
using Comun.Interfaces;
using Persistencia.Entidades;
using Persistencia.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Negocio.UsuariosNegocio
{
    public class UsuariosNegocio: IUsuariosNegocio
    {
        public readonly IUsuariosRepositorio _usuariosRepositorio;

        public UsuariosNegocio(IUsuariosRepositorio usuariosRepositorio)
        {
            _usuariosRepositorio = usuariosRepositorio;
        }

        public async Task<Boolean> LoguearUsuario(CredencialesUsuarios credenciales)
        {
            var consulta = await _usuariosRepositorio.GetAllAsync();
            var usuario = consulta.Where(usr => usr.Usuario == credenciales.Usuario && usr.Contrasena == credenciales.Contrasena).FirstOrDefault();
            if(usuario != null)
            {
                return true;
            }

            return false;
        }

        public async Task<List<UsuariosModel>> ObtenerTodo()
        {
            try
            {
                var config = new MapperConfiguration(cfg => {

                    cfg.CreateMap<List<Usuarios>, List<UsuariosModel>>();

                });

                IMapper iMapper = config.CreateMapper();

                var consulta = await _usuariosRepositorio.GetAllAsync();
                var lista = consulta.ToList();
                return iMapper.Map<List<UsuariosModel>>(lista);
            }
            catch(Exception e)
            {
                throw new Exception("Error al obtener todos los usuarios : " + e.Message);
            }
            
        }

        public async Task Crear(UsuariosModel usuario)
        {
            try
            {
                
                var config = new MapperConfiguration(cfg => {

                    cfg.CreateMap<UsuariosModel, Usuarios>();

                });

                IMapper iMapper = config.CreateMapper();
                var usuarioEntidad = iMapper.Map<Usuarios>(usuario);
                
                //Console.WriteLine($"Usuarios -> Id : {usuario.Id}");
                var buscarUsuario = await _usuariosRepositorio.GetByIdAsync(usuario.Id);
                if(buscarUsuario == null)
                {
                    await _usuariosRepositorio.CreateAsync(usuarioEntidad);
                }
                else
                {
                    throw new Exception("El usuario ya existe");
                }
                
                
            }
            catch(Exception e)
            {
                throw new Exception("Error al crear usuario : " + e.Message);
            }
            
        }

        public async Task Eliminar(UsuariosModel usuario)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {

                    cfg.CreateMap<UsuariosModel, Usuarios>();

                });

                IMapper iMapper = config.CreateMapper();
                var usuarioEntidad = iMapper.Map<Usuarios>(usuario);

                var buscarUsuario = await _usuariosRepositorio.GetByIdAsync(usuario.Id);
                if(buscarUsuario != null)
                {
                    await _usuariosRepositorio.DeleteAsync(usuarioEntidad);
                }
                else
                {
                    throw new Exception("El usuario no existe");
                } 

            }
            catch(Exception e)
            {
                throw new Exception("Error al eliminar usuario : " + e.Message);
            }
            
        }

        public async Task Actualizar(UsuariosModel usuario)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {

                    cfg.CreateMap<UsuariosModel, Usuarios>();

                });

                IMapper iMapper = config.CreateMapper();
                var usuarioEntidad = iMapper.Map<Usuarios>(usuario);

                var buscarUsuario = await _usuariosRepositorio.GetByIdAsync(usuario.Id);
                if (buscarUsuario != null)
                {
                    await _usuariosRepositorio.UpdateAsync(usuarioEntidad);
                }
                else
                {
                    throw new Exception("El usuario no existe");
                } 
            }
            catch(Exception e)
            {
                throw new Exception("Error al actualizar usuario : " + e.Message);
            }
            
        }
    }
}
