using Injection.Entities.ViewModel;

namespace Injection.Services.Interface
{
    public interface IOrderService{
        Task<OrderViewModel> GetByIdAsync(Guid id);
        Task<(bool success, string message)> CreateOrder(OrderRequest request, string personId);
    }
}
