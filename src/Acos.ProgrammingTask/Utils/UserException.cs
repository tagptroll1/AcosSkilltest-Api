using System;

namespace Acos.ProgrammingTask.Utils
{
    public class UserException : Exception
    {
        public UserException() :base() {}
        public UserException(string message) : base(message) {}
    }
}