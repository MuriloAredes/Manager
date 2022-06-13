using Manager.Context.Data;
using Manager.Context.Repositorio.Interfaces;
using Manager.Domain.Entity;

namespace Manager.Context.Repositorio
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
       private IRepository<Usuario> usuariosRepository { get; set; }

        private IRepository<Produto> produtosRepository { get; set; }

        private IRepository<Categoria> categoriasRepository { get; set; }


        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IRepository<Usuario> Usuarios
        {
            get
            {
                if (usuariosRepository == null)
                    return usuariosRepository = new Repository<Usuario>(_context);

                return usuariosRepository;
            }

        }

        public IRepository<Produto> Produtos
        {
            get
            {
                if (produtosRepository == null)
                    return produtosRepository = new Repository<Produto>(_context);

                return produtosRepository;
            }
        }

        public IRepository<Categoria> Categorias
        {
            get
            {
                if (categoriasRepository == null)
                    return categoriasRepository = new Repository<Categoria>(_context);

                return categoriasRepository;
            }
        }
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public async Task<bool> Commit() 
        {
           return await _context.SaveChangesAsync() > 0;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
