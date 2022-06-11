using Manager.Context.Data;
using Manager.Context.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System.Linq.Expressions;

namespace Manager.Context.Repositorio
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DataContext _context;
        DbSet<T> _dbSet;
        public Repository(DataContext context )
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            if(predicate !=null)
                return _dbSet.Where(predicate);
            
            return _dbSet.AsEnumerable();
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            
        }
    }
}
