using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasks.Interfaces;
using Tasks.Models;
using Tasks.services;
using Token.Services;

namespace Tasks.controllers
{
    [ApiController]
    [Route("[controller]")]

    public class LoginController : ControllerBase
    {
        public IUserService UserService;

        public LoginController(IUserService userService)
        {
            this.UserService = userService;
        }


        [HttpPost]
        public ActionResult<String> Login([FromBody] User User)
        {
            System.Console.WriteLine("hi im enter");

            User myUser = UserService.GetAll().FirstOrDefault(p => p.UserName == User.UserName && p.Password == User.Password);
            //    if(UserService.FindUser(User) == null)
            //    {
            //         return BadRequest();
            //    }
            if (myUser == null)
                return Unauthorized();
            var claims = new List<Claim>();
            if (myUser.IsAdmin == UserType.UnConnected)
            {
                return BadRequest();
            }
          
            
                claims = new List<Claim>
                {
                    new Claim("UserType" , "User"),
                    new Claim("Id" , myUser.Id.ToString()),
                };

            
            if (myUser.IsAdmin == UserType.Admin)
            {
                claims.Add(new Claim("UserType", "Admin"));
                
                
                    // new Claim("Id" , myUser.Id.ToString()),
                

            }
            var token = TokenService.GetToken(claims);
            return new OkObjectResult(TokenService.WriteToken(token));
        }
    }
}