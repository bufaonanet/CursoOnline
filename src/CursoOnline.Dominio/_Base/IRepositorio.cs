using System.Collections.Generic;

namespace CursoOnline.Dominio._Base
{
    public interface IRepositorio<T>
    {
        T ObterPorId(int id);
        List<T> Consultar();
        void Adicionar(T entity);
    }
}