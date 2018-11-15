using System.IO;
using ATM_App2.Classes;
using ATM_App2.Interfaces;

namespace ATM_App2.Events
{
    public class LogToFile : ILogToFile
    {
        private readonly string _path;

        public LogToFile(string path = @"SeparationLog.txt")
        {
            _path = path;
        }

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

        public void Log(Danger dan)
        {
            if (!File.Exists(_path))
            {
                var sr = File.CreateText(_path);
                sr.Close();
            }

            using (var sr = File.AppendText(_path))
            {
                sr.Write(dan.track1_.tag_ + " and " + dan.track2_.tag_);
                sr.Write($" Distance: {0} ", dan.distance_);
            }
        }
    }
}