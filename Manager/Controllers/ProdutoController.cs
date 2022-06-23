using Manager.Api.Helper;
using Manager.Application.Categorie.Command.Update;
using Manager.Application.Product.Command.Create;
using Manager.Application.Product.Command.Update;
using Manager.Application.Product.Queries.GetAll;
using Manager.Application.Product.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class ProdutoController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public ProdutoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("api/[controller]/CreateProduto")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseDefault))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseDefault))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseDefault))]
        public async Task<IActionResult> CreateProduto([FromBody] CreateProdutoRequest request)
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

        [HttpPut("api/[controller]/UpdateProduto")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseDefault))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseDefault))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseDefault))]
        public async Task<IActionResult> UpdateProduto([FromBody] UpdateProductRequest request)
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


        [HttpGet("api/[controller]/GetAllProdutos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetAllProductsResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseDefault))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseDefault))]
        public async Task<IActionResult> GetAllProdutos([FromBody] GetAllProductsRequest request)
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

        [HttpGet("api/[controller]/GetByIdProdutos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByIdProductResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseDefault))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseDefault))]
        public async Task<IActionResult> GetByIdProdutos([FromBody] GetByIdProductRequest request)
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
    }
}
