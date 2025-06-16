using Fiap.Api.EnvironmentalAlert.Enums;
using Fiap.Api.EnvironmentalAlert.Services.Interfaces;
using Fiap.Api.EnvironmentalAlert.ViewModel.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Fiap.Api.EnvironmentalAlert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;

        public AuthController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var result = await _service.RegisterAsync(model);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var result = await _service.LoginAsync(model);
            if (result == null)
                return Unauthorized("Usuário ou senha inválidos");

            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> getAllUsers() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> getUserById(int id)
        {
            var user = await _service.GetByIdAsync(id);
            return user != null ? Ok(user) : NotFound();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.User)}")]
        public async Task<IActionResult> updateUser(int id, [FromBody] UpdateUserViewModel model)
        {
            try
            {
                return Ok(await _service.UpdateUser(id, model));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> deleteUser(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent(); // 204 No Content
            }
            catch (Exception)
            {
                return NotFound(); // 404 Not Found
            }
        }
    }
}
