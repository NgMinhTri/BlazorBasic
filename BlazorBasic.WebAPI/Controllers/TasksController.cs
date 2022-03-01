using Blazorbasic.Models;
using Blazorbasic.Models.Enums;
using BlazorBasic.WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBasic.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TaskListSearch taskListSearch)
        {
            var tasks = await _taskRepository.GetTaskList(taskListSearch);
            var taskDtos = tasks.Select(x => new TaskDto()
            {
                Status = x.Status,
                Name = x.Name,
                AssigneeId = x.AssignerId,
                CreatedDate = x.CreatedDate,
                Priority = x.Priority,
                Id = x.Guid,
                AssignerName = x.Assigner != null ? x.Assigner.FirstName + ' ' + x.Assigner.LastName : "N/A"
            }).OrderByDescending(taskDtos => taskDtos.CreatedDate);

            return Ok(taskDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]TaskCreateRequest request)
        {
            var task = await _taskRepository.Create(new Entities.Task()
            {
                Name = request.Name,
                Priority = request.Priority.HasValue ? request.Priority.Value : Priority.Low,
                Status = Status.Open,
                CreatedDate = DateTime.Now,
                Guid = request.Id
            });
            return CreatedAtAction(nameof(GetById), new { id = task.Guid }, task);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TaskUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskFromDb = await _taskRepository.GetById(id);

            if (taskFromDb == null)
            {
                return NotFound($"{id} is not found");
            }

            taskFromDb.Name = request.Name;
            taskFromDb.Priority = request.Priority;

            var taskResult = await _taskRepository.Update(taskFromDb);

            return Ok(new TaskDto()
            {
                Name = taskResult.Name,
                Status = taskResult.Status,
                Id = taskResult.Guid,
                AssigneeId = taskResult.AssignerId,
                Priority = taskResult.Priority,
                CreatedDate = taskResult.CreatedDate
            });
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById( Guid id)
        {
            var task = await _taskRepository.GetById(id);
            if (task == null)
                return NotFound($"{id} is not found");
            return Ok(new TaskDto()
            {
                Name = task.Name,
                Status = task.Status,
                Id = task.Guid,
                AssigneeId = task.AssignerId,
                Priority = task.Priority,
                CreatedDate = task.CreatedDate
            });
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var task = await _taskRepository.GetById(id);
            if (task == null) return NotFound($"{id} is not found");

            await _taskRepository.Delete(task);
            return Ok(new TaskDto()
            {
                Name = task.Name,
                Status = task.Status,
                Id = task.Guid,
                AssigneeId = task.AssignerId,
                Priority = task.Priority,
                CreatedDate = task.CreatedDate
            });
        }
    }
}
