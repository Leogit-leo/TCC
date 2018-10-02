using System;
using System.Linq;

namespace Aplicacao.Core.Dominio.Contrato
{
    public interface IRepositorio<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Get(Func<TEntity, bool> predicate);
        IQueryable<TEntity> Get(Func<TEntity, bool> predicate, string[] includes);
        TEntity Find(params object[] key);
        void Atualizar(TEntity obj);
        void SalvarTodos();
        void Adicionar(TEntity obj);
        void Excluir(Func<TEntity, bool> predicate);
        void Dispose();
    }
}
