using Manager.Api.Helper;
using Manager.Application.Categoria.Command.Create;
using Manager.Application.Categoria.Command.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Api.Controllers
{
    /// <summary>
    /// UserController
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    public class CategoriaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoriaController(IMediator mediator)
        {
           _mediator = mediator;
        }

        [HttpPost("api/[controller]/CreateCategorie")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseDefault))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseDefault))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseDefault))]
        public async Task<IActionResult> CreateCategorie([FromBody] CreateCategoriaRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                
                var sucessMessage = result != null ? "Cadastrado Com Sucesso" : "Ocorreu um erro ao cadastrar";
               
                return Ok(sucessMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("api/[controller]/UpdateCategorie")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseDefault))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseDefault))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseDefault))]
        public async Task<IActionResult> UpdateCategorie([FromBody] UpdateCategoriaRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);

                var sucessMessage = result != null ? "Atualizado Com Sucesso" : "Ocorreu um erro ao atualizacão";

                return Ok(sucessMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
