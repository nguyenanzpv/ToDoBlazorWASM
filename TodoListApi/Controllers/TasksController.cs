using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListApi.Repositories;
using TodoListApi.Data;

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
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(Entities.Task task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var tasks = await _taskRepository.CreateTask(task);
            return CreatedAtAction(nameof(GetByID), new { Id = task.Id }, tasks);
        }

        //api//tasks/xxxx
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByID([FromRoute] Guid id)
        {
            var task = await _taskRepository.GetById(id);
            if (task == null) return NotFound($"{id} is not found");

            return Ok(task);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, Entities.Task task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var taskFromDB = await _taskRepository.GetById(id);
            if (taskFromDB == null) return NotFound($"{id} is not found");

            taskFromDB.Name = task.Name;
            var tasks = await _taskRepository.UpdateTask(task);
            return Ok(tasks);
        }
    }
}
