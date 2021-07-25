using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class LoggerClass
    {
        string path = @"C:\Users\colak\Desktop\PROJEKATRES3a\ProjectRazvojEES\Logger.txt";

        public LoggerClass() { }

        public void LogEvent(string from, string message)
        {

            if (from == null || message == null)
            {
                throw new ArgumentNullException("Ne sme biti null.");
            }

            if (from.Equals(String.Empty) || message.Equals(String.Empty))
            {
                throw new ArgumentException("Ne sme biti prazan string.");
            }

            if (!(from == "Writter") && !(from == "DumpingBuffer") && !(from == "Historical") && !(from == "Reader") && !(from == "Database") && !(from == "Program"))
            {
                throw new ArgumentException("Mora biti neka od ovih komponenti.");
            }

            FileStream fileStream = new FileStream(path, FileMode.Append);
            StreamWriter sw = new StreamWriter(fileStream);
            string upis = "Time: " + DateTime.Now + "\t" + "Component: " + from + "\tMessage: " + message + Environment.NewLine + "---------------------\n";
            sw.WriteLine(upis);
            sw.Close();
            fileStream.Close();
        }
    }
}
