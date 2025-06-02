using ausemartweb.Models;
using System.Security.Cryptography;
using System.Text;

namespace ausemartweb.Services
{
    public class AuthService
    {
        private static readonly List<User> _users = new List<User>();

        public async Task<bool> RegisterUserAsync(RegisterModel model)
        {
            // Check if user already exists
            if (_users.Any(u => u.Email.Equals(model.Email, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            // Create new user
            var user = new User
            {
                Id = _users.Count + 1,
                Email = model.Email,
                Password = HashPassword(model.Password),
                FirstName = model.FirstName,
                LastName = model.LastName,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _users.Add(user);
            return true;
        }

        public async Task<User?> ValidateUserAsync(LoginModel model)
        {
            var user = _users.FirstOrDefault(u => 
                u.Email.Equals(model.Email, StringComparison.OrdinalIgnoreCase) && 
                u.IsActive);

            if (user != null && VerifyPassword(model.Password, user.Password))
            {
                return user;
            }

            return null;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            var hashedInput = HashPassword(password);
            return hashedInput.Equals(hashedPassword);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return _users.ToList();
        }
    }
} 