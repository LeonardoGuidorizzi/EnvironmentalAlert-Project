using Fiap.Api.EnvironmentalAlert.Data.Contexts;
using Fiap.Api.EnvironmentalAlert.Model;
using Fiap.Api.EnvironmentalAlert.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.EnvironmentalAlert.Repository
{
    public class DeviceConsumptionRepository : IDeviceConsumptionRepository
        
    {
        private readonly DatabaseContext _context;
        public DeviceConsumptionRepository(DatabaseContext context)
        {
            _context = context;
        }
        
        async Task<IEnumerable<DeviceConsumptionModel>> IDeviceConsumptionRepository.GetAllAsync() => await _context.DeviceConsumptions.ToListAsync();
        public async Task<DeviceConsumptionModel?> GetByIdAsync(int id) => await _context.DeviceConsumptions.FindAsync(id);

        async Task IDeviceConsumptionRepository.AddAsync(DeviceConsumptionModel deviceConsumption)
        {
             await _context.DeviceConsumptions.AddAsync(deviceConsumption);
             await _context.SaveChangesAsync();
        }
        async Task IDeviceConsumptionRepository.UpdateAsync(DeviceConsumptionModel deviceConsumption)
        {
            _context.DeviceConsumptions.Update(deviceConsumption);
            await _context.SaveChangesAsync();
        }

        async Task IDeviceConsumptionRepository.DeleteAsync(int id)
        {
            var deviceConsumption = await GetByIdAsync(id);
            if (deviceConsumption != null)
            {
                _context.DeviceConsumptions.Remove(deviceConsumption);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Device consumption not found"); // Throwing an exception if the device consumption is not found
            }
        }

       
    }
}
