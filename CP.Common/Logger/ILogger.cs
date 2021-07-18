using System;
using System.Collections.Generic;
using System.Text;

namespace CP.Common.Logger
{
    public enum LogLevel
    {
        MESSAGE, INFO, WARNING, ERROR
    }

    public interface ILogger
    {
        void WriteLine(string message, LogLevel level = LogLevel.MESSAGE);

        void Write(string message, LogLevel level = LogLevel.MESSAGE);
    }
}
