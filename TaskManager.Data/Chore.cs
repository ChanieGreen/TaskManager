using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Data
{
    public class Chore
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Status Status { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
