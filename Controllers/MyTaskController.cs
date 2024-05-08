using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasks.Interfaces;
using Tasks.Models;

namespace Tasks.controllers;

[ApiController]
[Route("[controller]")]

public class MyTasksController : ControllerBase
{
    public ITaskService TaskService;
    public IUserService userService;
    public int userId {get; set ; }
    
    public MyTasksController(ITaskService TaskService, IUserService userService, IHttpContextAccessor httpContextAccessor)
    {
        this.TaskService = TaskService;
        this.userService = userService;
        this.userId = int.Parse(httpContextAccessor.HttpContext.User?.FindFirst("Id")?.Value ?? "0",CultureInfo.InstalledUICulture);
    }
    [HttpGet]
    public List<MyTask> Get()
    {
        
        return TaskService.Get(222);
        
    }
    [HttpPost]
    public IActionResult Post(MyTask newTask)
    {
           TaskService.Post(newTask);
        return CreatedAtAction(nameof(Post), new { id = newTask.Id }, newTask);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, MyTask newTask)
    {
        TaskService.Put(id, newTask);
        return Ok();
    }
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        TaskService.Delete(id);
        return Ok();


    }

    
}