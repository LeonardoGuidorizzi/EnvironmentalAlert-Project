using Fiap.Api.EnvironmentalAlert.Repository;
using Fiap.Api.EnvironmentalAlert.ViewModel.ConsumptionAlert;

namespace Fiap.Api.EnvironmentalAlert.Services.Interfaces
{
    public interface IConsumptionAlertService
    {
        Task<IEnumerable<ConsumptionAlertViewModel>> GetAllAsync(); // Method to get all consumption alerts
        Task<ConsumptionAlertViewModel?> GetByIdAsync(int id); // Method to get a specific consumption alert by ID
        Task<ConsumptionAlertViewModel> AddAsync(CreateConsumptionAlertViewModel model); // Method to add a new consumption alert
        Task<ConsumptionAlertViewModel> UpdateAsync(int id, UpdateConsumptionAlertViewModel model); // Method to update an existing consumption alert
        Task<bool> DeleteAsync(int id); // Method to delete a consumption alert by ID
    }
}
