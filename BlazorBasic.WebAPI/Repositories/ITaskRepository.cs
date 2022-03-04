
using Blazorbasic.Models;
using Blazorbasic.Models.SeedWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = BlazorBasic.WebAPI.Entities.Task;
namespace BlazorBasic.WebAPI.Repositories
{
    public interface ITaskRepository
    {
        Task<PagedList<Task>> GetTaskList(TaskListSearch taskListSearch);
        Task<Task> Create(Task task);

        Task<Task> Update(Task task);

        Task<Task> Delete(Task task);

        Task<Task> GetById(Guid id);
    }
}
