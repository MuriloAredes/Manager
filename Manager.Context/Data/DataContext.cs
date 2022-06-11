using Manager.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Manager.Context.Data
{
    public class DataContext : DbContext
    {
        #region Entities 
        public virtual DbSet<Usuarios> usuarios { get; set; }
        public virtual DbSet<Categorias> categorias { get; set; }
        public virtual DbSet<Produtos> produtos { get; set; }
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