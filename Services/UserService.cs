using Tasks.Models;
using Microsoft.AspNetCore.Mvc;
using Tasks.Interfaces;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json;
namespace Tasks.services;

public class UserServiceFile:IUserService
{
    private  List<User> list;
    private string filePath;
     public UserServiceFile(IWebHostEnvironment webHost)
    {
        this.filePath=Path.Combine(webHost.ContentRootPath, "Data", "Users.json");
          using (var jsonFile = File.OpenText(filePath))
            {
                list = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
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
    public List<User> GetAll()=>list;
 
    public User Get(int id)
    {
        return list.FirstOrDefault(t=>t.Id==id); }
   
    public void Post(User newUser)
    {
        int max = list.Max(p => p.Id);
        newUser.Id = max + 1;
        list.Add(newUser);
        saveToFile();
    }

    public void Put(int id, User newUser)
    {
        if (id == newUser.Id)
        {
            var user = list.Find(p => p.Id == id);
            if (user != null)
            {
                int index = list.IndexOf(user);
                list[index] = newUser;
                saveToFile();
            }
        }
    }
    public void Delete(int id)
    {

        var user = list.Find(p => p.Id == id);
        if (user != null)
        {
            list.Remove(user);
            saveToFile();
        }

    }

  
    public User FindUser(User user)
    {
        return  list.Find(p => p.UserName == user.UserName && p.Password == user.Password);
    }


}



