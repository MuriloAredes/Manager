using Manager.Context.Data;
using Manager.Context.Repositorio.Interfaces;
using Manager.Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Application.Categoria.Command.Update
{
    public class UpdateCategoriaRequest : IRequest
    {
        public long Id { get; set; }
        public string Name { get; set; } = String.Empty;
    }
    public class UpdateCategoriaHandler : IRequestHandler<UpdateCategoriaRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoriaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateCategoriaRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Name))
                return Unit.Value;

            _unitOfWork.Categorias.Update(new Domain.Entity.Categoria { Id = request.Id, Name = request.Name });

            await _unitOfWork.Commit();

            return Unit.Value;
        }
    }
}
