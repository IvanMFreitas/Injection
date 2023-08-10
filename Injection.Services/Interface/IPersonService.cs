using Injection.Entities.ViewModel;

namespace Injection.Services.Interface
{
    public interface IPersonService{
        Task<PersonViewModel> GetByIdAsync(Guid id);
    }
}
