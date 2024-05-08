using Tasks.Models;
namespace Tasks.Interfaces;
public interface ITaskService
{
    List<MyTask> Get(int Id);
    void Post(MyTask newTask);
    void Put(int id,MyTask newTask);
    void Delete(int id);

}