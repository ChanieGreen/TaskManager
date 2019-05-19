using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManager.Data
{
    public class TaskRepository
    {
        private string _connectionString;

        public TaskRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddTask (Chore task)
        {
            using (var context = new TaskContext(_connectionString))
            {
                context.Tasks.Add(task);
                context.SaveChanges();
            }
        }

        public IEnumerable<Chore> GetTasksThatAreNotDone()
        {
            using (var context = new TaskContext(_connectionString))
            {
                return context.Tasks
                    .Include(t => t.User)
                    .Where(t => t.Status != Status.Done).ToList();
            }
        }

        public void UpdateTaskStatus (int id, Status status,User user)
        {
            using (var context = new TaskContext(_connectionString))
            {
                var task = GetTaskById(id);
                task.UserId = user.Id;
                task.Status = status;
                context.SaveChanges();
            }
        }

        public Chore GetTaskById(int id)
        {
            using (var context = new TaskContext(_connectionString))
            {
                return context.Tasks.FirstOrDefault(t => t.Id == id);
            }
        }

    }
}
