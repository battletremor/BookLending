using BookLending.Interfaces;
using DTO.DBContexts;
using DTO.Models;

namespace BookLending.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly DTOContext _context;

        public AuthenticationRepository(DTOContext context)
        {
            _context = context;
        }
        public int Login(string Email, string Password)
        {
            //first check if username exists

            if (UserExists(Email, out int UserId))
            {
                string? hashedPassword = (from u in _context.Users where u.UserId == UserId select u.PasswordHash).FirstOrDefault();
                //then hash password compare and return
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(Password, hashedPassword);

                if (isPasswordValid)
                {
                    return UserId; // Return user ID if login is successful
                }
                else
                {
                    return -1; //invalid password
                }
            }

            return 0; // Return 0 if login fails

        }

        private bool UserExists(string Email, out int userId)
        {
            Email = Email?.ToLower();
            var user = _context.Users
                               .FirstOrDefault(u => u.Email.ToLower() == Email);

            userId = user?.UserId ?? 0;
            return user != null;
        }

        public int Register(RegisterInfo regInfo)
        {
            // Check if username already exists
            if (UserExists(regInfo.Username, out int userId))
            {
                return userId; // Return existing user ID
            }

            // Validate username and password length
            if (!IsValidUsername(regInfo.Username) || !IsValidPassword(regInfo.Password))
            {
                return -1; // Return -1 error code for invalid input
            }

            // Hash the password
            var hashedPassword = HashPassword(regInfo.Password);

            // Create a new user
            var newUser = new User
            {
                UserName = regInfo.Username,
                PasswordHash = hashedPassword,
                CreatedAt = DateTime.UtcNow,
                Email = regInfo.Email,
                FullName = regInfo.FullName,
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser.UserId; // Return the new user's ID
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool IsPasswordValid(int userId, string hashedPassword)
        {
            var user = _context.Users.Find(userId);
            return user != null && user.PasswordHash == hashedPassword;
        }

        private bool IsValidUsername(string username)
        {
            return !string.IsNullOrWhiteSpace(username) && username.Length >= 3 && username.Length <= 20;
        }

        private bool IsValidPassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && password.Length >= 6;
        }

        public class RegisterInfo
        {
            public string Username{ get; set; }
            public string Password{ get; set; }
            public string Email{ get; set; }
            public string FullName { get; set; }
        }

        public class LoginInfo
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
}
