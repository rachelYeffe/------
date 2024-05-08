using Tasks.Interfaces;
using Tasks.services;
using Microsoft.Extensions.DependencyInjection;
using Token.Services;
using Tasks.controllers;
namespace Tasks.Utilities
{
    public static class Utilities
    {
        public static void AddTask(this IServiceCollection services)
        {
            services.AddSingleton<ITaskService, TaskServiceFile>();



        }
        public static void AddUser(this IServiceCollection services)
        {

            services.AddSingleton<IUserService, UserServiceFile>();


        }
    }
}
