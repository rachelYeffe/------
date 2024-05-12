using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasks.Interfaces;
using Tasks.Models;

namespace Tasks.controllers;

[ApiController]
[Route("[controller]")]

public class MyUsersController : ControllerBase
{
    public IUserService UserService;
    public int userId { get; set; }

    public MyUsersController(IUserService UserService, IHttpContextAccessor httpContextAccessor)
    {
        this.UserService = UserService;
        this.userId = int.Parse(httpContextAccessor.HttpContext.User?.FindFirst("Id")?.Value ?? "0", CultureInfo.InstalledUICulture);

    }
    [HttpGet]
    [Authorize(Policy = "Admin")]
    public ActionResult<List<User>> GetAllUsers()
    {
        // System.Console.WriteLine();
        return UserService.GetAll();
    }

    [HttpGet]
    [Route("/Admin")]
    [Authorize(Policy = "Admin")]
    public ActionResult<string> GetAdmin()
    {
        // System.Console.WriteLine("C fljkv");
        return new OkObjectResult("true");
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "Admin")]
    public ActionResult<User> Get(int id)
    {
        return UserService.Get(id);
    }
    // [HttpGet]
    // [Authorize(Policy = "User")]
    // public ActionResult<User> Get()
    // {
    //     return UserService.Get(userId);
    // }
    [HttpPost]
    [Authorize(Policy = "Admin")]
    public IActionResult Post([FromBody] User newUser)
    {
        // System.Console.WriteLine("Ncccccc");
        UserService.Post(newUser);
        return CreatedAtAction(nameof(Post), new { id = newUser.Id }, newUser);
    }

    [HttpPut]
    [Authorize(Policy = "User")]
    public ActionResult Put(User newUser)
    {
        // System.Console.WriteLine("fandskjl");
        User user=UserService.Get(userId);
        System.Console.WriteLine(userId+"fdsn");
        UserService.Put(newUser,user);
        return Ok();
    }
    [HttpDelete("{id}")]
    [Authorize(Policy = "Admin")]
    public ActionResult Delete(int id)
    {
        UserService.Delete(id);
        return Ok();


    }


}