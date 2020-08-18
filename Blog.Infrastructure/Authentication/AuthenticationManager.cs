using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Application.Services.Security;
using Infrastructure.Security.Interfaces;
using Infrastructure.Security.Authentication;
using Infrastructure.Security.Models;

namespace Infrastructure.Authentication
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IHttpContextAccessor HttpContextAccessor;

        private readonly IPasswordHasher PasswordHasher;

        private readonly IUserStore UserStore;

        public AuthenticationManager(
            IHttpContextAccessor httpContextAccessor,
            IPasswordHasher passwordHasher,
            IUserStore userStore)
        {
            this.HttpContextAccessor = httpContextAccessor;
            this.PasswordHasher = passwordHasher;
            this.UserStore = userStore;
        }

        public async Task<LoginResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            var user = await this.UserStore.GetUserByEmailAsync(email);

            if (user == null)
            {
                return null;
            }

            var passwordHash = new PasswordHash(Convert.FromBase64String(user.PasswordHash));
            PasswordVerificationResult passwordHasherResult = this.PasswordHasher.VerifyPassword(password, passwordHash);

            if (passwordHasherResult == PasswordVerificationResult.Success)
            {
                return null;
            }

            // sign in
            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim(ClaimTypes.Role, "Administrator"),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await this.HttpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties());

            return new LoginResult
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PasswordHash = user.PasswordHash,
                Locked = user.Locked,
                Email = user.Email
            };
        }

        public async Task<LogoutResult> Logout()
        {
            await this.HttpContextAccessor.HttpContext.SignOutAsync();

            return new LogoutResult();
        }
    }
}
