using Blazorbasic.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorAssemblyBasic.Services
{
    public class UserApiClient : IUserApiClient
    {
        private readonly HttpClient _httpClient;
        public UserApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<AssignerDto>> GetAssigners()
        {
            var users = await _httpClient.GetFromJsonAsync<List<AssignerDto>>("/api/users");
            return users;
        }
    }
}
