using BDGR1_TareaProgramada_03_04.Models;
using Microsoft.EntityFrameworkCore;

namespace BDGR1_TareaProgramada_03_04.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<EntidadUsuario> Usuarios { get; set; }
        public DbSet<EntidadEmpleado> Empleados { get; set; }
    }
}
