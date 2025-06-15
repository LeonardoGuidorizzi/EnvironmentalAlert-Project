using Fiap.Api.EnvironmentalAlert.Services;
using Fiap.Api.EnvironmentalAlert.Services.Interfaces;
using Fiap.Api.EnvironmentalAlert.ViewModel.ConsumptionAlert;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.EnvironmentalAlert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumptionAlertController : ControllerBase
    {
        readonly IConsumptionAlertService _service;
        public ConsumptionAlertController(IConsumptionAlertService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllConsumptionAlerts() => Ok(await _service.GetAllAsync());
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConsumptionAlertById(int id)
        {
            var consumptionAlert = await _service.GetByIdAsync(id);
            return consumptionAlert != null ? Ok(consumptionAlert) : NotFound(); // Return 404 if alert not found
        }
        [HttpPost]
        public async Task<IActionResult> CreateConsumptionAlert([FromBody] CreateConsumptionAlertViewModel model)
        {
            var created = await _service.AddAsync(model);
            return CreatedAtAction(nameof(GetConsumptionAlertById), new { id = created.Id }, created); // Assuming model has an Id property
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConsumptionAlert(int id, [FromBody] UpdateConsumptionAlertViewModel model)
        {
            try
            {
                return Ok(await _service.UpdateAsync(id, model));
            }
            catch (Exception)
            {
                return NotFound(); // Return 404 if alert not found
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteConsumptionAlert(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound(); // Return 204 No Content if deleted, 404 Not Found if alert not found
        }
    }
}
