using Injection.Entities;
using Injection.Entities.ViewModel;
using AutoMapper;
using Injection.Services.Interface;
using Injection.Data.Repositories;
using Microsoft.Data.SqlClient;

namespace Injection.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        public OrderService(IRepository<Order> orderRepository, IRepository<Product> productRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<OrderViewModel> GetByIdAsync(Guid id){
            var order = await _orderRepository.GetByIdAsync(id);
            return _mapper.Map<OrderViewModel>(order);
        }

        public async Task<(bool success, string message)> CreateOrder(OrderRequest request, string personId){
            try
            {
                if (request.Qty <= 0){
                    return (false, "The quantity should have a positive value");
                }

                var product = await _productRepository.GetByIdAsync(Guid.Parse(request.ProductId));

                if (product == null){
                    return (false, "The product doesn't exist");
                }

                var parameters = new[]
                {
                    new SqlParameter("@PersonId", personId),
                    new SqlParameter("@ProductId", request.ProductId),
                    new SqlParameter("@Qty", request.Qty)
                };

                var orderCreated = await _orderRepository.ExecuteStoredProcedureAsync<int>("CreateOrder", parameters);

                return (true, "The order was created successfully");
            }
            catch (Exception ex)
            {
                return (false, $"There was an error: {ex.Message}");
            }
        }

    }
}