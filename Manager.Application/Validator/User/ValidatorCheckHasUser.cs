using FluentValidation;
using Manager.Application.User.Command.Create;
using Manager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Application.Validator.User
{   
    public class CheckHasUser 
    {
        public Usuarios usuario{ get; set; }
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
