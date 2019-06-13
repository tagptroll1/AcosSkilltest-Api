
namespace Acos.ProgrammingTask.Models
{
    public class Whiteboard
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public PostIt[] Postits { get; set; }
    }
}