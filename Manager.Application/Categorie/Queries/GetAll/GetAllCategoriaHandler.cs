using Manager.Context.Repositorio.Interfaces;
using Manager.Domain.Enum;
using MediatR;

namespace Manager.Application.Categorie.Queries.GetAll
{
    public class GetAllCategoriaRequest : IRequest<List<GetByIdCategoriaResponse>>
    {
        public string Search { get; set; } = string.Empty;
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool IsAsc { get; set; }
        public CategoriaSortColunm? SortColunm { get; set; }
    }
    public class GetByIdCategoriaResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
    public class GetAllCategoriaHandler : IRequestHandler<GetAllCategoriaRequest, List<GetByIdCategoriaResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllCategoriaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<GetByIdCategoriaResponse>> Handle(GetAllCategoriaRequest request, CancellationToken cancellationToken)
        {
            
            var categories = _unitOfWork.Categorias.GetAll(e => (string.IsNullOrEmpty(request.Search) ||
                                                                       e.Name.Contains(request.Search)) &&
                                                                       !e.Deletado)
                                                         .Select(res => new GetByIdCategoriaResponse
                                                         {
                                                             Id = res.Id,
                                                             Name = res.Name,
                                                             Ativo = res.Ativo
                                                         }).Skip((request.Page - 1) * request.PageSize)
                                                           .Take(request.PageSize).ToList();


            switch (request.SortColunm)
            {
                case CategoriaSortColunm.Name:
                    categories = request.IsAsc ?
                        categories.OrderByDescending(e => e.Name).ToList() :
                        categories;
                    break;
            };
                                                            
           return categories;
        }
    }
}
