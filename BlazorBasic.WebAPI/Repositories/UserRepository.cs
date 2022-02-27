using BlazorBasic.WebAPI.Data;
using BlazorBasic.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorBasic.WebAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUserList()
        {
            var tasks = await _context.Users.ToListAsync();
            return tasks;
        }
    }
}
