namespace Fiap.Api.EnvironmentalAlert.ViewModel.Auth
{
    public class UserViewModel
    {
        public int Id { get; set; }          // ID do usuário
        public string Username { get; set; } = null!; // Nome de usuário
        public string Email { get; set; } = null!;    // Email do usuário
        public string Role { get; set; } = null!;     // Papel (Admin, User, etc.)
    }
}
