using System;
using System.Collections.Generic;
using System.Text;

namespace LoggerService
{
    public interface ILoggerManager
    {
        void LogError(string message);
    }
}
