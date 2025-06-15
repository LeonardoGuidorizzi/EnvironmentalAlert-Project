using Fiap.Api.EnvironmentalAlert.Model;
using Fiap.Api.EnvironmentalAlert.ViewModel.DeviceConsumption;

namespace Fiap.Api.EnvironmentalAlert.Services.Interfaces
{
    public interface IDeviceConsumptionService
    {
        Task<IEnumerable<DeviceConsumptionViewModel>> GetAllAsync();
        Task<DeviceConsumptionViewModel?> GetByIdAsync(int id);
        Task<DeviceConsumptionViewModel> AddAsync(CreateDeviceConsumptionViewModel model);
        Task<DeviceConsumptionViewModel> UpdateAsync(int id, UpdateDeviceConsumptionViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}
