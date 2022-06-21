using FluentValidation;
using Manager.Context.Repositorio.Interfaces;
using Manager.Domain.Entity;
using MediatR;

namespace Manager.Application.Product.Command.Create
{
    public class CreateProdutoRequest : IRequest 
    {
        public string Name { get; set; } = string.Empty;
        public int  Quantidade { get; set; }
        public long CategoriaId { get; set; }
        public double ValorUnitario { get; set; } 
    }
    public class CreateProdutoHandler : IRequestHandler<CreateProdutoRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateProdutoRequest> _validator;
        public CreateProdutoHandler(IUnitOfWork unitOfWork,
                                    IValidator<CreateProdutoRequest>validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public async Task<Unit> Handle(CreateProdutoRequest request, CancellationToken cancellationToken)
        {
            #region Validator 

            var validator = _validator.Validate(request);

            if (!validator.IsValid)
                throw new Exception(string.Join(",", validator.Errors.Select(x => x.ErrorMessage)));
            
            #endregion
            
            await _unitOfWork.Produtos.Add(new Produto
            {
                Name = request.Name,
                Quantidade = request.Quantidade,
                ValorUnitario = request.ValorUnitario,
                CategoriaId = request.CategoriaId,
                Ativo = true,
                Deletado = false
            });

           await _unitOfWork.Commit();

            return Unit.Value;
        }
    }
}
