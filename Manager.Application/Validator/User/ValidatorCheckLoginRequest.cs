using FluentValidation;
using Manager.Application.User.Command.Login;
using Manager.Context.Data;

namespace Manager.Application.Validator.User
{
    public class ValidatorCheckLoginRequest : AbstractValidator<LoginUserRequest>
    {
        private readonly DataContext _context;
        public ValidatorCheckLoginRequest(DataContext context)
        {
            _context = context;

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Preencha o campo email")
                .NotNull().WithMessage("Preencha o campo email");
                

            RuleFor(e => e.Password)
                .NotNull().WithMessage("preencha o campo Senha")
                .NotEmpty().WithMessage("Preencha o campo Senha");

            
        }

    }
}
