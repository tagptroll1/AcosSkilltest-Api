using System;

namespace Acos.ProgrammingTask.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime created { get; set; }
        public bool Finished { get; set; }
        public PostIt PostIt { get; set; }
    }
}