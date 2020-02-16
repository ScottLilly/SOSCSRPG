using System;
using System.IO;

namespace Engine.Services
{
    public static class LoggingService
    {
        private const string LOG_FILE_DIRECTORY = "Logs";

        static LoggingService()
        {
            string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LOG_FILE_DIRECTORY);

            if(!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
        }

        public static void Log(Exception exception, bool isInnerException = false)
        {
            using(StreamWriter sw = new StreamWriter(LogFileName(), true))
            {
                sw.WriteLine(isInnerException ? "INNER EXCEPTION" : $"EXCEPTION: {DateTime.Now}");
                sw.WriteLine(new string(isInnerException ? '-' : '=', 40));
                sw.WriteLine($"{exception.Message}");
                sw.WriteLine($"{exception.StackTrace}");

                sw.WriteLine(); // Blank line, to make the log file easier to read
            }

            if(exception.InnerException != null)
            {
                Log(exception.InnerException, true);
            }
        }

        private static string LogFileName()
        {
            // This will create a separate log file for each day.
            // Not that we're hoping to have many days of errors.
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LOG_FILE_DIRECTORY,
                                $"SOSCSRPG_{DateTime.Now:yyyyMMdd}.log");
        }
    }
}