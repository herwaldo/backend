using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistencia.Entidades
{
    public class Tareas
    {
        [Key]
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Descripcion { get; set; }
        public string EstadoTarea { get; set; }
        public DateTime FechaVencimiento { get; set; }

        [ForeignKey("Usuarios")]
        public int UsuarioRefId { get; set; }
        public Usuarios Usuarios { get; set; }
        
    }
}
