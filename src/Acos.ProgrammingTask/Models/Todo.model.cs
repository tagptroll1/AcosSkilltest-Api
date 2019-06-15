using System;

namespace Acos.ProgrammingTask.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public bool Finished { get; set; }
        public int PostitForeignKey { get; set; }
        public Postit Postit { get; set; }
    }

    public class TodoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public bool Finished { get; set; }
    }

}