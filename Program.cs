using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Critical
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Debug.Listeners.Add(new TextWriterTraceListener(File.CreateText("Log.txt")));
            Debug.AutoFlush = true;
            Critical cr = new Critical();
            cr.Work();
        }
    }
}
