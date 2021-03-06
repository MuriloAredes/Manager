using FluentValidation;
using Manager.Application.Categoria.Command.Create;
using Manager.Context.Data;

namespace Manager.Application.Validator.Categoria
{
    public class ValidatorCheckCategoriaRequest : AbstractValidator<CreateCategoriaRequest>
    {
        private readonly DataContext _context;
        public ValidatorCheckCategoriaRequest(DataContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
                .NotNull().WithMessage("Preencha o campo")
                .Must(CheckHasName).WithMessage("categoria já existente");
        }

        public bool CheckHasName(string name) 
        {
            var categorie = _context.Categorias.FirstOrDefault(e => e.Name.Equals(name));

            return categorie != null? false: true;
        }
    }
}
