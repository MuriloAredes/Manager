using Manager.Context.Repositorio.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Application.Categorie.Command.Delete
{
    public class DeleteCategoriaRequest : IRequest 
    {
        public long Id { get; set; }
    }
    public class DeleteCategoriaHandler : IRequestHandler<DeleteCategoriaRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCategoriaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteCategoriaRequest request, CancellationToken cancellationToken)
        {
            _unitOfWork.Categorias.Delete(new  Domain.Entity.Categoria { Id = request.Id});
            
            return Unit.Value;
        }
    }
}
