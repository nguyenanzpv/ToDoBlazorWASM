using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListApi.Entities;
using Task = TodoListApi.Entities.Task;

namespace TodoListApi.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Task>> GetTaskList();

        Task<Task> CreateTask(Task task);

        Task<Task> UpdateTask(Task task);

        Task<Task> DeleteTask(Task task);

        Task<Task> GetById(Guid id);
    }
}
