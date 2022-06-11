using FluentValidation;
using Manager.Context.Data;
using Manager.Context.Repositorio.Interfaces;
using Manager.Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Application.User.Command.Create
{
    public  class CreateUserRequest : IRequest
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
    public class CreateUserHandler : IRequestHandler<CreateUserRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DataContext _context;
        private readonly IValidator<CreateUserRequest> _validator;

        public CreateUserHandler(DataContext context, 
            IValidator<CreateUserRequest> validator,
            IUnitOfWork unitOfWork)
        {
            _context = context;
            _validator = validator;
            _unitOfWork = unitOfWork;   
        }

        public async Task<Unit> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
          #region Validator
            var validator = _validator.Validate(request);

            if (!validator.IsValid)
                throw new Exception (string.Join(",", validator.Errors.Select(x => x.ErrorMessage)));
            #endregion
        
            var user = await _context.usuarios.AddAsync(new Usuarios 
            {
                Nome = request.Nome,
                Sobrenome = request.Sobrenome,
                Email = request.Email,
                Senha = request.Senha,
                Ativo = true,
                Registro = DateTime.Now
            });
           
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
