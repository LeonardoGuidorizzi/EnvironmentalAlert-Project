using AutoMapper;
using Fiap.Api.EnvironmentalAlert.Model;
using Fiap.Api.EnvironmentalAlert.Repository.Interfaces;
using Fiap.Api.EnvironmentalAlert.Services.Interfaces;
using Fiap.Api.EnvironmentalAlert.ViewModel.DeviceConsumption;

namespace Fiap.Api.EnvironmentalAlert.Services
{
    public class DeviceConsumptionService : IDeviceConsumptionService
    {
        private readonly IDeviceConsumptionRepository _repository;
        private readonly IMapper _mapper; // Assuming you are using AutoMapper for mapping between models and view models   
        private readonly IConsumptionAlertRepository _alertRepository; // Assuming you have a repository for consumption alerts
        public DeviceConsumptionService(IDeviceConsumptionRepository repository, IMapper mapper, IConsumptionAlertRepository alertRepository)
        {
            _repository = repository;
            _mapper = mapper;           
            _alertRepository = alertRepository; // Injecting the consumption alert repository
        }

        public async Task<IEnumerable<DeviceConsumptionViewModel>> GetAllAsync()
        {
            var devicesConsumptions = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<DeviceConsumptionViewModel>>(devicesConsumptions); // Mapping DeviceConsumptionModel to DeviceConsumptionViewModel 
        }
        public async Task<DeviceConsumptionViewModel?> GetByIdAsync(int id)
        {
            var devicesConsumption = await _repository.GetByIdAsync(id);
            return devicesConsumption == null ? null : _mapper.Map<DeviceConsumptionViewModel>(devicesConsumption);
        }
        public async Task <DeviceConsumptionViewModel> AddAsync(CreateDeviceConsumptionViewModel model)
        {
            var devicesConsumptionEntity = _mapper.Map<DeviceConsumptionModel>(model); //transforming data in a db model
            if(devicesConsumptionEntity.ConsumptionKwh > devicesConsumptionEntity.ExpectedLimitKwh)
            {
                var alert = new ConsumptionAlertModel
                {
                    DeviceId = devicesConsumptionEntity.DeviceId,
                    RecordedConsumption = devicesConsumptionEntity.ConsumptionKwh,
                    ExpectedLimit = devicesConsumptionEntity.ExpectedLimitKwh,
                    Message = "Consumption exceeded the expected limit"
                };
                await _alertRepository.AddAsync(alert); // saving the alert in the db
            }
            await _repository.AddAsync(devicesConsumptionEntity); // saving in the db
            return _mapper.Map<DeviceConsumptionViewModel>(devicesConsumptionEntity); // returning an obj
        }
        public async Task<DeviceConsumptionViewModel> UpdateAsync(int id, UpdateDeviceConsumptionViewModel model)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) throw new Exception("Consumo não encontrado");
            existing.DeviceId = model.DeviceId;
            existing.ConsumptionKwh = model.ConsumptionKwh;
            existing.ExpectedLimitKwh = model.ExpectedLimitKwh; 
            await _repository.UpdateAsync(existing); // Update the existing consumption in the repository
            return _mapper.Map<DeviceConsumptionViewModel>(existing); // Return the updated consumption as a view model

        }

        public  async Task<bool> DeleteAsync(int id)
        {
            var deviceConsumption = await _repository.GetByIdAsync(id);
            if (deviceConsumption == null) throw new Exception("Consumo não encontrado");
            await _repository.DeleteAsync(id); // Delete the consumption
            return true; // Return true to indicate successful deletion
        }
    }
}
