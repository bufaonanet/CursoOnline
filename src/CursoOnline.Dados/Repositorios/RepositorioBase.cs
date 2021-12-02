using CursoOnline.Dados.Context;
using CursoOnline.Dominio._Base;
using System.Collections.Generic;
using System.Linq;

namespace CursoOnline.Dados.Repositorios
{
    public class RepositorioBase<T> : IRepositorio<T> where T : Entidade
    {
        protected readonly ApplicationDbContext _context;

        public RepositorioBase(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Adicionar(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public List<T> Consultar()
        {
            var entidades = _context.Set<T>().ToList();
            return entidades.Any() ? entidades : new List<T>();
        }

        public T ObterPorId(int id)
        {
            var query = _context.Set<T>().Where(entidade => entidade.Id == id);
            return query.Any() ? query.First() : null;
        }
    }
}