using Microsoft.EntityFrameworkCore;
using Persistencia.Entidades;

namespace Persistencia.Context
{
    public class PruebaContext: DbContext
    {
        public PruebaContext(DbContextOptions<PruebaContext> options) : base(options){ }

        public DbSet<Tareas> Tareas { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
    }
}
