using System.ComponentModel.DataAnnotations;

namespace Acos.ProgrammingTask.Models
{
    public class Postit
    {
        public int Id { get; set; }
        public Todo Todo { get; set; }
        public Whiteboard Whiteboard { get; set; }
        public User Owner { get; set;}
        public string Color { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class PostitDtoOut
    {
        public int Id { get; set; }
        public TodoDto Todo { get; set; }
        public WhiteboardDtoOut Whiteboard { get; set; }
        public UserDto Owner { get; set;}
        public string Color { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class PostitDtoIn
    {
        public int Id { get; set; }
        
        [Required]
        public TodoDto Todo { get; set; }
        public int WhiteboardId { get; set; }
        public int UserId { get; set;}
        public string Color { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}