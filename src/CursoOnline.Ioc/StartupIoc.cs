using CursoOnline.Dados.Context;
using CursoOnline.Dados.Repositorios;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CursoOnline.Ioc
{
    public static class StartupIoc
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
               b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped(typeof(IRepositorio<>), typeof(RepositorioBase<>));
            services.AddScoped<ICursoRepositorio, CursoRepositorio>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ArmazenadorDeCurso>();
        }
    }
}