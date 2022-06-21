using Manager.Context.Repositorio.Interfaces;
using Manager.Domain.Entity;
using MediatR;

namespace Manager.Application.Product.Command.Update
{
    public class UpdateProductRequest : IRequest
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public long CategoriaId { get; set; }
        public int Quantidade { get; set; }
        public double ValorUnitario { get; set; }
    }
    internal class UpdateProductHandler : IRequestHandler<UpdateProductRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateProductHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {

            var product = await _unitOfWork.Produtos.Get(e => e.Id == request.Id);

            if(product == null )
                throw new Exception("produto não encontrado!");

            product.Name = request.Name;
            product.CategoriaId = request.CategoriaId;
            product.Quantidade = request.Quantidade;
            product.ValorUnitario = request.ValorUnitario;

            _unitOfWork.Produtos.Update(product);

            await _unitOfWork.Commit();

            return Unit.Value;
        }
    }
}
