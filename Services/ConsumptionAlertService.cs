using AutoMapper;
using Fiap.Api.EnvironmentalAlert.Repository.Interfaces;
using Fiap.Api.EnvironmentalAlert.Services.Interfaces;
using Fiap.Api.EnvironmentalAlert.ViewModel.ConsumptionAlert;

namespace Fiap.Api.EnvironmentalAlert.Services
{
    public class ConsumptionAlertService : IConsumptionAlertService
    { 
        readonly IConsumptionAlertRepository _repository;
        readonly IMapper _mapper; // Assuming you are using AutoMapper for mapping between models and view models
        public ConsumptionAlertService(IConsumptionAlertRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ConsumptionAlertViewModel>> GetAllAsync()
        {
           var consumptionAlerts = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ConsumptionAlertViewModel>>(consumptionAlerts); // Mapping ConsumptionAlertModel to ConsumptionAlertViewModel
        }
        public async Task<ConsumptionAlertViewModel?> GetByIdAsync(int id)
        {
            var consumptionAlert = await _repository.GetByIdAsync(id);
            return consumptionAlert == null ? null : _mapper.Map<ConsumptionAlertViewModel>(consumptionAlert); // Mapping ConsumptionAlertModel to ConsumptionAlertViewModel if found
        }

        public async Task<ConsumptionAlertViewModel> AddAsync(CreateConsumptionAlertViewModel model)
        {
            var consumptionAlert = _mapper.Map<Model.ConsumptionAlertModel>(model); // Transforming data into a db model
            await _repository.AddAsync(consumptionAlert); // Saving in the db
            return _mapper.Map<ConsumptionAlertViewModel>(consumptionAlert); // Ensure changes are saved
        }
        public async Task<ConsumptionAlertViewModel> UpdateAsync(int id, UpdateConsumptionAlertViewModel model)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) throw new Exception("Alerta de consumo não encontrado");
            existing.DeviceId = model.DeviceId;
            existing.RecordedConsumption = model.RecordedConsumption;
            existing.ExpectedLimit = model.ExpectedLimit;
            existing.Message = model.Message;
            await _repository.UpdateAsync(existing); // Update the existing consumption alert in the repository
            return _mapper.Map<ConsumptionAlertViewModel>(existing); // Return the updated consumption alert as a view model
        }

        public async  Task<bool> DeleteAsync(int id)
        {
           var consumptionAlert = _repository.GetByIdAsync(id);
            if (consumptionAlert == null) throw new Exception("Alerta de consumo não encontrado");
            _repository.DeleteAsync(id); // Delete the consumption alert
            return true;// Return the deleted consumption alert as a view model
        }

    }
}
