using Manager.Context.Data;
using Manager.Context.Repositorio.Interfaces;
using Manager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Context.Repositorio
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public IRepository<Usuarios> usuarioRepository { get; set; } 

        public IRepository<Produtos> produtosRepository { get; set; }

        public IRepository<Categorias> categoriasRepository { get; set; }


        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IRepository<Usuarios> UsuarioRepository
        {
            get 
            {
                if(usuarioRepository == null)
                    return usuarioRepository = new Repository<Usuarios>(_context);
            
                return usuarioRepository;
            }
            
        }

        public IRepository<Produtos> ProdutosRepository 
        {
            get 
            {
                if (produtosRepository == null)
                    return produtosRepository = new Repository<Produtos>(_context);

                return produtosRepository;
            }
        }

        public IRepository<Categorias> CategoriasRepository
        {
            get
            {
                if (categoriasRepository == null)
                    return categoriasRepository = new Repository<Categorias>(_context);

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

        public void Dispose() 
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
