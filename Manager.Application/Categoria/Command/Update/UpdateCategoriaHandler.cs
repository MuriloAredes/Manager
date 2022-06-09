using Manager.Context.Data;
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
        public string Name { get; set; }
    }
    public class UpdateCategoriaHandler : IRequestHandler<UpdateCategoriaRequest>
    {
        private readonly DataContext _context;

        public UpdateCategoriaHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCategoriaRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Name))
                return Unit.Value;

            _context.categorias.Update(new Categorias { Id = request.Id, Name = request.Name });

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
