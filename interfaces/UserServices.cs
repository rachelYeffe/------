using Tasks.Models;
namespace Tasks.Interfaces;
public interface IUserService
{
    List<User> GetAll();
    User Get(int id);
    void Post(User newUser);
    void Put(int id,User newUser);
    void Delete(int id);
    User FindUser(User user);
}