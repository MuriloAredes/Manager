using Manager.Context.Repositorio.Interfaces;
using MediatR;

namespace Manager.Application.Categoria.Command.Inativar
{
    public class InativarOrActiveCategoriaRequest : IRequest 
    {
        public long Id { get; set; }
    }
    public class InativarOrActiveCategoriaHandler : IRequestHandler<InativarOrActiveCategoriaRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        public InativarOrActiveCategoriaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(InativarOrActiveCategoriaRequest request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Categorias.Get(e => e.Id == request.Id);
            category.Ativo = !category.Ativo;

            _unitOfWork.Categorias.Update(category);
            
            return Unit.Value;
        }
    }
}
