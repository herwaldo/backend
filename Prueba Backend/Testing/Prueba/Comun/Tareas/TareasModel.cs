using System;

namespace Comun
{
    public class TareasModel
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Descripcion { get; set; }
        public string EstadoTarea { get; set; }
        public DateTime FechaVencimiento { get; set; } 

        public int UsuarioRefId { get; set; }
    }
}
