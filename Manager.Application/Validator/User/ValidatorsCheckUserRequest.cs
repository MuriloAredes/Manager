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
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .Must(ChekHasEmail).WithMessage("E-Mail já existente");

            RuleFor(s => s.Senha)
                .Length(6)
                .MaximumLength(10)
                .Equal(e => e.ConfirmarSenha);
        }

        public bool ChekHasEmail(string email) 
        {
            var result = _context.Usuarios.Any(e => e.Email.Equals(email));

            return !result; 
        }
    }
}
