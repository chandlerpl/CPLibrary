using System;
using System.Collections.Generic;
using System.Text;

namespace CP.Common.Logger
{
    public class ConsoleLogger : ILogger
    {
        public void Write(string message, LogLevel level = LogLevel.INFO)
        {
            Console.Write(DateTime.Now + " - " + level.ToString() + ": " + message);
        }

        public void WriteLine(string message, LogLevel level = LogLevel.INFO)
        {
            Console.WriteLine(DateTime.Now + " - " + level.ToString() + ": " + message);
        }
    }
}
