using NPOI.SS.Formula.Functions;
using System.Linq.Expressions;

namespace Manager.Context.Repositorio.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null);
        Task<T> Get(Expression<Func<T, bool>> predicate);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
