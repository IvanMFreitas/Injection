using Injection.Entities.ViewModel;

namespace Injection.Services.Interface
{
    public interface IOrderService{
        Task<OrderViewModel> GetByIdAsync(Guid id);
    }
}
