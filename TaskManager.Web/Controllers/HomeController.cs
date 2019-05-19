using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TaskManager.Data;
using TaskManager.Web.Models;

namespace TaskManager.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            var taskRepo = new TaskRepository(_connectionString);
            var userRepo = new UserRepository(_connectionString);
            var vm = new IndexViewModel
            {
                Tasks = taskRepo.GetTasksThatAreNotDone(),
                CurrentUser = userRepo.GetByEmail(User.Identity.Name)

            };

            return View(vm);
        }
    }
}
