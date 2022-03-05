using Blazorbasic.Models;
using Blazorbasic.Models.SeedWork;
using Microsoft.AspNetCore.WebUtilities;
using System;
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

        public async Task<bool> CreateTask(TaskCreateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/tasks", request);
            return result.IsSuccessStatusCode;

        }

        public async Task<TaskDto> GetTaskById(string id)
        {
            var task = await _httpClient.GetFromJsonAsync<TaskDto>($"/api/tasks/{id}");
            return task;
        }

        public async Task<PagedList<TaskDto>> GetTaskList(TaskListSearch taskListSearch)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = taskListSearch.PageNumber.ToString()
            };

            if (!string.IsNullOrEmpty(taskListSearch.Name))
                queryStringParam.Add("name", taskListSearch.Name);
            if (taskListSearch.AssignerId.HasValue)
                queryStringParam.Add("assigneeId", taskListSearch.AssignerId.ToString());
            if (taskListSearch.Priority.HasValue)
                queryStringParam.Add("priority", taskListSearch.Priority.ToString());

            string url = QueryHelpers.AddQueryString("/api/tasks", queryStringParam);

            var result = await _httpClient.GetFromJsonAsync<PagedList<TaskDto>>(url);
            return result;
        }

        public async Task<bool> UpdateTask(Guid taskID, TaskUpdateRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/tasks/{taskID}", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTask(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/tasks/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> AssignTask(Guid id, AssignTaskRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/tasks/{id}/assign", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<PagedList<TaskDto>> GetMyTasks(TaskListSearch taskListSearch)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = taskListSearch.PageNumber.ToString()
            };

            if (!string.IsNullOrEmpty(taskListSearch.Name))
                queryStringParam.Add("name", taskListSearch.Name);
            if (taskListSearch.AssignerId.HasValue)
                queryStringParam.Add("assigneeId", taskListSearch.AssignerId.ToString());
            if (taskListSearch.Priority.HasValue)
                queryStringParam.Add("priority", taskListSearch.Priority.ToString());

            string url = QueryHelpers.AddQueryString("/api/tasks/me", queryStringParam);

            var result = await _httpClient.GetFromJsonAsync<PagedList<TaskDto>>(url);
            return result;
        }
    }
}
