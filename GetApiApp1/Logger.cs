using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetApiApp1
{
    public class Logger
    {
        private string pathOfLogFile;
        public Logger(string filePath)
        {
            pathOfLogFile = filePath;

        }
        public void Log(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(pathOfLogFile, true))
                {
                    string logMessage = $"{DateTime.Now} - {message}";
                    writer.WriteLine(logMessage);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("There is some error in Logging the Message, below is the error");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
