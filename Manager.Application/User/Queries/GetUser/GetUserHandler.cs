using FluentValidation;
using Manager.Application.Auth;
using Manager.Context.Data;
using Manager.Domain.Entity;
using MediatR;

namespace Manager.Application.User.Queries.GetUser
{
    public class GetUserRequest : IRequest<AcessResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class AcessResponse
    {
        public string Token { get; set; }
    }

  
    public class GetUserHandler : IRequestHandler<GetUserRequest, AcessResponse>
    {
        private readonly DataContext _context;
        private readonly IValidator<GetUserRequest> _validator;

        public GetUserHandler(DataContext context, IValidator<GetUserRequest> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<AcessResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            #region Validator
            var validator = _validator.Validate(request);

            if (!validator.IsValid)
                throw new Exception(string.Join(",", validator.Errors.Select(x => x.ErrorMessage)));

            #endregion


           var usuario = _context.usuarios.FirstOrDefault(e => e.Email.Equals(request.Email.Trim()) && e.Senha.Equals(request.Password) && e.Ativo);



            if (usuario == null)
                throw new Exception("Usuario ou senha incorreto");

            #region GenerateToken
           

            var response = new AcessResponse
            {
                Token = TokenService.GenerateToken(usuario)

            };
            #endregion

            #region AtualizaAcesso
            
            usuario.UltimoAcesso = DateTime.Now;
            _context.usuarios.Update(usuario);
          
            await _context.SaveChangesAsync();
            #endregion

            return response;
        }
    }
}
