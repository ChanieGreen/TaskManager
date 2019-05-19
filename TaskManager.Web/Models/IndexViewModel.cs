using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;

namespace TaskManager.Web.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Chore> Tasks { get; set; }
        public User CurrentUser { get; set; }
    }
}
