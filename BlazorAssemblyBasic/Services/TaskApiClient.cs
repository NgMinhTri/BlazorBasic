﻿using Blazorbasic.Models;
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

        public async Task<TaskDto> GetTaskById(string id)
        {
            var task = await _httpClient.GetFromJsonAsync<TaskDto>($"/api/tasks/{id}");
            return task;
        }

        public async Task<List<TaskDto>> GetTaskList(TaskListSearch taskListSearch)
        {
            string url = $"/api/tasks?name={taskListSearch.Name}&assignerId={taskListSearch.AssignerId}&priority={taskListSearch.Priority}";
            var tasks = await _httpClient.GetFromJsonAsync<List<TaskDto>>(url);
            return tasks;
        }
    }
}
