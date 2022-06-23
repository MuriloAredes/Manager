using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manager.Domain.Entity
{
    public class Categoria
    {
      //  public Categoria()
        //{
        //    Produtos = new HashSet<Produto>();
       // }
        [Key]
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public bool Deletado { get; set; }

        [InverseProperty("Categorias")]
        public virtual ICollection<Produto> Produtos { get; set; }
    }
}

