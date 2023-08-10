using Injection.Entities;
using Injection.Entities.ViewModel;
using Injection.Services.Interface;
using Injection.Data.Repositories;
using AutoMapper;

namespace Injection.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductViewModel> GetByIdAsync(Guid id){
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductViewModel>(product);
        }
       
    }
}