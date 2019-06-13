namespace Acos.ProgrammingTask.Models
{
    public class PostIt
    {
        public int Id { get; set; }
        public Todo Todo { get; set; }
        public string Color { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}