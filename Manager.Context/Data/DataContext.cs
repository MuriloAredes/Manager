using Manager.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Manager.Context.Data
{
    public  class DataContext : DbContext
    {
        #region Entities 
        public  virtual  DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Produto> Produtos { get; set; }
        #endregion

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {   
            base.OnModelCreating(builder);
        }
    }
}