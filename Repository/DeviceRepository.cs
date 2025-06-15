using Fiap.Api.EnvironmentalAlert.Data.Contexts;
using Fiap.Api.EnvironmentalAlert.Model;
using Fiap.Api.EnvironmentalAlert.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.EnvironmentalAlert.Repository
{

    public class DeviceRepository : IDeviceRepository
    {
        private readonly DatabaseContext _context;
        public DeviceRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DeviceModel>> GetAllAsync() => await _context.Devices.ToListAsync(); // Using ToListAsync to fetch all devices asynchronously


        public async Task<DeviceModel?> GetByIdAsync(int id) => await _context.Devices.FindAsync(id); // Using FindAsync to fetch a device by ID asynchronously


        public async Task addAsync(DeviceModel device)
        {
            await _context.Devices.AddAsync(device);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DeviceModel device)
        {
            _context.Devices.Update(device);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var device = await GetByIdAsync(id);
            if (device != null)
            {
                _context.Devices.Remove(device);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Device not found"); // Throwing an exception if the device is not found
            }
        }
    }
}
