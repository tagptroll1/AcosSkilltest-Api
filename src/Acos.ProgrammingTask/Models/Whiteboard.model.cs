
using System.Collections.Generic;

namespace Acos.ProgrammingTask.Models
{
    public class Whiteboard
    {
        public int WhiteboardId { get; set; }
        public string Title { get; set; }
        public ICollection<Postit> Postits { get; set; }
        public User User { get; set; }
    }

    public class WhiteboardDtoIn
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class WhiteboardDtoOut
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public UserDto User { get; set; }
        public List<Postit> Postits { get; set; }
    }
}