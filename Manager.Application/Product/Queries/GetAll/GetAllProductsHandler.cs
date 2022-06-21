using Manager.Context.Repositorio.Interfaces;
using Manager.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Application.Product.Queries.GetAll
{
    public class GetAllProductsRequest : IRequest<List<GetAllProductsResponse>>
    {
        public string Search { get; set; } = string.Empty;
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool IsAsc { get; set; }
        public ProductSortColunm SortColunm { get; set; }
    }

    public class GetAllProductsResponse 
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public double ValorUnitario { get; set; }
        public bool Ativo { get; set; }
    }
    internal class GetAllProductsHandler : IRequestHandler<GetAllProductsRequest, List<GetAllProductsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllProductsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<GetAllProductsResponse>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            var produtos = _unitOfWork.Produtos.GetAll(e => (string.IsNullOrEmpty(request.Search) ||
                                                                       e.Name.Contains(request.Search)) &&
                                                                       !e.Deletado)
                                                     .Select(res => new GetAllProductsResponse 
                                                     {
                                                         Id = res.Id,
                                                          Name = res.Name,
                                                          Categoria =  res.Categorias.Name,
                                                          Quantidade = res.Quantidade,
                                                          ValorUnitario = res.ValorUnitario,
                                                          Ativo = res.Ativo
                                                     } ).Skip((request.Page - 1) * request.PageSize)
                                                        .Take(request.PageSize)
                                                        .ToList();
         
            switch (request.SortColunm)
            {
                case ProductSortColunm.Name:
                    produtos = request.IsAsc ?
                        produtos.OrderByDescending(e => e.Name).ToList() :
                        produtos.ToList();
                    break;

                case ProductSortColunm.Quantidade:
                    produtos = request.IsAsc ?
                       produtos.OrderByDescending(e => e.Quantidade).ToList() :
                       produtos.ToList();
                    break;

                case ProductSortColunm.ValorUnitario:
                    produtos = request.IsAsc ?
                       produtos.OrderByDescending(e => e.ValorUnitario).ToList() :
                       produtos.ToList();
                    break;
            };

            return produtos;
        }
    }
}
