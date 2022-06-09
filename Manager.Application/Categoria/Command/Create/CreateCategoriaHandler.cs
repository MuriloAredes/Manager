using FluentValidation;
using Manager.Context.Data;
using Manager.Domain.Entity;
using MediatR;

namespace Manager.Application.Categoria.Command.Create
{
    public class CreateCategoriaRequest : IRequest
    {
        public string Name { get; set; }
    }
    public class CreateCategoriaHandler : IRequestHandler<CreateCategoriaRequest>
    {
        private readonly DataContext _context;
        private readonly IValidator<CreateCategoriaRequest> _validator;
        public CreateCategoriaHandler(DataContext context, IValidator<CreateCategoriaRequest> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<Unit> Handle(CreateCategoriaRequest request, CancellationToken cancellationToken)
        {
          
            #region Validator
            var validator = _validator.Validate(request);

            if (!validator.IsValid)
                throw new Exception(string.Join(",", validator.Errors.Select(x => x.ErrorMessage)));
            #endregion

            await _context.categorias.AddAsync(new Categorias 
            {
                Name = request.Name,
                Ativo = true,
                Deletado = false
            });

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
