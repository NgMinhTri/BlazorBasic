using Blazorbasic.Models;
using BlazorBasic.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBasic.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetUserList();
            var userDtos = users.Select(x => new AssignerDto()
            {
                Id = x.Id,
                FullName = x.FirstName + " " + x.LastName

            });

            return Ok(userDtos);
        }
    }
}
