using Fiap.Api.EnvironmentalAlert.Model;

namespace Fiap.Api.EnvironmentalAlert.Repository.Interfaces
{
    public interface IConsumptionAlertRepository
    {
        Task<IEnumerable<ConsumptionAlertModel>> GetAllAsync();
        Task<ConsumptionAlertModel?> GetByIdAsync(int id);
        Task AddAsync(ConsumptionAlertModel consumptionAlert);
        Task UpdateAsync(ConsumptionAlertModel consumptionAlert);
        Task DeleteAsync(int id);
    }
}
