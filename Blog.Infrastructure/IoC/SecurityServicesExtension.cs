using Application.Services.Security;
using Infrastructure.Authentication;
using Infrastructure.Security.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.Security.IoC
{
    public static class SecurityServicesExtension
    {
        public static void AddSecuritServices(this IServiceCollection services, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(connectionString);
            }

            services.AddTransient<IUserStore>(_ => new UserStore(connectionString));
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IAuthenticationManager, AuthenticationManager>();
        }
    }
}
