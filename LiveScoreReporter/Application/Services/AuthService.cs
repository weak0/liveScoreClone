using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using LiveScoreReporter.Application.Services.Interfaces;
using LiveScoreReporter.EFCore.Infrastructure.Entities;
using LiveScoreReporter.EFCore.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace LiveScoreReporter.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Register(string username, string password)
        {
            User user = new()
            {
                Username = username,
                PasswordHash = HashPassword(password)
            };

            var isUserAlreadyExist = (await _userRepository.SelectAsync(x => x.Username == user.Username)) != null;

            if (isUserAlreadyExist)
                throw new Exception("User already created.");

            _userRepository.Add(user);

            var userRegisteredSuccessfully = await _userRepository.SaveAsync() > 0;

            if (!userRegisteredSuccessfully)
                throw new ApplicationException("Error while saving user to database");

            return true;
        }

        public async Task<bool> Login(string username, string password)
        {
            var correctCredentials = (await _userRepository.GetAllAsync())
                .SingleOrDefault(u => u.Username == username && u.PasswordHash == HashPassword(password)) != null;

            if (!correctCredentials)
                throw new Exception("Could not find User by given credentials. Enter correct Username and Password.");
            return correctCredentials;
        }

        public ClaimsIdentity GetClaimsIdentity(string login)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, login)
            };

            ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return claimsIdentity;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
