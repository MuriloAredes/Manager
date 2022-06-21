using FluentValidation;
using Manager.Domain.Entity;

namespace Manager.Application.Validator.CategoriaValidator
{
    public class CheckOutHasValue 
    {
        public Categoria? Categorie { get; set; }
    }
    public class ValidatorCheckOutCategorieHasValue : AbstractValidator<CheckOutHasValue>
    {
        public ValidatorCheckOutCategorieHasValue()
        {
            RuleFor(e => e.Categorie)
                .NotNull().WithMessage("Categoria não encontrada !");
        }
    }
}
