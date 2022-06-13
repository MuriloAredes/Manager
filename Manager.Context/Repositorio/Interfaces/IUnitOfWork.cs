using Manager.Domain.Entity;

namespace Manager.Context.Repositorio.Interfaces
{
    public interface IUnitOfWork
    {
         IRepository<Usuario> Usuarios { get; }
         IRepository<Produto> Produtos{ get; }
         IRepository<Categoria> Categorias { get; }
         Task<bool> Commit();
    }
}
