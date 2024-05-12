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
    public int userId { get; set; }

    public MyTasksController(ITaskService TaskService, IUserService userService, IHttpContextAccessor httpContextAccessor)
    {
        this.TaskService = TaskService;
        this.userService = userService;
        this.userId = int.Parse(httpContextAccessor.HttpContext.User?.FindFirst("Id")?.Value ?? "0", CultureInfo.InstalledUICulture);
    }
    [HttpGet]
    [Authorize(Policy = "User")]

    public List<MyTask> Get()
    {

System.Console.WriteLine(userId+" UserId");
        return TaskService.Get(userId);

    }
    [HttpPost]
    [Authorize(Policy = "User")]

    public IActionResult Post(MyTask newTask)
    {
        newTask.UserId=this.userId;
        TaskService.Post(newTask);
        return CreatedAtAction(nameof(Post), new { id = newTask.Id }, newTask);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "User")]

    public ActionResult Put(int id,[FromBody] MyTask newTask)
    {
        newTask.UserId=userId;
        TaskService.Put(id, newTask);
        return Ok();
    }
    [HttpDelete("{id}")]
    [Authorize(Policy = "User")]

    public ActionResult Delete(int id)
    {
        TaskService.Delete(id);
        return Ok();


    }


}