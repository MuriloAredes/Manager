using Manager.Context.Repositorio.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Application.Product.Queries.GetById
{
    public class GetByIdProductRequest : IRequest<GetByIdProductResponse>
    {
        public long Id { get; set; }
    }

    public class GetByIdProductResponse 
    {
        public string Name { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public double ValorUnitario { get; set; }
        public bool Ativo { get; set; }
    }
    internal class GetByIdProductHandler : IRequestHandler<GetByIdProductRequest, GetByIdProductResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetByIdProductHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async  Task<GetByIdProductResponse> Handle(GetByIdProductRequest request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Produtos.Get(e => e.Id == request.Id);
            
            if (result == null)
                throw new Exception("categoria não encontrado!");

            var categorie = await _unitOfWork.Categorias.Get(e => e.Id == request.Id);

            var product = new GetByIdProductResponse
            {
                Name = result.Name,
                Categoria = categorie != null ? categorie.Name : "Undefined",
                Quantidade = result.Quantidade,
                ValorUnitario = result.ValorUnitario,
                Ativo = result.Ativo

            };



            return product;
        }
    }
}
