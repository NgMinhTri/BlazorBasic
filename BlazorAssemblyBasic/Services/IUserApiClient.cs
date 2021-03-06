using Blazorbasic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorAssemblyBasic.Services
{
    public interface IUserApiClient
    {
        Task<List<AssignerDto>> GetAssigners();
    }
}
