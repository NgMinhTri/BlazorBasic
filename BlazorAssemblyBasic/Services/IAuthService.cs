using Blazorbasic.Models;
using System.Threading.Tasks;

namespace BlazorAssemblyBasic.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
        Task Logout();
    }
}
