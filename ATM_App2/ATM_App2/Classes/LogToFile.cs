using System.IO;
using ATM_App2.Interfaces;

namespace ATM_App2.Events
{
    /// <summary>
    /// Class that logs all event 
    /// constructor take file path, default in build directory
    /// </summary>
    public class LogToFile : ILogToFile
    {
        private readonly string _path;

        public LogToFile(string path = @"SeparationLog.txt")
        {
            _path = path;
        }
        /// <summary>
        /// Logs message in file
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            if (!File.Exists(_path))
            {
                var sr = File.CreateText(_path);
                sr.Close();
            }

            using (var sr = File.AppendText(_path))
            {
                sr.WriteLine(message);
            }
        }
    }
}