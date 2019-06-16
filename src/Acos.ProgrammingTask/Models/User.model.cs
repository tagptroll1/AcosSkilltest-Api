
using System.Collections.Generic;

namespace Acos.ProgrammingTask.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public byte[] PassHash { get; set; }
        public byte[] PassSalt { get; set; }
        public ICollection<Whiteboard> Whiteboards { get; set; }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}