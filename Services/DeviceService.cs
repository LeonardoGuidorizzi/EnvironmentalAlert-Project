using AutoMapper;
using Fiap.Api.EnvironmentalAlert.Model;
using Fiap.Api.EnvironmentalAlert.Repository.Interfaces;
using Fiap.Api.EnvironmentalAlert.Services.Interfaces;
using Fiap.Api.EnvironmentalAlert.ViewModel.Device;

namespace Fiap.Api.EnvironmentalAlert.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _repository;
        private readonly IMapper _mapper; // Assuming you are using AutoMapper for mapping between models and view models
        public DeviceService(IDeviceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<DeviceViewModel>> GetAllDevicesAsync()
        {
            var devices = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<DeviceViewModel>>(devices); // Mapping DeviceModel to DeviceViewModel
        }

        public async Task<DeviceViewModel?> GetByIdAsync(int id)
        {
            var device = await _repository.GetByIdAsync(id);
            return device == null ? null : _mapper.Map<DeviceViewModel>(device); // Mapping DeviceModel to DeviceViewModel if found
        }
        public async Task<DeviceViewModel> CreateDeviceAsync(CreateDeviceViewModel model)
        {
            var device = _mapper.Map<DeviceModel>(model); //transforming data in a db model
            await _repository.addAsync(device); // saving in the db 
            return _mapper.Map<DeviceViewModel>(device); // returning an obj

        }
        public async Task<DeviceViewModel> UpdateDeviceAsync(int id, UpdateDeviceViewModel model)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) throw new Exception("Dispositivo não encontrado");
            existing.Name = model.Name;
            existing.Location = model.Location;
            await _repository.UpdateAsync(existing);
            return _mapper.Map<DeviceViewModel>(existing);
        }

        public async Task<bool> DeleteDeviceAsync(int id)
        {
            var device = await _repository.GetByIdAsync(id);
            if (device == null) return false; // Return false if device not found
            await _repository.DeleteAsync(id); // Delete the device
            return true;
        }

    }
}
