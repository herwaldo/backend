using Comun;
using Comun.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuariosNegocio _usuariosNegocio;

        public UsuariosController(IConfiguration configuration, IUsuariosNegocio usuariosNegocio)
        {
            _configuration = configuration;
            _usuariosNegocio = usuariosNegocio;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login(CredencialesUsuarios model)
        {
            /*
            * Aquí deberá ir la lógica de validación, en nuestro ASP.NET Identity
            * y luego de verificar que sea una autenticación correcta vamos a proceder a
            * generar el token
            */
            var response = await _usuariosNegocio.LoguearUsuario(model);
            // usuario válido
            if (response)
            {
                var user = new UsuarioModel
                {
                    Usuario = model.Usuario,
                    Contrasena = model.Contrasena
                };

                /* Creamos la información que queremos transportar en el token,
                * en nuestro los datos del usuario
                */
                var claims = new[]
                {
            new Claim("UserData", JsonConvert.SerializeObject(user))
            };

                // Generamos el Token
                var token = new JwtSecurityToken
                (
                    issuer: _configuration["ApiAuth:Issuer"],
                    audience: _configuration["ApiAuth:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(60),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["ApiAuth:SecretKey"])),
                    SecurityAlgorithms.HmacSha256)
                );

                // Retornamos el token
                return Ok(
                    new
                    {
                        response = new JwtSecurityTokenHandler().WriteToken(token)
                    }
                );
            }
            return NotFound();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Crear([FromBody] UsuariosModel usuario)
        {
            await _usuariosNegocio.Crear(usuario);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Actualizar([FromBody] UsuariosModel usuario)
        {
            await _usuariosNegocio.Actualizar(usuario);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Eliminar(UsuariosModel usuario)
        {
            await _usuariosNegocio.Eliminar(usuario);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<List<UsuariosModel>> ObtenerTodo()
        {
            return await _usuariosNegocio.ObtenerTodo();
        }
    }
}
