using Fiap.Api.EnvironmentalAlert.Model;

namespace Fiap.Api.EnvironmentalAlert.Repository.Interfaces
{
    public interface IDeviceConsumptionRepository
    {
        Task<IEnumerable<DeviceConsumptionModel>> GetAllAsync();
        Task<DeviceConsumptionModel?> GetByIdAsync(int id);
        Task AddAsync(DeviceConsumptionModel deviceConsumption);
        Task UpdateAsync(DeviceConsumptionModel deviceConsumption);
        Task DeleteAsync(int id);
    }
}
