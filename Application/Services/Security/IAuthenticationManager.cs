using System.Threading.Tasks;

namespace Application.Services.Security
{
    public interface IAuthenticationManager
    {
        Task<LoginResult> Login(string email, string password);

        Task<LogoutResult> Logout();
    }
}
