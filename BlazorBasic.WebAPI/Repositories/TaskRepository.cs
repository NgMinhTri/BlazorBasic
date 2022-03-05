using Blazorbasic.Models;
using Blazorbasic.Models.SeedWork;
using BlazorBasic.WebAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBasic.WebAPI.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Entities.Task>> GetTaskList(TaskListSearch taskListSearch)
        {
            var query = _context.Tasks.Include(x => x.Assigner).AsQueryable();
            if (!string.IsNullOrEmpty(taskListSearch.Name))
                query = query.Where(x => x.Name.Contains(taskListSearch.Name));

            if(taskListSearch.AssignerId.HasValue)
                query = query.Where(x => x.AssignerId == taskListSearch.AssignerId.Value);

            if (taskListSearch.Priority.HasValue)
                query = query.Where(x => x.Priority == taskListSearch.Priority.Value);

            var count = await query.CountAsync();

            var data = await query.OrderByDescending(x => x.CreatedDate)
                .Skip((taskListSearch.PageNumber - 1) * taskListSearch.PageSize)
                .Take(taskListSearch.PageSize)
                .ToListAsync();
            return new PagedList<Entities.Task>(data, count, taskListSearch.PageNumber, taskListSearch.PageSize);

        }

        public async Task<Entities.Task> Create(Entities.Task task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Entities.Task> Delete(Entities.Task task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Entities.Task> GetById(Guid id)
        {
            return await _context.Tasks.FindAsync(id);
        }       

        public async Task<Entities.Task> Update(Entities.Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<PagedList<Entities.Task>> GetTaskListByUserId(Guid userId, TaskListSearch taskListSearch)
        {
            var query = _context.Tasks
                     .Where(x => x.AssignerId == userId)
                  .Include(x => x.Assigner).AsQueryable();

            if (!string.IsNullOrEmpty(taskListSearch.Name))
                query = query.Where(x => x.Name.Contains(taskListSearch.Name));

            if (taskListSearch.AssignerId.HasValue)
                query = query.Where(x => x.AssignerId == taskListSearch.AssignerId.Value);

            if (taskListSearch.Priority.HasValue)
                query = query.Where(x => x.Priority == taskListSearch.Priority.Value);

            var count = await query.CountAsync();

            var data = await query.OrderByDescending(x => x.CreatedDate)
                .Skip((taskListSearch.PageNumber - 1) * taskListSearch.PageSize)
                .Take(taskListSearch.PageSize)
                .ToListAsync();
            return new PagedList<Entities.Task>(data, count, taskListSearch.PageNumber, taskListSearch.PageSize);
        }
    }
}
