﻿using System.ComponentModel.DataAnnotations;

namespace Persistencia.Entidades
{
    public class Usuarios
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Ciudad { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
    }
}
