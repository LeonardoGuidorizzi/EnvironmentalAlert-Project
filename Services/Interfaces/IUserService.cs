using Fiap.Api.EnvironmentalAlert.ViewModel.Auth;

namespace Fiap.Api.EnvironmentalAlert.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthResponseViewModel> RegisterAsync(RegisterViewModel model);
        Task<AuthResponseViewModel> LoginAsync(LoginViewModel model);
        Task<IEnumerable<UserViewModel>> GetAllAsync();
        Task<UserViewModel?> GetByIdAsync(int id);
        Task<UserViewModel> UpdateUser(int id, UpdateUserViewModel model);
        Task DeleteAsync(int id);
    }
}
