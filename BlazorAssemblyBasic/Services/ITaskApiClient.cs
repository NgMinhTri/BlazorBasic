using Blazorbasic.Models;
using Blazorbasic.Models.SeedWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorAssemblyBasic.Services
{
    public interface ITaskApiClient
    {
        Task<PagedList<TaskDto>> GetTaskList(TaskListSearch taskListSearch);
        Task<TaskDto> GetTaskById(string id);
        Task<bool> CreateTask(TaskCreateRequest request);
        Task<bool> UpdateTask(Guid taskID, TaskUpdateRequest request);
        Task<bool> DeleteTask(Guid id);
        Task<bool> AssignTask(Guid id, AssignTaskRequest request);
    }
}
