using System.ComponentModel.DataAnnotations;

namespace Manager.Domain.Entity
{
    public class Usuarios
    {
        [Key]
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Sobrenome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public DateTime Registro{ get; set; }
        public DateTime UltimoAcesso{ get; set; }
        public bool Ativo { get; set; }
        
    }
}