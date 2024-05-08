using Tasks;
namespace Tasks.Models;
public enum UserType{User,Admin,UnConnected}
public class User
{
    
    public int Id { get; set; } 
    public string UserName { get; set; }
    public UserType IsAdmin { get; set; } = UserType.UnConnected;
    public int Password { get; set; }


//  *****public ActionResult Login([FromBody] User User)
//     {
//         List<User> users = _userService.GetAll();
//         User user = users.FirstOrDefault(user => user.Name == User.Name && user.Password == User.Password);

//         if (user == null)
//         {
//             return Unauthorized();
//         }
//         System.Console.WriteLine($"User: {user.Name} Login: {user.Password} Admin: {user.IsAdmin}");
//         var claims = new List<Claim>
//             {
//                 new Claim("type", "User"),
//                 new Claim("userId", user.Id.ToString())
//             };
//         if (user.IsAdmin)
//         {
//             System.Console.WriteLine("I am Admin");
//             claims.Add(new Claim("type", "Admin"));
//         }
        
//         System.Console.WriteLine("I in Login");
//         var token = TokenService.GetToken(claims);
//         return CreatedAtAction(TokenService.WriteToken(token) ,new {id= user.Id.ToString()} );
//     }
// ***********

  
}