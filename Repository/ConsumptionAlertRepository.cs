using Fiap.Api.EnvironmentalAlert.Data.Contexts;
using Fiap.Api.EnvironmentalAlert.Model;
using Fiap.Api.EnvironmentalAlert.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.EnvironmentalAlert.Repository
{
    public class ConsumptionAlertRepository : IConsumptionAlertRepository
    {
        private readonly DatabaseContext _context; // Uncomment if you need to use the context for database operations
        public ConsumptionAlertRepository(DatabaseContext context)
        {
            _context = context; // Initialize the context if needed for database operations
        }
        public async Task<IEnumerable<ConsumptionAlertModel>> GetAllAsync() => await _context.ConsumptionAlerts.ToListAsync(); // Using ToListAsync to fetch all consumption alerts asynchronously


        public async Task<ConsumptionAlertModel?> GetByIdAsync(int id) => await _context.ConsumptionAlerts.FindAsync(id); // Using FindAsync to fetch a consumption alert by ID asynchronously
        public async Task AddAsync(ConsumptionAlertModel consumptionAlert)
        {
            await _context.ConsumptionAlerts.AddAsync(consumptionAlert); // Using AddAsync to add a new consumption alert asynchronously
            await _context.SaveChangesAsync(); // Save changes to the database
        }
        public async Task UpdateAsync(ConsumptionAlertModel consumptionAlert)
        {
            _context.ConsumptionAlerts.Update(consumptionAlert); // Update the consumption alert in the context
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var consumptionAlert = await GetByIdAsync(id); // Fetch the consumption alert by ID
            if (consumptionAlert != null)
            {
                _context.ConsumptionAlerts.Remove(consumptionAlert); // Remove the consumption alert from the context
                await _context.SaveChangesAsync(); // Save changes to the database
            }
            else
            {
                throw new KeyNotFoundException("Consumption alert not found"); // Throwing an exception if the consumption alert is not found
            }
        }
    }
}
