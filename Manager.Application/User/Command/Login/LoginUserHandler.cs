using FluentValidation;
using Manager.Application.Auth;
using Manager.Application.Validator.User;
using Manager.Context.Data;
using Manager.Context.Repositorio.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Manager.Application.User.Command.Login
{
    public class LoginUserRequest : IRequest<AcessResponse>
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public class AcessResponse
    {
        public string Token { get; set; } = string.Empty;
    }


    public class LoginUserHandler : IRequestHandler<LoginUserRequest, AcessResponse>
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<LoginUserRequest> _validator;
        private readonly IValidator<CheckHasUser> _validatorCheckHasUser;

        public LoginUserHandler(IUnitOfWork unitOfWork,
            IValidator<LoginUserRequest> validator,
            IValidator<CheckHasUser> validatorCheckHasUser)
        {
            unitOfWork = unitOfWork;
            _validator = validator;
            _validatorCheckHasUser = validatorCheckHasUser;
        }

        public async Task<AcessResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            #region Validator
            var validator = _validator.Validate(request);

            if (!validator.IsValid)
                throw new Exception(string.Join(",", validator.Errors.Select(x => x.ErrorMessage)));

            #endregion

            var usuario = await _unitOfWork.Usuarios.Get(e => e.Email.Equals(request.Email.Trim()) && e.Senha.Equals(request.Password) && e.Ativo);

            #region ValidatorsHasUser

            var validatorUser = _validatorCheckHasUser.Validate(new CheckHasUser { usuario = usuario });

            if (!validatorUser.IsValid)
                throw new Exception(string.Join(",", validatorUser.Errors.Select(x => x.ErrorMessage)));
            #endregion

            #region GenerateToken


            var response = new AcessResponse
            {
                Token = usuario.GenerateToken()

            };
            #endregion

            #region UpdateAcess

            usuario.UltimoAcesso = DateTime.Now;
            _unitOfWork.Usuarios.Update(usuario);

            await _unitOfWork.Commit();
            #endregion

            return response;
        }
    }
}
