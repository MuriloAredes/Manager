using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Domain.Entity
{
    public class Produto
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public long CategoriaId { get; set; } 
        public int Quantidade { get; set; }
        public double ValorUnitario { get; set; }
        public bool Ativo { get; set; }
        public bool Deletado { get; set; }
       
        [ForeignKey(nameof(CategoriaId))]
        [InverseProperty(nameof(Entity.Categoria.Produtos))]
        public virtual Categoria Categorias { get; set; } 
    }
}
