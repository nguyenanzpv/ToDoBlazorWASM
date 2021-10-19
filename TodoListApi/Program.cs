using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListApi.Data;
using TodoListApi.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace TodoListApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //lesson 11
            var host = CreateHostBuilder(args).Build();
            host.MigrateDbContext<TodoListDbContext>( (context, services) => 
            {
                var logger = services.GetService<ILogger<TodoListContextSeed>>();
                new TodoListContextSeed().SeedAsync(context, logger).Wait();
            });
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
