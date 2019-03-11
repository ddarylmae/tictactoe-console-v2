using System;

namespace TictactoeVer2
{
    public class OutputWriter : IOutputWriter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}