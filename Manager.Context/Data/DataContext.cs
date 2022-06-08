using Manager.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Manager.Context.Data
{
    public class DataContext : DbContext
    {
        #region Entities 
        public DbSet<Usuario> usuarios { get; set; }
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