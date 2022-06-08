using System.ComponentModel.DataAnnotations;

namespace Manager.Domain.Entity
{
    public class Usuario
    {
        [Key]
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; } 
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime Registro{ get; set; }
        public bool Ativo { get; set; }
        
    }
}