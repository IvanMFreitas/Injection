using Injection.Entities;
using Injection.Entities.ViewModel;
using AutoMapper;
using Injection.Services.Interface;
using Injection.Data.Repositories;

namespace Injection.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IMapper _mapper;
        public OrderService(IRepository<Order> orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderViewModel> GetByIdAsync(Guid id){
            var order = await _orderRepository.GetByIdAsync(id);
            return _mapper.Map<OrderViewModel>(order);
        }

    }
}