using Fiap.Api.EnvironmentalAlert.Model;

namespace Fiap.Api.EnvironmentalAlert.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> GetAllAsync();
        Task<UserModel?> GetByIdAsync(int id);
        Task<UserModel?> GetByUsernameAsync(string username);
        Task AddAsync(UserModel user);
        Task UpdateAsync(UserModel user);
        Task DeleteAsync(int id);

    }
}
