using Blazorbasic.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorAssemblyBasic.Services
{
    public class TaskApiClient : ITaskApiClient
    {
        private readonly HttpClient _httpClient;
        public TaskApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;   
        }
        public async Task<List<TaskDto>> GetTaskList()
        {
            var tasks = await _httpClient.GetFromJsonAsync<List<TaskDto>>("/api/tasks");
            return tasks;
        }
    }
}
