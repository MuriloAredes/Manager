using FluentValidation;
using Manager.Context.Data;
using Manager.Context.Repositorio.Interfaces;
using Manager.Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Application.User.Command.Create
{
    public  class CreateUserRequest : IRequest<string>
    {
        public string Nome { get; set; } = string.Empty;
        public string Sobrenome { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } =  string.Empty;       
        public string ConfirmarSenha { get; set; } =  string.Empty;
    }
    public class CreateUserHandler : IRequestHandler<CreateUserRequest,string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateUserRequest> _validator;

        public CreateUserHandler(DataContext context, 
            IValidator<CreateUserRequest> validator,
            IUnitOfWork unitOfWork)
        {
            
            _validator = validator;
            _unitOfWork = unitOfWork;   
        }

        public async Task<string> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
          #region Validator
            var validator = _validator.Validate(request);

            if (!validator.IsValid)
                return string.Join(",", validator.Errors.Select(x => x.ErrorMessage));
            #endregion

           await _unitOfWork.Usuarios.Add(new Usuario
            {             
                Nome = request.Nome,
                Sobrenome = request.Sobrenome,
                Email = request.Email,
                Senha = GenerateMD5(request.Senha),
                Ativo = true,
                Registro = DateTime.Now,
                UltimoAcesso = null
            });

           await _unitOfWork.Commit();

            return "Cadastrado Com Sucesso!!";
        }

        public static string GenerateMD5(string Valor)
        {
            
            StringBuilder strBuilder = new StringBuilder();

            MD5 md5Hasher = MD5.Create();

            byte[] valorCriptografado = md5Hasher.ComputeHash(Encoding.Default.GetBytes(Valor));

            for (int i = 0; i < valorCriptografado.Length; i++)
            {
                strBuilder.Append(valorCriptografado[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
