using Tasks.Models;
using Microsoft.AspNetCore.Mvc;
using Tasks.Interfaces;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json;
namespace Tasks.services;

public class TaskServiceFile:ITaskService
{
    private  List<MyTask> list;
    private string filePath;
     public TaskServiceFile(IWebHostEnvironment webHost)
    {
        this.filePath=Path.Combine(webHost.ContentRootPath, "Data", "Task.json");
          using (var jsonFile = File.OpenText(filePath))
            {
                list = JsonSerializer.Deserialize<List<MyTask>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
    }
    private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(list));
        }
    public List<MyTask> GetAll()=>list;
 
    public List<MyTask> Get(int userId)
    {
        return list.FindAll(t => t.UserId == userId);
    }
   
    public void Post(MyTask newTask)
    {
        int max = list.Max(p => p.Id);
        newTask.Id = max + 1;
        list.Add(newTask);
        saveToFile();
    }

    public void Put(int id, MyTask newTask)
    {
        if (id == newTask.Id)
        {
            var task = list.Find(p => p.Id == id);
            if (task != null)
            {
                int index = list.IndexOf(task);
                list[index] = newTask;
                saveToFile();
            }
        }
    }
    public void Delete(int id)
    {

        var task = list.Find(p => p.Id == id);
        if (task != null)
        {
            list.Remove(task);
            saveToFile();
        }

    }

    public  void DeleteAllTasks(int UserId)
    {
        list.RemoveAll(p=>p.UserId==UserId);
        saveToFile();
    }
}