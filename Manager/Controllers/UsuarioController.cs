using Manager.Api.Helper;
using Manager.Api.Security;
using Manager.Application.User.Command.Create;
using Manager.Application.User.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Api.Controllers
{
    /// <summary>
    /// UserController
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("api/[controller]/CreateUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseDefault))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseDefault))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseDefault))]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request) 
        {
            try
            {
                var result = await _mediator.Send(new CreateUserRequest
                {
                    Nome = request.Nome,
                    Sobrenome = request.Sobrenome,
                    Email = request.Email,
                    Senha = Encrypt.GenerateMD5(request.Senha)
                });

                var suceesMessage = result != null ? " Cadastrado com Sucesso !" : "Ocorreu um erro ao cadastrar";
                return Ok(suceesMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("api/[controller]/Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AcessResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseDefault))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseDefault))]
        public async Task<IActionResult> Login([FromBody] GetUserRequest request)
        {
            try
            {
                var result = await _mediator.Send(new GetUserRequest
                {
                    Email = request.Email,
                    Password = Encrypt.GenerateMD5(request.Password)
                    
                });

                var suceesMessage = result != null ? " Cadastrado com Sucesso !" : "Ocorreu um erro ao cadastrar";
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
