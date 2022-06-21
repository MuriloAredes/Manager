using FluentValidation;
using Manager.Application.Validator.CategoriaValidator;
using Manager.Context.Repositorio.Interfaces;
using MediatR;

namespace Manager.Application.Categorie.Queries.GetById
{
    public class GetByIdCategoriesRequest : IRequest<GetByIdCategoriesResponse>
    {
        public long Id { get; set; }
    }
    public class GetByIdCategoriesResponse 
    {
        public string Nome { get; set; } = String.Empty;
        public bool Ativo { get; set; }
    }
    public class GetByIdCategoriesHGandler : IRequestHandler<GetByIdCategoriesRequest, GetByIdCategoriesResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CheckOutHasValue> _validator;
        public GetByIdCategoriesHGandler(IUnitOfWork unitOfWork,
                                         IValidator<CheckOutHasValue> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;

        }

        public async Task<GetByIdCategoriesResponse> Handle(GetByIdCategoriesRequest request, CancellationToken cancellationToken)
        {

            var categorie = await _unitOfWork.Categorias.Get(e => e.Id == request.Id &&                                                            e.Ativo &&
                                                             !e.Deletado);

            #region validator
            var validator = _validator.Validate(new CheckOutHasValue {Categorie = categorie });

            if (!validator.IsValid)
                throw new Exception(string.Join(",", validator.Errors.Select(x => x.ErrorMessage)));
            #endregion


            return new GetByIdCategoriesResponse {Nome= categorie.Name, Ativo = categorie.Ativo };
        }
    }
}
