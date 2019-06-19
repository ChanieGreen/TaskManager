using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;

namespace TaskManager.Web
{
    public class TaskHub : Hub
    {
        private string _connectionString;

        public TaskHub(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        //public void SendMessage(ChatMessage message)
        //{
        //    Context.User.Identity.Name
        //    Clients.All.SendAsync("NewMessage", message);
        //}

        public void AddNewTask(Chore task)
        {
            task.Status = Status.NotStarted;
            var db = new TaskRepository(_connectionString);
            db.AddTask(task);
            var userRepo = new UserRepository(_connectionString);
            Clients.All.SendAsync("AddTask", task);
        }

        public void UpdateStatus(UpdateTask updateTask)
        {
            var db = new TaskRepository(_connectionString);
            var userRepo = new UserRepository(_connectionString);
            db.UpdateTaskStatus(updateTask.Id, updateTask.Status, userRepo.GetByEmail(Context.User.Identity.Name));
            var task = db.GetTaskById(updateTask.Id);
            if (updateTask.Status == Status.Done)
            {
                Clients.All.SendAsync("RemoveTask", task.Id);
            }
            else
            {
                Clients.Caller.SendAsync("MyTask", task);
                Clients.Others.SendAsync("UpdateTask", task);
            }
            
        }
    }
}
