using FluentValidation;
using Manager.Domain.Entity;

namespace Manager.Application.Validator.User
{
    public class CheckHasUser 
    {
        public Usuario usuario { get; set; }
    }
    public class ValidatorCheckHasUser : AbstractValidator<CheckHasUser>
    {
        public ValidatorCheckHasUser()
        {
            RuleFor(x => x.usuario)
                .NotNull().WithMessage("Usuario ou senha invalido");
        }
    }
}
