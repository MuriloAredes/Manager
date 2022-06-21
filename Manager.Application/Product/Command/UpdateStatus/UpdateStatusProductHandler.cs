using Manager.Context.Repositorio.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Application.Product.Command.UpdateStatus
{
    public class UpdateStatusProduct : IRequest 
    {
        public long Id { get; set; }
    }
    internal class UpdateStatusProductHandler : IRequestHandler<UpdateStatusProduct>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateStatusProductHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateStatusProduct request, CancellationToken cancellationToken)
        {
            var prod = await _unitOfWork.Produtos.Get(e => e.Id == request.Id && !e.Deletado);

            prod.Ativo = !prod.Ativo;

             _unitOfWork.Produtos.Update(prod);
            
            await _unitOfWork.Commit();
           
            return Unit.Value;
        }
    }
}
