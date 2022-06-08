using FluentValidation;
using Manager.Application.User.Command.Create;
using Manager.Context.Data;

namespace Manager.Application.Validator.User
{
    public class ValidatorsCheckUserRequest : AbstractValidator<CreateUserRequest>
    {
        private readonly DataContext _context;
        public ValidatorsCheckUserRequest(DataContext context)
        {
            _context = context;
            
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O campo Nome é obrigatorio")
                .NotNull().WithMessage("O campo Nome é obrigatorio");

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("O campo Email é obrigatorio")
                .NotNull().WithMessage("O campo Email é obrigatorio")
                .EmailAddress().WithMessage("E-mail invalido")
                .Must(ChekHasEmail).WithMessage("E-mail jás existente !!");
        }

        public bool ChekHasEmail(string email) 
        {
            var result = _context.usuarios.FirstOrDefault(e => e.Email.Equals(email));

            return result == null? true: false;
        }
    }
}
