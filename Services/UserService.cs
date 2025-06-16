using AutoMapper;
using Fiap.Api.EnvironmentalAlert.Model;
using Fiap.Api.EnvironmentalAlert.Repository.Interfaces;
using Fiap.Api.EnvironmentalAlert.Services.Interfaces;
using Fiap.Api.EnvironmentalAlert.ViewModel.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Fiap.Api.EnvironmentalAlert.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _repository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<AuthResponseViewModel> RegisterAsync(RegisterViewModel model)
        {
            var existing = await _repository.GetByUsernameAsync(model.Username);
            if (existing != null)
                throw new Exception("Usuário já existe.");

            var user = _mapper.Map<UserModel>(model);
            user.PasswordHash = HashPassword(model.Password);

            await _repository.AddAsync(user);

            var token = GenerateJwtToken(user);
            var response = _mapper.Map<AuthResponseViewModel>(user);
            response.Token = token;
            return response;
        }

        public async Task<AuthResponseViewModel> LoginAsync(LoginViewModel model)
        {
            var user = await _repository.GetByUsernameAsync(model.Username);
            if (user == null || !VerifyPassword(model.Password, user.PasswordHash))
                throw new Exception("Usuário ou senha inválidos.");

            var token = GenerateJwtToken(user);
            var response = _mapper.Map<AuthResponseViewModel>(user);
            response.Token = token;
            return response;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAsync()
        {
             var users = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserViewModel>>(users);
        }

        public async Task<UserViewModel?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : _mapper.Map<UserViewModel>(user);
        }
       public async Task <UserViewModel> UpdateUser(int id, UpdateUserViewModel model)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) throw new Exception("User não encontrado");
            existing.Email = model.Email;
            existing.Username = model.Username;
            existing.PasswordHash = model.Password;
            await _repository.UpdateAsync(existing);
            return _mapper.Map<UserViewModel>(existing);
        }
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
        // ------------------- JWT -------------------

        private string GenerateJwtToken(UserModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        // ------------------- Senha -------------------
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            return HashPassword(inputPassword) == storedHash;
        }
    }
}
