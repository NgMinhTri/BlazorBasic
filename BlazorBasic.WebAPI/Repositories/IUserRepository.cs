using BlazorBasic.WebAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorBasic.WebAPI.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUserList();
    }
}
