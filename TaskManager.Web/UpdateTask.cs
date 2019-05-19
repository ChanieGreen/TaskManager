using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;

namespace TaskManager.Web
{
    public class UpdateTask
    {
        public int Id { get; set; }
        public Status Status { get; set; } 
    }
}
