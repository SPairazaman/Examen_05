using Microsoft.EntityFrameworkCore;

namespace Examen_05.Models
{
    public class AppDBContext:DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=L-Lenovo1-SP\\SqlExpress2022;"+
                                                            "Database=Examen05DB;User Id=UserSql;Pwd=123456;"+
                                                            "Trust Server Certificate=True");
        }
    }
}
