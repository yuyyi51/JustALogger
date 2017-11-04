using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace JustALogger
{
    public class Logger
    {
        static public string logPath;
        static public string LogPath
        {
            get { return logPath; }
            set
            {
                logPath = value;
                if(!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }
            }
        }
        public delegate void LogHandler(string info);
        static public void ConsoleHandler(string info)
        {
            Console.Out.WriteLine(info);
        }
        static public void FileHandler(string info)
        {
            FileStream fs = new FileStream(logPath+"\\"+DateTime.Now.ToString("yyyy-MM-dd")+".log",FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(info);
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        static private LogHandler LogEvent;
        static Logger()
        {
            logPath = ".";
        }

        static public void Log(string info)
        {
            StringBuilder builder = new StringBuilder();
            StackFrame frame = new StackFrame(1);
            builder.AppendFormat("[{0}] {1} -> {2}",
                DateTime.Now.ToString(),
                frame.GetMethod(),
                info);
            LogEvent?.Invoke(builder.ToString());
        }
        static public void Add(LogHandler lh)
        {
            LogEvent += lh;
        }
    }
}
