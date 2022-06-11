using Manager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Context.Repositorio.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Usuarios> usuarioRepository { get; set; }
        IRepository<Produtos> produtosRepository { get; set; }
        IRepository<Categorias> categoriasRepository { get; set;}
    }
}
