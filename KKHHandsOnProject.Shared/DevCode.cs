using KKHHandsOnProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKHHandsOnProject.Shared
{
    public static class DevCode
    {
        public static LogInformation GetLogs(string message, LogData? logData)
        {
            LogInformation log = new LogInformation();
            log.Message = message;
            log.Data    = logData;
            return log;
        }
    }
}
