using Tasks.Models;
namespace Tasks.Interfaces;
public interface IUserService
{
    List<User> GetAll();
    User Get(int id);
    void Post(User newUser);
    void Put(User newuser,User olsUser);
    void Delete(int id);
    // User FindUser();
}