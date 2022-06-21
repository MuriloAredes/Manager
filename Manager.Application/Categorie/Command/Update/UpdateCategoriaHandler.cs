using Manager.Context.Repositorio.Interfaces;
using MediatR;

namespace Manager.Application.Categorie.Command.Update
{
    public class UpdateCategoriaRequest : IRequest
    {
        public long Id { get; set; }
        public string Name { get; set; } = String.Empty;
    }
    public class UpdateCategoriaHandler : IRequestHandler<UpdateCategoriaRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoriaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateCategoriaRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Name))
                return Unit.Value;

            var categorie = await _unitOfWork.Categorias.Get(e => e.Id == request.Id);

            if (categorie == null)
                throw new Exception("categoria não encontrado!");

            categorie.Name = request.Name;

            _unitOfWork.Categorias.Update(categorie);

            await _unitOfWork.Commit();

            return Unit.Value;
        }
    }
}
