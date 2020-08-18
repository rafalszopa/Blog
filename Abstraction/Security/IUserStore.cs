using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Abstractions.Security
{
    public interface IUserStore
    {
        Task CreateUserAsync(User user);

        Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
