using Manager.Context.Repositorio.Interfaces;
using MediatR;

namespace Manager.Application.Categorie.Command.Inativar
{
    public class UpdateStatusCategoriaRequest : IRequest 
    {
        public long Id { get; set; }
    }
    public class UpdateStatusCategoriaHandler : IRequestHandler<UpdateStatusCategoriaRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateStatusCategoriaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateStatusCategoriaRequest request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Categorias.Get(e => e.Id == request.Id);

            if (category == null)
                throw new Exception("categoria não encontrado!");

           category.Ativo = !category.Ativo;

            _unitOfWork.Categorias.Update(category);
            await _unitOfWork.Commit();
           
            return Unit.Value;
        }
    }
}
