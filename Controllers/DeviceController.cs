using Fiap.Api.EnvironmentalAlert.Data.Contexts;
using Fiap.Api.EnvironmentalAlert.Enums;
using Fiap.Api.EnvironmentalAlert.Repository.Interfaces;
using Fiap.Api.EnvironmentalAlert.Services.Interfaces;
using Fiap.Api.EnvironmentalAlert.ViewModel.Device;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.EnvironmentalAlert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _service;

        public DeviceController(IDeviceService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.User)}")]
        public async Task<IActionResult> GetAllDevices() => Ok(await _service.GetAllDevicesAsync());


        [HttpGet("{id}")]
        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.User)}")]
        public async Task<IActionResult> GetDeviceById(int id)
        {
            var device = await _service.GetByIdAsync(id);
            return device != null ? Ok(device) : NotFound(); // Return 404 if device not found

        }
        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> CreateDevice([FromBody] CreateDeviceViewModel model)
        {
            var created = await _service.CreateDeviceAsync(model);
            return CreatedAtAction(nameof(GetDeviceById), new { id = created.Id }, created);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> UpdateDevice(int id, [FromBody] UpdateDeviceViewModel model)
        {
            try
            {
                return Ok(await _service.UpdateDeviceAsync(id, model));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            var deleted = await _service.DeleteDeviceAsync(id);
            return deleted ? NoContent() : NotFound(); // Return 204 No Content if deleted, 404 Not Found if device not found
        }
    }
}

