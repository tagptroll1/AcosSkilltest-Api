using System;

namespace Acos.ProgrammingTask.Utils
{
    public class WhiteboardException : Exception
    {
        public WhiteboardException() :base() {}
        public WhiteboardException(string message) : base(message) {}
    }
}