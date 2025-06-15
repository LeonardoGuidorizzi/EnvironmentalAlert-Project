using Fiap.Api.EnvironmentalAlert.Services.Interfaces;
using Fiap.Api.EnvironmentalAlert.ViewModel.DeviceConsumption;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.EnvironmentalAlert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceConsumptionController : ControllerBase
    {
        private readonly IDeviceConsumptionService _service;
        public DeviceConsumptionController(IDeviceConsumptionService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDeviceConsumptions() =>  Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeviceConsumptionById(int id)
        {
            var deviceConsumption = await _service.GetByIdAsync(id);
            return deviceConsumption != null ? Ok(deviceConsumption) : NotFound(); // Return 404 if consumption not found
        }

        [HttpPost]
        public async Task<ActionResult> CreateDeviceConsumption([FromBody] CreateDeviceConsumptionViewModel model)
        {
            var created  = await  _service.AddAsync(model);
            return CreatedAtAction(nameof(GetDeviceConsumptionById), new { id = created.Id }, model); // Assuming model has an Id property
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDeviceConsumption(int id, [FromBody] UpdateDeviceConsumptionViewModel model)
        {
            try
            {
                return Ok(await _service.UpdateAsync(id, model));
            }
            catch (Exception)
            {
                return NotFound(); // Return 404 if consumption not found
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeviceConsumption(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
