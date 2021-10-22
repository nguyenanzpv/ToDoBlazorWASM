using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListApi.Repositories;
using TodoListApi.Data;
using TodoList.Models.Enums;
using TodoList.Models;

namespace TodoListApi.Controllers
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

        //api//tasks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskRepository.GetTaskList();

            var taskDtos = tasks.Select(x => new TaskDto()
            {
                Status = x.Status,
                Name = x.Name,
                AssigneeId = x.AssigneeId,
                CreatedDate = x.CreatedDate,
                Priority = x.Priority,
                Id = x.Id,
                AssigneeName = x.Assignee !=null ? x.Assignee.firstName + " " + x.Assignee.lastName : "N/A"
            });
            return Ok(taskDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var task = await _taskRepository.CreateTask(new Entities.Task() {
                Name = request.Name,
                Priority = request.Priority.HasValue ? request.Priority.Value : Priority.Low,
                Status = Status.Open,
                CreatedDate = DateTime.Now,
                Id = request.Id

            });
            return CreatedAtAction(nameof(GetByID), new { Id = task.Id }, task);
        }

        //api//tasks/xxxx
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByID([FromRoute] Guid id)
        {
            var task = await _taskRepository.GetById(id);
            if (task == null) return NotFound($"{id} is not found");

            return Ok(new TaskDto()
            {
                Name = task.Name,
                Status = task.Status,
                AssigneeId = task.AssigneeId,
                Priority = task.Priority,
                CreatedDate = task.CreatedDate
            });
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, TaskUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var taskFromDB = await _taskRepository.GetById(id);
            if (taskFromDB == null) return NotFound($"{id} is not found");

            taskFromDB.Name = request.Name;
            taskFromDB.Priority = request.Priority;
            var taskResult = await _taskRepository.UpdateTask(taskFromDB);
            return Ok(new TaskDto() { 
                Name = taskResult.Name,
                Status = taskResult.Status,
                AssigneeId = taskResult.AssigneeId,
                Priority = taskResult.Priority,
                CreatedDate = taskResult.CreatedDate
            });
        }
    }
}
