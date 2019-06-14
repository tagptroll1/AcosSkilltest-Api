
namespace Acos.ProgrammingTask.Models
{
    public class Whiteboard
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public User Owner { get; set; }
        
    }

    public class WhiteboardDtoIn
    {
        

        public int Id { get; set; }

        public string Title { get; set; }

        public string Owner { get; set; }
    }

    public class WhiteboardDtoOut
    {
        

        public int Id { get; set; }

        public string Title { get; set; }

        public UserDto Owner { get; set; }
    }
}