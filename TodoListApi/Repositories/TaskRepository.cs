using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListApi.Data;

namespace TodoListApi.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TodoListDbContext _context;
        public TaskRepository(TodoListDbContext context)
        {
            _context = context;
        }
        public async Task<Entities.Task> CreateTask(Entities.Task task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Entities.Task> DeleteTask(Entities.Task task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Entities.Task> GetById(Guid id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<IEnumerable<Entities.Task>> GetTaskList()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<Entities.Task> UpdateTask(Entities.Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }
    }
}
