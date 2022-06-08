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
                .EmailAddress().WithMessage("E-mail invalido! digite um email valido ");
               // .Must(ChekHasEmail).WithMessage("E-mail exitente !!");
        }

        public bool ChekHasEmail(string email) 
        {
            var result = _context.usuarios.Find(email);

            return result.Email.Equals(email) || email.Equals(result.Email);
        }
    }
}
