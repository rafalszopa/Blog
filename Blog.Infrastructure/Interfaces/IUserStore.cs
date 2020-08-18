using Infrastructure.Security.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Security.Interfaces
{
    public interface IUserStore
    {
        Task CreateUserAsync(User user);

        Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
