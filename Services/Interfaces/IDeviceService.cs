using Fiap.Api.EnvironmentalAlert.ViewModel.Device;

namespace Fiap.Api.EnvironmentalAlert.Services.Interfaces
{
    public interface IDeviceService //Interface para injeção de dependência
    {
        Task<IEnumerable<DeviceViewModel>> GetAllDevicesAsync();
        Task<DeviceViewModel?> GetByIdAsync(int id);
        Task<DeviceViewModel> CreateDeviceAsync(CreateDeviceViewModel model);
        Task<DeviceViewModel> UpdateDeviceAsync(int id, UpdateDeviceViewModel model);
        Task<bool> DeleteDeviceAsync(int id);



    }
}
