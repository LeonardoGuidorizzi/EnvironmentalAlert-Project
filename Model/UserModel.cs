using Fiap.Api.EnvironmentalAlert.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.Api.EnvironmentalAlert.Model
{
    public class UserModel
    {
      
        public int Id { get; set; }

        public string Username { get; set; } = null!;

  
        public string PasswordHash { get; set; } = null!;

       
        public string Email { get; set; } = null!;

        
        public UserRole Role { get; set; } = UserRole.User;
    }
}
