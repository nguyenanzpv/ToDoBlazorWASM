using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListApi.Entities;

namespace TodoListApi.Data
{
    public class TodoListContextSeed
    {
        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        public async System.Threading.Tasks.Task SeedAsync(TodoListDbContext context, ILogger<TodoListContextSeed> logger)
        {
           
            if (!context.Users.Any())
            {
                var user = new User()               
                {
                   Id = Guid.NewGuid(),
                   firstName = "Mr",
                   lastName = "A",
                   Email ="abc@mail.com",
                   PhoneNumber = "0321456789",
                   UserName ="admin"

                };
                user.PasswordHash = _passwordHasher.HashPassword(user, "Admin@123$");
                context.Users.Add(user);
    }
            if (!context.Tasks.Any())
            {
                context.Tasks.Add(new Entities.Task()
                {
                    Id = Guid.NewGuid(),
                    Name = "Same Task 1",
                    CreatedDate = DateTime.Now,
                    Priority = TodoList.Models.Enums.Priority.High,
                    Status = TodoList.Models.Enums.Status.Open
                }) ;
            }

            await context.SaveChangesAsync();
        }
    }
}
