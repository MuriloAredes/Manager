using Manager.Api.Helper;
using Manager.Application.Categorie.Command.Create;
using Manager.Application.Categorie.Command.Inativar;
using Manager.Application.Categorie.Command.Update;
using Manager.Application.Categorie.Queries.GetAll;
using Manager.Application.Categorie.Queries.GetById;
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

        [HttpPut("api/[controller]/UpdateStatus")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseDefault))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseDefault))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseDefault))]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusCategoriaRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);

                var sucessMessage = result != null ? "Atualizado Com Sucesso" : "Ocorreu um erro ao atualizacão";

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        [HttpGet("api/[controller]/GetAllCategories")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetByIdCategoriaResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseDefault))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseDefault))]
        public async Task<IActionResult> GetAllCategories([FromBody] GetAllCategoriaRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("api/[controller]/GetByIdCategories")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByIdCategoriaResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseDefault))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseDefault))]
        public async Task<IActionResult> GetByIdCategories([FromBody] GetByIdCategoriesRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);

                return  Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
