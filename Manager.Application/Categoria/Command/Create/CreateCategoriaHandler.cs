using FluentValidation;
using Manager.Context.Repositorio.Interfaces;
using MediatR;

namespace Manager.Application.Categoria.Command.Create
{
    public class CreateCategoriaRequest : IRequest
    {
        public string Name { get; set; } = string.Empty;
    }
    public class CreateCategoriaHandler : IRequestHandler<CreateCategoriaRequest>
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateCategoriaRequest> _validator;
        public CreateCategoriaHandler(IUnitOfWork unitOfWork, IValidator<CreateCategoriaRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Unit> Handle(CreateCategoriaRequest request, CancellationToken cancellationToken)
        {
          
            #region Validator
            var validator = _validator.Validate(request);

            if (!validator.IsValid)
                throw new Exception(string.Join(",", validator.Errors.Select(x => x.ErrorMessage)));
            #endregion

            await _unitOfWork.Categorias.Add(new Domain.Entity.Categoria 
            {
                Name = request.Name,
                Ativo = true,
                Deletado = false
            });

            await _unitOfWork.Commit();

            return Unit.Value;
        }
    }
}
