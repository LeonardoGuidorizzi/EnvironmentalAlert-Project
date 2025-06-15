using Fiap.Api.EnvironmentalAlert.Model;

namespace Fiap.Api.EnvironmentalAlert.Repository.Interfaces
{
    public interface IDeviceRepository // Interface para injeção de dependência
    {
        Task<IEnumerable<DeviceModel>> GetAllAsync();
        Task<DeviceModel?> GetByIdAsync(int id);
        Task addAsync(DeviceModel device);
        Task UpdateAsync(DeviceModel device);
        Task DeleteAsync(int id);

    }
}
