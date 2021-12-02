using CursoOnline.Dominio.Cursos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CursoOnline.Dados.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Curso> Cursos { get; set; }

        public async Task Commit()
        {
            await SaveChangesAsync();
        }
    }
}