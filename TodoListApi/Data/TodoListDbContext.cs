using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TodoListApi.Entities;

namespace TodoListApi.Data
{
    public class TodoListDbContext : IdentityDbContext<User, Role, Guid> //DbContext //Leson 10 doi thanh IdentityDbContext
    {
        public TodoListDbContext(DbContextOptions<TodoListDbContext> options) : base(options)
        {

        }

        public DbSet<Task> Tasks { get; set; }
    }
}
