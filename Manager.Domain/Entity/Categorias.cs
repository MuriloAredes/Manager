using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manager.Domain.Entity
{
    public class Categorias
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Ativo { get; set; }
        public bool Deletado { get; set; }

        [InverseProperty("Categoria")]
        public virtual ICollection<Produtos> Produtos { get; set; }
    }
}

