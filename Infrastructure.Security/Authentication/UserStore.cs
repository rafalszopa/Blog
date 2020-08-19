using Dapper;
using Infrastructure.Security.Interfaces;
using Infrastructure.Security.Models;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Authentication
{
    public class UserStore : IUserStore
    {
        public readonly string ConnectionString;

        public UserStore(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public Task CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(this.ConnectionString))
            {
                var user = await connection
                    .QueryFirstOrDefaultAsync<User>("SELECT first_name FirstName, last_name LastName, email Email, password_hash PasswordHash, locked Locked FROM users WHERE email = @email", new { email });

                if (user == null)
                {

                }

                return user;
            }
        }
    }
}
