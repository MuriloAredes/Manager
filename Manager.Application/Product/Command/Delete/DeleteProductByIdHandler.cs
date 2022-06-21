using Manager.Context.Repositorio.Interfaces;
using Manager.Domain.Entity;
using MediatR;

namespace Manager.Application.Product.Command.Delete
{
    public class DeleteProductByIdRequest : IRequest 
    {
        public long Id { get; set; }
    }
    internal class DeleteProductByIdHandler : IRequestHandler<DeleteProductByIdRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteProductByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteProductByIdRequest request, CancellationToken cancellationToken)
        {
      
             _unitOfWork.Produtos.Update(new Produto
            {
                Id = request.Id,
                Deletado = true
            });

            await _unitOfWork.Commit();

            return Unit.Value;
        }
    }
}
