using Injection.Entities.ViewModel;

namespace Injection.Services.Interface
{
    public interface IProductService{
        Task<ProductViewModel> GetByIdAsync(Guid id);
    }
}