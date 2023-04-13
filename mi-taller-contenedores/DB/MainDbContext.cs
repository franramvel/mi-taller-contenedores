using mi_taller_contenedores.DB.Model;
using Microsoft.EntityFrameworkCore;

namespace mi_taller_contenedores.DB
{
    public class MainDbContext:DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options)
        {
        }
        //Definimos todas las tablas que van a estar en la base de datos
        public virtual DbSet<Factura> TblFacturas { get; set; }
    }
}
