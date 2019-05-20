using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public void AddTask(Chore task)
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

        public void UpdateTaskStatus(int id, Status status, User user)
        {
            using (var context = new TaskContext(_connectionString))
            {
                //var task = GetTaskById(id);
                //task.UserId = user.Id;
                //task.Status = status;
                //context.SaveChanges();

                context.Database.ExecuteSqlCommand(
                    "UPDATE Tasks SET UserId = @userId, Status = @status WHERE Id = @id",
                    new SqlParameter("@userId", user.Id),
                    new SqlParameter("@id", id),
                    new SqlParameter("@status", status)
                    );
            }
        }

        public Chore GetTaskById(int id)
        {
            using (var context = new TaskContext(_connectionString))
            {
                return context.Tasks.Include(t => t.User).FirstOrDefault(t => t.Id == id);
            }
        }

    }
}
