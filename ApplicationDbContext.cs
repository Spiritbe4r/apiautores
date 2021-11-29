using Microsoft.EntityFrameworkCore;
using webapiautores.Entities;

namespace webapiautores
{
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) { }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
    }
}