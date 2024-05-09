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
    public MyUsersController(IUserService UserService)
    {
        this.UserService = UserService;
    }
    [HttpGet]
    [Authorize(Policy = "Admin")]
    public ActionResult<List<User>> GetAllUsers()
    {
        System.Console.WriteLine();
        return UserService.GetAll();
    }

    [HttpGet]
    [Route("/Admin")]
    [Authorize(Policy = "Admin")]
    public ActionResult<string> GetAdmin()
    {
        System.Console.WriteLine("C fljkv");
        return new OkObjectResult("true");
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "Admin")]
    public ActionResult<User> Get(int id)
    {
        return UserService.Get(id);
    }
    [HttpPost]
    [Authorize(Policy = "Admin")]
    public IActionResult Post([FromBody] User newUser)
    {
        System.Console.WriteLine("Ncccccc");
        UserService.Post(newUser);
        return CreatedAtAction(nameof(Post), new { id = newUser.Id }, newUser);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "User")]
    public ActionResult Put(int id, User newUser)
    {
        System.Console.WriteLine("fandskjl");
        UserService.Put(id, newUser);
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