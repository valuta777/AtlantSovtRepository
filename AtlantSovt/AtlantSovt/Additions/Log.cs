using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlantSovt.Additions
{
    class Log
    {
        private static object sync = new object();

        public static void Write(Exception ex)
        {
            try
            {
                // Path .\\Log
                string pathToLog = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Log").Replace("\\bin\\Release", "");
                if (!Directory.Exists(pathToLog))
                    Directory.CreateDirectory(pathToLog); // Create folder, if it need to
                string filename = Path.Combine(pathToLog, Environment.MachineName + "." +DateTime.Now.ToShortDateString() + ".log");

                string fullText = Environment.NewLine + DateTime.Now.ToString("[0:dd.MM.yyy HH:mm:ss.fff]") + "\t\r\n" + ex.StackTrace + "\t\r\n" + ex.Message + Environment.NewLine;

                lock (sync)
                {
                    File.AppendAllText(filename, fullText, Encoding.GetEncoding("Windows-1251"));
                }
                Mail.Send();
            }
            catch
            {
                // Catch all and do nothing
            }
        }
    }
}
