using FluentValidation;
using Manager.Application.Product.Command.Create;
using Manager.Context.Repositorio.Interfaces;

namespace Manager.Application.Validator.Produto
{
    public class CheckCreateProdutoRequest :  AbstractValidator<CreateProdutoRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CheckCreateProdutoRequest(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(n => n.Name)
                .NotEmpty().WithMessage("Preencha o campo nome !")
                .NotNull().WithMessage("Preencha o campo nome ")
                .Must(CheckHasName).WithMessage("Nome de produto já existente");

            RuleFor(r => r.Quantidade)
                .NotNull()
                .NotEmpty()
                .Equals(0);

            RuleFor(i => i.ValorUnitario)
                .NotEmpty()
                .NotNull()
              //  .Must(CheckMinimunValue).WithMessage("preencha o campo valor unitario")
                ;
        }

        public bool CheckHasName(string name) 
        {
            var produto = _unitOfWork.Produtos.Get(e => e.Name.Equals(name));

            return produto != null;
        }

        public bool CheckMinimunValue(double num) 
        {
           return num != 0;
        }
    }
}
